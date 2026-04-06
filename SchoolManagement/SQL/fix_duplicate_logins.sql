-- =============================================================================
-- Fix Duplicate Logins / Enforce "One Login Per Student"
-- =============================================================================

DO $$
DECLARE
    r RECORD;
    v_rows_deleted INT := 0;
    v_links_deleted INT := 0;
BEGIN
    RAISE NOTICE 'Starting duplicate login cleanup...';

    -- 1. Unlink Extra Parent Logins (Keep only 1 per Student)
    FOR r IN (
        SELECT s.id AS student_id, s.first_name, s.last_name
        FROM public.students s
        JOIN public.parent_student ps ON s.id = ps.student_id
        WHERE ps.is_deleted = false
        GROUP BY s.id, s.first_name, s.last_name
        HAVING COUNT(DISTINCT ps.parent_id) > 1
    ) LOOP
        -- Delete extra links for this student
        DELETE FROM public.parent_student
        WHERE student_id = r.student_id
          AND parent_id NOT IN (
              SELECT p.id
              FROM public.parent_student ps2
              JOIN public.parents p ON ps2.parent_id = p.id
              JOIN public.users u ON p.user_id = u.user_id
              WHERE ps2.student_id = r.student_id
              ORDER BY 
                 -- Priority: Non-placeholder email first, then smallest ID
                 (CASE WHEN u.email LIKE '%@neuropi.placeholder' THEN 1 ELSE 0 END) ASC,
                 p.id ASC
              LIMIT 1
          );
        
        GET DIAGNOSTICS v_rows_deleted = ROW_COUNT;
        v_links_deleted := v_links_deleted + v_rows_deleted;
        
        RAISE NOTICE 'Student % % (ID: %): Removed % extra parent link(s).', r.first_name, r.last_name, r.student_id, v_rows_deleted;
    END LOOP;

    RAISE NOTICE 'Link cleanup complete. Total links removed: %', v_links_deleted;

    -- 2. Cleanup Orphaned Records (Optional / Safe Cleanup)
    -- Order is critical: Children FKs -> Users
    
    -- Identify Orphaned Users Correctly
    -- Filter out system users (ID < 10) just to be safe
    CREATE TEMP TABLE orphaned_users AS
    SELECT u.user_id
    FROM public.users u
    WHERE u.created_by = 1
      AND u.user_id > 10 -- SAFETY: Don't delete seed users
      AND NOT EXISTS (SELECT 1 FROM public.parents p WHERE p.user_id = u.user_id) -- No parent record
      AND NOT EXISTS (SELECT 1 FROM public.user_roles ur WHERE ur.user_id = u.user_id AND ur.role_id != 5); -- No roles except possibly 5
      
    RAISE NOTICE 'Found % orphaned users to clean up.', (SELECT COUNT(*) FROM orphaned_users);

    -- Delete Orphaned Parents
    DELETE FROM public.parents p
    WHERE p.user_id IN (SELECT user_id FROM orphaned_users);

    -- Delete Orphaned User Roles
    DELETE FROM public.user_roles ur
    WHERE ur.user_id IN (SELECT user_id FROM orphaned_users);

    -- Delete Orphaned User Departments
    DELETE FROM public.user_departments ud
    WHERE ud.user_id IN (SELECT user_id FROM orphaned_users);

    -- Handle Student Attendance (Reassign to Admin)
    UPDATE public.student_attendance sa 
    SET user_id = 1
    WHERE sa.user_id IN (SELECT user_id FROM orphaned_users);
    
    UPDATE public.student_attendance sa 
    SET created_by = 1
    WHERE sa.created_by IN (SELECT user_id FROM orphaned_users);

    -- Handle Groups (Reassign Ownership / Audit)
    UPDATE public.groups g 
    SET updated_by = 1, created_by = 1
    WHERE g.updated_by IN (SELECT user_id FROM orphaned_users)
       OR g.created_by IN (SELECT user_id FROM orphaned_users);

    -- Handle Group Users (Constraint Fix: Remove membership)
    DELETE FROM public.group_users gu
    WHERE gu.user_id IN (SELECT user_id FROM orphaned_users);

    -- Handle Time Table Topics (Reassign to Admin)
    UPDATE public.time_table_topics ttt
    SET updated_by = 1
    WHERE ttt.updated_by IN (SELECT user_id FROM orphaned_users);

    UPDATE public.time_table_topics ttt
    SET created_by = 1
    WHERE ttt.created_by IN (SELECT user_id FROM orphaned_users);

    -- Handle Course Teacher (Reassign to Admin - FK is likely teacher_id)
    UPDATE public.course_teacher ct
    SET teacher_id = 1 
    WHERE ct.teacher_id IN (SELECT user_id FROM orphaned_users);

    UPDATE public.course_teacher ct
    SET created_by = 1
    WHERE ct.created_by IN (SELECT user_id FROM orphaned_users);

    -- Handle Contact (Reassign to NULL for user_id to unlink, or 1 for audit)
    -- Assuming user_id is the user associated with this contact. Since user deleted, unlink.
    -- Assuming this contact needs to survive (Mother's contact for example).
    UPDATE public.contact c
    SET user_id = NULL
    WHERE c.user_id IN (SELECT user_id FROM orphaned_users);

    UPDATE public.contact c
    SET created_by = 1
    WHERE c.created_by IN (SELECT user_id FROM orphaned_users);
    
    UPDATE public.contact c
    SET updated_by = 1
    WHERE c.updated_by IN (SELECT user_id FROM orphaned_users);


    -- Delete Orphaned Users
    DELETE FROM public.users u
    WHERE u.user_id IN (SELECT user_id FROM orphaned_users);

    DROP TABLE orphaned_users;
    
    RAISE NOTICE 'Orphan cleanup complete.';

END $$;

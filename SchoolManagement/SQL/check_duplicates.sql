-- =============================================================================
-- Check for Duplicate Logins / Multiple Logins per Student
-- =============================================================================

-- 1. Students having MORE THAN ONE Parent Login linked
-- (Violation of "One Login Per Kid" policy if active)
SELECT 
    s.id AS student_id,
    s.first_name || ' ' || COALESCE(s.last_name, '') AS student_name,
    COUNT(DISTINCT p.user_id) AS parent_login_count,
    STRING_AGG(u.username, ', ') AS usernames,
    STRING_AGG(u.email, ', ') AS emails
FROM public.students s
JOIN public.parent_student ps ON s.id = ps.student_id
JOIN public.parents p ON ps.parent_id = p.id
JOIN public.users u ON p.user_id = u.user_id
WHERE s.is_deleted = false 
  AND ps.is_deleted = false 
  AND p.is_deleted = false
GROUP BY s.id, s.first_name, s.last_name
HAVING COUNT(DISTINCT p.user_id) > 1
ORDER BY parent_login_count DESC;


-- 2. Users sharing the exact same Email Address (Multiple User IDs for same email)
-- (Potential Duplicate Users)
SELECT 
    email,
    COUNT(user_id) as user_count,
    STRING_AGG(username, ', ') as usernames,
    STRING_AGG(user_id::text, ', ') as user_ids
FROM public.users
WHERE email IS NOT NULL 
GROUP BY email
HAVING COUNT(user_id) > 1;


-- 3. Users sharing the exact same Mobile Number (Multiple User IDs for same phone)
-- (Potential Duplicate Users)
SELECT 
    mobile_number,
    COUNT(user_id) as user_count,
    STRING_AGG(username, ', ') as usernames,
    STRING_AGG(user_id::text, ', ') as user_ids
FROM public.users
WHERE mobile_number IS NOT NULL AND mobile_number != '0000000000'
GROUP BY mobile_number
HAVING COUNT(user_id) > 1;

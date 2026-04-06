-- Cleanup Script
-- Generated on 2026-02-17 17:05:00
-- WARNING: This will delete records for the specified students/families.

DO $$
DECLARE
    v_tenant_id INT := 1;
BEGIN
    RAISE NOTICE 'Starting cleanup...';

    -- Clean up Student Course mappings
    DELETE FROM public.student_course WHERE student_id IN (
        SELECT id FROM public.students WHERE 
          (first_name = 'Dhiyansh reddy' AND last_name = 'Yasa' AND dob = '2021-10-07') OR
          (first_name = 'Aadhya' AND last_name = 'Thummala aadhya' AND dob = '2023-06-16')
    );

    -- Clean up Parent Student mappings
    DELETE FROM public.parent_student WHERE student_id IN (
        SELECT id FROM public.students WHERE 
          (first_name = 'Dhiyansh reddy' AND last_name = 'Yasa' AND dob = '2021-10-07') OR
          (first_name = 'Aadhya' AND last_name = 'Thummala aadhya' AND dob = '2023-06-16')
    );
     
    -- Clean up Students
    DELETE FROM public.students WHERE 
        (first_name = 'Dhiyansh reddy' AND last_name = 'Yasa' AND dob = '2021-10-07') OR
        (first_name = 'Aadhya' AND last_name = 'Thummala aadhya' AND dob = '2023-06-16');


    -- Clean up Parents (via specific Users created)
    DELETE FROM public.parents WHERE user_id IN (
        SELECT user_id FROM public.users WHERE email IN ('mahendherreddyyasa1990@gmail.com', 'Tulasigudavalli@gmail.com')
    );

    -- Clean up User Roles (via specific Users created)
    DELETE FROM public.user_roles WHERE user_id IN (
        SELECT user_id FROM public.users WHERE email IN ('mahendherreddyyasa1990@gmail.com', 'Tulasigudavalli@gmail.com')
    );

    -- Clean up Users (via specific Email)
    DELETE FROM public.users WHERE email IN ('mahendherreddyyasa1990@gmail.com', 'Tulasigudavalli@gmail.com');

    -- Clean up CONTACTS (Father)
    DELETE FROM public.contact WHERE 
       (email = 'mahendherreddyyasa1990@gmail.com' OR name = 'Y.mahender reddy Yasa') OR
       (email = 'Tulasigudavalli@gmail.com' OR name = 'Thummala gopinath');
    
    -- Clean up CONTACTS (Mother - shared email ones)
    DELETE FROM public.contact WHERE 
       (email = 'Mahendherreddyyasa1990@gmail.com' AND name = 'A.shobarani') OR
       (email = 'Tulasigudavalli@gmail.com' AND name = 'Tulasi Thummala tulasi');

    RAISE NOTICE 'Cleanup completed.';
END $$;

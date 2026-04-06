-- Student Registration Script - Generated Version
-- Generated on 2026-02-17 17:04:30

DO $$
DECLARE
    v_tenant_id INT := 1;
    v_parent_role_id INT := 5;
    v_password TEXT := 'changeme';
    v_created_by INT := 1;
    v_placeholder_phone TEXT := '0000000000';
    v_placeholder_addr TEXT := 'NA';
    v_placeholder_city TEXT := 'Hyderabad';
    v_placeholder_state TEXT := 'Telangana';
    v_placeholder_pincode TEXT := '000000';
    v_f_contact_id INT;
    v_m_contact_id INT;
    v_p_user_id INT; -- Primary User ID
    v_p_parent_id INT; -- Primary Parent ID
    v_student_id INT;
    v_branch_id INT;
    v_course_id INT;
    v_p_name TEXT;
    v_p_email TEXT;
    v_p_phone TEXT;
    v_p_type TEXT; 
    v_p_uname TEXT;
    v_p_fname TEXT;
    v_p_lname TEXT;
    v_p_email_to_use TEXT;
    v_p_dob DATE;
BEGIN


-- =============================================================================
-- STUDENT 1: Dhiyansh reddy Yasa
-- =============================================================================
-- Father Contact: Y.mahender reddy Yasa
v_p_name := 'Y.mahender reddy Yasa'; v_p_email := 'mahendherreddyyasa1990@gmail.com'; v_p_phone := '9032165732'; v_p_type := 'FATHER';

    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_f_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    
    IF v_f_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_f_contact_id;
    END IF;

-- Mother Contact: A.shobarani
v_p_name := 'A.shobarani'; v_p_email := 'Mahendherreddyyasa1990@gmail.com'; v_p_phone := '9505832179'; v_p_type := 'MOTHER';

    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_m_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_m_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_m_contact_id;
    END IF;

-- Primary Login Identity: FATHER (Y.mahender reddy Yasa)
v_p_uname := 'mahendherreddyyasa1990@gmail.com'; v_p_fname := 'Y.mahender reddy'; v_p_lname := 'Yasa';
v_p_email := 'mahendherreddyyasa1990@gmail.com'; v_p_phone := '9032165732';
v_p_dob := NULL;

    -- User & Role (Primary)
    v_p_user_id := NULL;
    IF v_p_email IS NOT NULL THEN
        SELECT user_id INTO v_p_user_id FROM public.users WHERE email = v_p_email AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    IF v_p_user_id IS NULL THEN
         SELECT user_id INTO v_p_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_p_user_id IS NULL THEN
        v_p_email_to_use := COALESCE(v_p_email, v_p_uname || '@neuropi.placeholder');
        INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, mobile_number, dob, created_by)
        VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email_to_use, v_tenant_id, v_p_phone, v_p_dob, v_created_by) RETURNING user_id INTO v_p_user_id;

        INSERT INTO public.user_roles (user_id, role_id, tenant_id, created_by) VALUES (v_p_user_id, v_parent_role_id, v_tenant_id, v_created_by);
    END IF;

    -- Parent (Primary)
    SELECT id INTO v_p_parent_id FROM public.parents WHERE user_id = v_p_user_id AND tenant_id = v_tenant_id LIMIT 1;
    IF v_p_parent_id IS NULL THEN
        INSERT INTO public.parents (user_id, tenant_id, created_by) VALUES (v_p_user_id, v_tenant_id, v_created_by) RETURNING id INTO v_p_parent_id;
    END IF;


    -- Student: Dhiyansh reddy
    SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Dhiyansh reddy' AND last_name = 'Yasa' AND dob = '2021-10-07' AND tenant_id = v_tenant_id LIMIT 1;
    
    IF v_student_id IS NULL THEN
        INSERT INTO public.students (first_name, middle_name, last_name, dob, gender, branch_id, course_id, tenant_id, reg_number, f_contact, m_contact, bloodgroup, date_of_joining, created_by)
        VALUES ('Dhiyansh reddy', '', 'Yasa', '2021-10-07', 'Male', 14, 3, v_tenant_id, NULL, v_f_contact_id, v_m_contact_id, NULL, '2025-12-27', v_created_by) RETURNING id INTO v_student_id;
        
        -- Map Parents (Primary Only)
        IF NOT EXISTS (SELECT 1 FROM public.parent_student WHERE parent_id = v_p_parent_id AND student_id = v_student_id AND tenant_id = v_tenant_id) THEN
            INSERT INTO public.parent_student (parent_id, student_id, tenant_id, created_by) VALUES (v_p_parent_id, v_student_id, v_tenant_id, v_created_by);
        END IF;

        INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year, created_by) VALUES (v_student_id, 3, 14, v_tenant_id, TRUE, v_created_by);
    END IF;


-- =============================================================================
-- STUDENT 2: Aadhya Thummala aadhya
-- =============================================================================
-- Father Contact: Thummala gopinath
v_p_name := 'Thummala gopinath'; v_p_email := 'Tulasigudavalli@gmail.com'; v_p_phone := v_placeholder_phone; v_p_type := 'FATHER';

    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_f_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    
    IF v_f_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_f_contact_id;
    END IF;

-- Mother Contact: Tulasi Thummala tulasi
v_p_name := 'Tulasi Thummala tulasi'; v_p_email := 'Tulasigudavalli@gmail.com'; v_p_phone := v_placeholder_phone; v_p_type := 'MOTHER';

    IF v_p_email IS NOT NULL THEN
        SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    ELSE
        SELECT id INTO v_m_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_m_contact_id IS NULL THEN
        INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode, created_by)
        VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode, v_created_by) RETURNING id INTO v_m_contact_id;
    END IF;

-- Primary Login Identity: FATHER (Thummala gopinath)
v_p_uname := 'Tulasigudavalli@gmail.com'; v_p_fname := 'Thummala gopinath'; v_p_lname := '';
v_p_email := 'Tulasigudavalli@gmail.com'; v_p_phone := v_placeholder_phone;
v_p_dob := NULL;

    -- User & Role (Primary)
    v_p_user_id := NULL;
    IF v_p_email IS NOT NULL THEN
        SELECT user_id INTO v_p_user_id FROM public.users WHERE email = v_p_email AND tenant_id = v_tenant_id LIMIT 1;
    END IF;
    IF v_p_user_id IS NULL THEN
         SELECT user_id INTO v_p_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id LIMIT 1;
    END IF;

    IF v_p_user_id IS NULL THEN
        v_p_email_to_use := COALESCE(v_p_email, v_p_uname || '@neuropi.placeholder');
        INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, mobile_number, dob, created_by)
        VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email_to_use, v_tenant_id, v_p_phone, v_p_dob, v_created_by) RETURNING user_id INTO v_p_user_id;

        INSERT INTO public.user_roles (user_id, role_id, tenant_id, created_by) VALUES (v_p_user_id, v_parent_role_id, v_tenant_id, v_created_by);
    END IF;

    -- Parent (Primary)
    SELECT id INTO v_p_parent_id FROM public.parents WHERE user_id = v_p_user_id AND tenant_id = v_tenant_id LIMIT 1;
    IF v_p_parent_id IS NULL THEN
        INSERT INTO public.parents (user_id, tenant_id, created_by) VALUES (v_p_user_id, v_tenant_id, v_created_by) RETURNING id INTO v_p_parent_id;
    END IF;


    -- Student: Aadhya
    SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Aadhya' AND last_name = 'Thummala aadhya' AND dob = '2023-06-16' AND tenant_id = v_tenant_id LIMIT 1;
    
    IF v_student_id IS NULL THEN
        INSERT INTO public.students (first_name, middle_name, last_name, dob, gender, branch_id, course_id, tenant_id, reg_number, f_contact, m_contact, bloodgroup, date_of_joining, created_by)
        VALUES ('Aadhya', '', 'Thummala aadhya', '2023-06-16', 'Female', 14, 2, v_tenant_id, NULL, v_f_contact_id, v_m_contact_id, NULL, '2025-11-14', v_created_by) RETURNING id INTO v_student_id;
        
        -- Map Parents (Primary Only)
        IF NOT EXISTS (SELECT 1 FROM public.parent_student WHERE parent_id = v_p_parent_id AND student_id = v_student_id AND tenant_id = v_tenant_id) THEN
            INSERT INTO public.parent_student (parent_id, student_id, tenant_id, created_by) VALUES (v_p_parent_id, v_student_id, v_tenant_id, v_created_by);
        END IF;

        INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year, created_by) VALUES (v_student_id, 2, 14, v_tenant_id, TRUE, v_created_by);
    END IF;

END $$;
-- Student Registration Script - FINAL VERIFIED VERSION
-- Generated for 7 students with parent logins, roles, and mappings.
-- Handles: contact -> users -> user_roles -> parents -> students -> parent_student -> student_course.
-- Includes duplicate checks and satisfy all NOT NULL constraints (pri_number, address_1, city, etc.).

DO $$
DECLARE
    v_tenant_id INT := 1;
    v_parent_role_id INT := 5;
    v_password TEXT := 'changeme';
    
    -- Mandatory Field Placeholders
    v_placeholder_phone TEXT := '0000000000';
    v_placeholder_addr TEXT := 'NA';
    v_placeholder_city TEXT := 'Hyderabad'; -- Placeholder for city
    v_placeholder_state TEXT := 'Telangana'; -- Placeholder for state
    v_placeholder_pincode TEXT := '000000'; -- Placeholder for pincode
    
    -- Variables to hold IDs
    v_f_contact_id INT;
    v_m_contact_id INT;
    v_f_user_id INT;
    v_m_user_id INT;
    v_f_parent_id INT;
    v_m_parent_id INT;
    v_student_id INT;
    
    -- Helper variables for iteration
    v_p_name TEXT;
    v_p_email TEXT;
    v_p_phone TEXT;
    v_p_type TEXT; -- 'FATHER' or 'MOTHER'
    v_p_uname TEXT;
    v_p_fname TEXT;
    v_p_lname TEXT;
BEGIN

-- =============================================================================
-- STUDENT 1: Gargi Jaiswal
-- =============================================================================
-- Mother: Rajani (rajani_nrcpb@yahoo.com)
v_p_name := 'Rajani'; v_p_email := 'rajani_nrcpb@yahoo.com'; v_p_phone := v_placeholder_phone; v_p_type := 'MOTHER';
v_p_uname := 'rajani_nrcpb@yahoo.com'; v_p_fname := 'Rajani'; v_p_lname := '.';

-- 1. Contact
SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode)
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
-- 2. User & Role
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id)
    VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id) RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
-- 3. Parent mapping
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN
    INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id;
END IF;

-- 4. Student & Mapping
SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Gargi' AND last_name = 'Jaiswal' AND dob = '2021-08-11' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, last_name, dob, gender, branch_id, course_id, tenant_id, m_contact, date_of_joining)
    VALUES ('Gargi', 'Jaiswal', '2021-08-11', 'Female', 12, 3, v_tenant_id, v_m_contact_id, '2025-12-22') RETURNING id INTO v_student_id;
    
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 3, 12, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 2: Shivansh Shivansh
-- =============================================================================
-- Father: Abhishek Singh (9536522290)
v_p_name := 'Abhishek Singh'; v_p_phone := '9536522290'; v_p_type := 'FATHER';
v_p_uname := 'abhishek_9536522290'; v_p_fname := 'Abhishek'; v_p_lname := 'Singh';

SELECT id INTO v_f_contact_id FROM public.contact WHERE pri_number = v_p_phone AND name = v_p_name AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, tenant_id, role_type_id, mobile_number) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_tenant_id, v_parent_role_id, v_p_phone) RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Suchitra Chowdhary (7599424790, Chaudharysuchitra407.@gmail.com)
v_p_name := 'Suchitra Chowdhary'; v_p_phone := '7599424790'; v_p_email := 'Chaudharysuchitra407.@gmail.com'; v_p_type := 'MOTHER';
v_p_uname := 'Chaudharysuchitra407.@gmail.com'; v_p_fname := 'Suchitra'; v_p_lname := 'Chowdhary';

SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone) RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Shivansh' AND last_name = 'Shivansh' AND dob = '2024-01-28' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, last_name, dob, gender, branch_id, course_id, tenant_id, f_contact, m_contact, date_of_joining)
    VALUES ('Shivansh', 'Shivansh', '2024-01-28', 'Male', 12, 5, v_tenant_id, v_f_contact_id, v_m_contact_id, '2026-02-14') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 5, 12, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 3: Yokshith Reddy Oota
-- =============================================================================
-- Father: Subha Reddy Oota (7730077530, subhareddyoota@gmail.com)
v_p_name := 'Subha Reddy Oota'; v_p_phone := '7730077530'; v_p_email := 'subhareddyoota@gmail.com'; v_p_type := 'FATHER';
v_p_uname := 'subhareddyoota@gmail.com'; v_p_fname := 'Subha'; v_p_lname := 'Reddy Oota';

SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1990-05-13') RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Naga Mani Yeruva (6301533424, yeruvanagamani@gmail.com)
v_p_name := 'Naga Mani Yeruva'; v_p_phone := '6301533424'; v_p_email := 'yeruvanagamani@gmail.com'; v_p_type := 'MOTHER';
v_p_uname := 'yeruvanagamani@gmail.com'; v_p_fname := 'Naga'; v_p_lname := 'Mani Yeruva';

SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1996-11-06') RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE reg_number = 'HYDKP2026-02' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, middle_name, last_name, dob, gender, branch_id, course_id, tenant_id, reg_number, f_contact, m_contact, bloodgroup, date_of_joining)
    VALUES ('Yokshith', 'Reddy', 'Oota', '2023-05-08', 'Male', 13, 2, v_tenant_id, 'HYDKP2026-02', v_f_contact_id, v_m_contact_id, 'A+', '2026-02-02') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 2, 13, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 4: Richard Rokkala
-- =============================================================================
-- Father: Vijay Krishna Rokkala (9164650600, vijay.rokkala@gmail.com)
v_p_name := 'Vijay Krishna Rokkala'; v_p_phone := '9164650600'; v_p_email := 'vijay.rokkala@gmail.com'; v_p_type := 'FATHER';
v_p_uname := 'vijay_9164650600'; v_p_fname := 'Vijay'; v_p_lname := 'Krishna Rokkala';

SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1988-02-07') RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Jessy Rani Rokkala (9550807450, vijay.rokkala@gmail.com)
v_p_name := 'Jessy Rani Rokkala'; v_p_phone := '9550807450'; v_p_email := 'vijay.rokkala@gmail.com'; v_p_type := 'MOTHER';
v_p_uname := 'jessy_9550807450'; v_p_fname := 'Jessy'; v_p_lname := 'Rani Rokkala';

SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND name = v_p_name AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1993-09-14') RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE reg_number = 'HYDKP2026-01' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, last_name, dob, gender, branch_id, course_id, tenant_id, reg_number, f_contact, m_contact, bloodgroup, date_of_joining)
    VALUES ('Richard', 'Rokkala', '2022-12-28', 'Male', 13, 2, v_tenant_id, 'HYDKP2026-01', v_f_contact_id, v_m_contact_id, 'O+', '2026-02-02') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 2, 13, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 5: Shivansh Das
-- =============================================================================
-- Father: Sutanu Das (8420196181, mailsutanu@yahoo.com)
v_p_name := 'Sutanu Das'; v_p_phone := '8420196181'; v_p_email := 'mailsutanu@yahoo.com'; v_p_type := 'FATHER';
v_p_uname := 'mailsutanu@yahoo.com'; v_p_fname := 'Sutanu'; v_p_lname := 'Das';

SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1985-05-15') RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Tanu Shree Modak (9903816181, mailsultanu@yahoo.com)
v_p_name := 'Tanu Shree Modak'; v_p_phone := '9903816181'; v_p_email := 'mailsultanu@yahoo.com'; v_p_type := 'MOTHER';
v_p_uname := 'tanushree_9903816181'; v_p_fname := 'Tanu'; v_p_lname := 'Shree Modak';

SELECT id INTO v_m_contact_id FROM public.contact WHERE pri_number = v_p_phone AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1998-02-19') RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Shivansh' AND last_name = 'Das' AND dob = '2023-04-27' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, last_name, dob, gender, branch_id, course_id, tenant_id, f_contact, m_contact, bloodgroup, date_of_joining)
    VALUES ('Shivansh', 'Das', '2023-04-27', 'Male', 13, 1, v_tenant_id, v_f_contact_id, v_m_contact_id, 'O+', '2025-12-02') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 1, 13, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 6: Srinika Valusa
-- =============================================================================
-- Father: Deepak Valusa (8099959197, deepakvalusa00@gmail.com)
v_p_name := 'Deepak Valusa'; v_p_phone := '8099959197'; v_p_email := 'deepakvalusa00@gmail.com'; v_p_type := 'FATHER';
v_p_uname := 'deepakvalusa00@gmail.com'; v_p_fname := 'Deepak'; v_p_lname := 'Valusa';

SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1992-05-28') RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Sravani Adepu (9177180178, adepusravani1410@gmail.com)
v_p_name := 'Sravani Adepu'; v_p_phone := '9177180178'; v_p_email := 'adepusravani1410@gmail.com'; v_p_type := 'MOTHER';
v_p_uname := 'adepusravani1410@gmail.com'; v_p_fname := 'Sravani'; v_p_lname := 'Adepu';

SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1995-10-14') RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Srinika' AND last_name = 'Valusa' AND dob = '2023-09-19' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, last_name, dob, gender, branch_id, course_id, tenant_id, f_contact, m_contact, bloodgroup, date_of_joining)
    VALUES ('Srinika', 'Valusa', '2023-09-19', 'Female', 13, 2, v_tenant_id, v_f_contact_id, v_m_contact_id, 'B+ve', '2025-10-30') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 2, 13, v_tenant_id, TRUE);
END IF;


-- =============================================================================
-- STUDENT 7: Ujjwal Ved Thamalapakula
-- =============================================================================
-- Father: Ashok Kumar Thamalapakula (9922008655, tashok2090@gmail.com)
v_p_name := 'Ashok Kumar Thamalapakula'; v_p_phone := '9922008655'; v_p_email := 'tashok2090@gmail.com'; v_p_type := 'FATHER';
v_p_uname := 'tashok2090@gmail.com'; v_p_fname := 'Ashok'; v_p_lname := 'Kumar Thamalapakula';

SELECT id INTO v_f_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_f_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_f_contact_id;
END IF;
SELECT user_id INTO v_f_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_f_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1990-07-25') RETURNING user_id INTO v_f_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_f_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_f_parent_id FROM public.parents WHERE user_id = v_f_user_id AND tenant_id = v_tenant_id;
IF v_f_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_f_user_id, v_tenant_id) RETURNING id INTO v_f_parent_id; END IF;

-- Mother: Mani Priya Garnepudi (9986102259, garnepudi.manipriya@gmail.com)
v_p_name := 'Mani Priya Garnepudi'; v_p_phone := '9986102259'; v_p_email := 'garnepudi.manipriya@gmail.com'; v_p_type := 'MOTHER';
v_p_uname := 'garnepudi.manipriya@gmail.com'; v_p_fname := 'Mani'; v_p_lname := 'Priya Garnepudi';

SELECT id INTO v_m_contact_id FROM public.contact WHERE email = v_p_email AND tenant_id = v_tenant_id;
IF v_m_contact_id IS NULL THEN
    INSERT INTO public.contact (name, email, pri_number, tenant_id, contact_type, address_1, city, state, pincode) 
    VALUES (v_p_name, v_p_email, v_p_phone, v_tenant_id, v_p_type, v_placeholder_addr, v_placeholder_city, v_placeholder_state, v_placeholder_pincode) RETURNING id INTO v_m_contact_id;
END IF;
SELECT user_id INTO v_m_user_id FROM public.users WHERE username = v_p_uname AND tenant_id = v_tenant_id;
IF v_m_user_id IS NULL THEN
    INSERT INTO public.users (username, first_name, last_name, password, email, tenant_id, role_type_id, mobile_number, dob) VALUES (v_p_uname, v_p_fname, v_p_lname, v_password, v_p_email, v_tenant_id, v_parent_role_id, v_p_phone, '1994-06-25') RETURNING user_id INTO v_m_user_id;
    INSERT INTO public.user_roles (user_id, role_id, tenant_id) VALUES (v_m_user_id, v_parent_role_id, v_tenant_id);
END IF;
SELECT id INTO v_m_parent_id FROM public.parents WHERE user_id = v_m_user_id AND tenant_id = v_tenant_id;
IF v_m_parent_id IS NULL THEN INSERT INTO public.parents (user_id, tenant_id) VALUES (v_m_user_id, v_tenant_id) RETURNING id INTO v_m_parent_id; END IF;

SELECT id INTO v_student_id FROM public.students WHERE first_name = 'Ujjwal' AND last_name = 'Thamalapakula' AND dob = '2023-01-03' AND tenant_id = v_tenant_id;
IF v_student_id IS NULL THEN
    INSERT INTO public.students (first_name, middle_name, last_name, dob, gender, branch_id, course_id, tenant_id, f_contact, m_contact, date_of_joining)
    VALUES ('Ujjwal', 'Ved', 'Thamalapakula', '2023-01-03', 'Male', 13, 1, v_tenant_id, v_f_contact_id, v_m_contact_id, '2025-11-06') RETURNING id INTO v_student_id;
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_f_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.parent_student (parent_id, student_id, tenant_id) VALUES (v_m_parent_id, v_student_id, v_tenant_id);
    INSERT INTO public.student_course (student_id, course_id, branch_id, tenant_id, is_current_year) VALUES (v_student_id, 1, 13, v_tenant_id, TRUE);
END IF;

END $$;

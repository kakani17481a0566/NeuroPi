-- Verification Script for Dynamic Forms
-- Includes: Target User (Teacher -> Student), Entry Date, Branch Assignemnts, Audit Columns

BEGIN;

-- 1. Setup Tenant & User
-- Tenant 101, User 1 (Admin/Teacher), User 505 (Student)

-- 2. Setup Master Data
INSERT INTO public.master_type (name, tenant_id)
VALUES ('FormQuestionType', 101)
ON CONFLICT DO NOTHING; 

DO $$
DECLARE 
    type_id INT;
    text_master_id INT;
    q_id BIGINT;
    s_id BIGINT;
    f_id BIGINT;
    sub_id BIGINT;
    teacher_id INT := 1;
    student_id INT := 505;
BEGIN
    SELECT id INTO type_id FROM public.master_type WHERE name = 'FormQuestionType' AND tenant_id = 101 LIMIT 1;
    
    INSERT INTO public.masters (name, masters_type_id, code, tenant_id, created_by)
    VALUES ('Text', type_id, 'TEXT', 101, teacher_id)
    RETURNING id INTO text_master_id;

    SELECT id INTO text_master_id FROM public.masters WHERE name = 'Text' AND masters_type_id = type_id LIMIT 1;

    -- 3. Create Question "Student Behavior"
    INSERT INTO public.form_questions (tenant_id, question_text, question_type_master_id, created_by) 
    VALUES (101, 'Student Behavior Comments', text_master_id, teacher_id)
    RETURNING id INTO q_id;

    -- 4. Create Section
    INSERT INTO public.form_sections (tenant_id, name, created_by)
    VALUES (101, 'Performance Review', teacher_id)
    RETURNING id INTO s_id;

    INSERT INTO public.section_questions (section_id, question_id, created_by)
    VALUES (s_id, q_id, teacher_id);

    -- 5. Create Form "Monthly Student Evaluation"
    INSERT INTO public.forms (tenant_id, title, created_by)
    VALUES (101, 'Monthly Student Evaluation', teacher_id)
    RETURNING id INTO f_id;

    INSERT INTO public.form_sections_mapping (form_id, section_id, created_by)
    VALUES (f_id, s_id, teacher_id);

    -- 6. Submit Form (Teacher submits FOR Student)
    INSERT INTO public.form_submissions (
        tenant_id, form_id, 
        submitted_by,       -- Teacher (Actor)
        target_user_id,     -- Student (Subject)
        status, submitted_at, total_score, created_by, entry_date
    )
    VALUES (
        101, f_id, 
        teacher_id,         -- User ID 1
        student_id,         -- User ID 505
        'SUBMITTED', NOW(), 0, teacher_id, CURRENT_DATE
    )
    RETURNING id INTO sub_id;
    
    INSERT INTO public.submission_values (submission_id, question_id, value_text, created_by)
    VALUES (sub_id, q_id, 'Good participation in class.', teacher_id);
    
    RAISE NOTICE 'Evaluation submitted by Teacher ID % for Student ID %', teacher_id, student_id;

END $$;

-- 7. Verification: Check Target User
SELECT 
    f.title,
    sub.submitted_by AS teacher_id,
    sub.target_user_id AS student_id,
    val.value_text AS comment
FROM public.form_submissions sub
JOIN public.forms f ON sub.form_id = f.id
JOIN public.submission_values val ON val.submission_id = sub.id
WHERE sub.tenant_id = 101 AND f.title = 'Monthly Student Evaluation';

ROLLBACK;

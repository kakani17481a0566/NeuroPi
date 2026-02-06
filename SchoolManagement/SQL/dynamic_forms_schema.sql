-- Dynamic Forms Schema for PostgreSQL
-- Supports Multi-tenancy, Reusability, Grading, System Masters, Branch Assignments, On-Behalf-Of
-- PKs: 'id', Audit: Standard MBaseModel

-- 1. REUSABLE QUESTIONS LIBRARY
CREATE TABLE IF NOT EXISTS public.form_questions (
    id BIGSERIAL PRIMARY KEY,
    tenant_id INT NOT NULL,
    question_text TEXT NOT NULL,
    question_type_master_id INT NOT NULL, 
    options JSONB, 
    validation_rules JSONB,
    placeholder TEXT,
    help_text TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_questions_tenant ON public.form_questions(tenant_id);

-- 2. REUSABLE SECTIONS
CREATE TABLE IF NOT EXISTS public.form_sections (
    id BIGSERIAL PRIMARY KEY,
    tenant_id INT NOT NULL,
    name VARCHAR(255) NOT NULL,
    description TEXT,
    is_active BOOLEAN DEFAULT TRUE,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_sections_tenant ON public.form_sections(tenant_id);

-- 3. SECTION -> QUESTION MAPPING
CREATE TABLE IF NOT EXISTS public.section_questions (
    id BIGSERIAL PRIMARY KEY,
    section_id BIGINT REFERENCES public.form_sections(id) ON DELETE CASCADE,
    question_id BIGINT REFERENCES public.form_questions(id) ON DELETE CASCADE,
    display_order INT NOT NULL DEFAULT 0,
    is_required BOOLEAN DEFAULT FALSE,
    custom_label TEXT,
    marks DECIMAL(5,2) DEFAULT 0,
    
    UNIQUE(section_id, question_id),
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_section_questions_order ON public.section_questions(section_id, display_order);

-- 4. FORMS DEFINITION
CREATE TABLE IF NOT EXISTS public.forms (
    id BIGSERIAL PRIMARY KEY,
    tenant_id INT NOT NULL,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    version INT DEFAULT 1,
    is_active BOOLEAN DEFAULT TRUE,
    is_published BOOLEAN DEFAULT FALSE,
    total_marks DECIMAL(10,2) DEFAULT 0,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_forms_tenant ON public.forms(tenant_id);

-- 5. FORM -> SECTION MAPPING
CREATE TABLE IF NOT EXISTS public.form_sections_mapping (
    id BIGSERIAL PRIMARY KEY,
    form_id BIGINT REFERENCES public.forms(id) ON DELETE CASCADE,
    section_id BIGINT REFERENCES public.form_sections(id) ON DELETE CASCADE,
    display_order INT NOT NULL DEFAULT 0,
    section_weightage DECIMAL(5,2) DEFAULT 0,
    section_max_marks DECIMAL(10,2) DEFAULT 0,
    
    UNIQUE(form_id, section_id),
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_form_sections_order ON public.form_sections_mapping(form_id, display_order);

-- 6. FORM -> BRANCH MAPPING
CREATE TABLE IF NOT EXISTS public.form_branches (
    id BIGSERIAL PRIMARY KEY,
    form_id BIGINT REFERENCES public.forms(id) ON DELETE CASCADE,
    branch_id INT NOT NULL, 
    
    UNIQUE(form_id, branch_id),
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);
CREATE INDEX IF NOT EXISTS idx_form_branches_lookup ON public.form_branches(form_id, branch_id);

-- 7. SUBMISSIONS
CREATE TABLE IF NOT EXISTS public.form_submissions (
    id BIGSERIAL PRIMARY KEY,
    tenant_id INT NOT NULL,
    form_id BIGINT REFERENCES public.forms(id) ON DELETE CASCADE,
    
    -- Actors and Subjects
    submitted_by INT, -- The User filling the form (e.g. Teacher)
    target_user_id INT, -- OPTIONAL: The User this form is about (e.g. Student). Null for self-submissions.
    
    status VARCHAR(50) DEFAULT 'DRAFT',
    total_score DECIMAL(10,2) DEFAULT 0,
  
    entry_date DATE,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_submissions_tenant_form ON public.form_submissions(tenant_id, form_id);
CREATE INDEX IF NOT EXISTS idx_submissions_user ON public.form_submissions(submitted_by);
CREATE INDEX IF NOT EXISTS idx_submissions_target ON public.form_submissions(target_user_id);

-- 8. SUBMISSION VALUES
CREATE TABLE IF NOT EXISTS public.submission_values (
    id BIGSERIAL PRIMARY KEY,
    submission_id BIGINT REFERENCES public.form_submissions(id) ON DELETE CASCADE,
    question_id BIGINT REFERENCES public.form_questions(id) ON DELETE RESTRICT,
    
    value_text TEXT,
    value_number DECIMAL,
    value_date TIMESTAMP,
    
    score_awarded DECIMAL(10,2) DEFAULT 0,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

CREATE INDEX IF NOT EXISTS idx_values_submission ON public.submission_values(submission_id);
CREATE INDEX IF NOT EXISTS idx_values_question ON public.submission_values(question_id); 

-- 9. AUDIT
CREATE TABLE IF NOT EXISTS public.form_versions (
    id BIGSERIAL PRIMARY KEY,
    form_id BIGINT REFERENCES public.forms(id) ON DELETE CASCADE,
    form_snapshot JSONB,
    
    created_on TIMESTAMP DEFAULT NOW(),
    created_by INT NOT NULL,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

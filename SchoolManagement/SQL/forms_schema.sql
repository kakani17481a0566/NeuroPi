-- Forms Module Schema - PostgreSQL (Finalized v8)

-- 1. Forms Table
CREATE TABLE forms (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    version INT DEFAULT 1,
    is_active BOOLEAN DEFAULT TRUE,
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE,
    UNIQUE(name, version, tenant_id)
);

-- 2. Form Sections
CREATE TABLE form_sections (
    id SERIAL PRIMARY KEY,
    form_id INT NOT NULL REFERENCES forms(id),
    name VARCHAR(255) NOT NULL,
    order_no INT NOT NULL DEFAULT 0,
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 3. Form Fields
CREATE TABLE form_fields (
    id SERIAL PRIMARY KEY,
    form_id INT NOT NULL REFERENCES forms(id),
    section_id INT REFERENCES form_sections(id),
    field_key VARCHAR(100) NOT NULL, -- Internal Identifier (e.g. 'q1_feedback')
    label VARCHAR(255) NOT NULL,     -- User Facing Label (e.g. 'What is your feedback?')
    field_type_id INT references masters(id), -- Linked to MMaster (MasterType: 'FormFieldType'. Ex Values: 'Text', 'Number', 'Radio', 'Checkbox', 'Select')
    is_required BOOLEAN DEFAULT FALSE,
    order_no INT NOT NULL DEFAULT 0,
    max_score DECIMAL(10, 2) DEFAULT 0,
    configurations JSONB, -- { "options": [], "validation": {"min": 1, "max": 10}, "logic": {} }
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 4. Form Field Options
-- Used for 'Select', 'Radio', 'Checkbox' types to store predefined choices.
CREATE TABLE form_field_options (
    id SERIAL PRIMARY KEY,
    field_id INT NOT NULL REFERENCES form_fields(id),
    label VARCHAR(255) NOT NULL, -- Display Text (e.g. 'Excellent')
    value VARCHAR(255) NOT NULL, -- Stored Value (e.g. '5' or 'excellent')
    order_no INT DEFAULT 0,
    is_default BOOLEAN DEFAULT FALSE,
    tenant_id INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 5. Form Assignments
CREATE TABLE form_assignments (
    id SERIAL PRIMARY KEY,
    form_id INT NOT NULL REFERENCES forms(id),
    branch_id INT REFERENCES branch(id),
    course_id INT REFERENCES course(id),
    is_active BOOLEAN DEFAULT TRUE,
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 6. Form Submissions
-- Removed form_id as requested (Found via assignment_id).
CREATE TABLE form_submissions (
    id SERIAL PRIMARY KEY,
    assignment_id INT NOT NULL REFERENCES form_assignments(id),
    staff_id INT,
    time_table_id INT REFERENCES time_table(id), -- Context: Specific Class/Session
    status VARCHAR(50) DEFAULT 'DRAFT', -- Values: 'DRAFT', 'SUBMITTED', 'APPROVED', 'REJECTED'
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 7. Form Values
CREATE TABLE form_values (
    id SERIAL PRIMARY KEY,
    submission_id INT NOT NULL REFERENCES form_submissions(id),
    field_id INT NOT NULL REFERENCES form_fields(id),
    time_table_id INT REFERENCES time_table(id),
    value TEXT, -- The actual answer provided by user
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 8. Form Scores
CREATE TABLE form_scores (
    id SERIAL PRIMARY KEY,
    submission_id INT NOT NULL REFERENCES form_submissions(id),
    section_id INT REFERENCES form_sections(id),
    score DECIMAL(10, 2), -- Calculated or Manual Score
    max_score DECIMAL(10, 2),
    remarks TEXT,
    grade_id INT REFERENCES grade(id), -- Linked to Grade table (e.g. 'A', 'B', 'Fail')
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 9. Form Attachments
CREATE TABLE form_attachments (
    id SERIAL PRIMARY KEY,
    submission_id INT NOT NULL REFERENCES form_submissions(id),
    field_id INT REFERENCES form_fields(id),
    file_name VARCHAR(255),
    file_path TEXT, -- URL or Storage Path
    file_type VARCHAR(50), -- e.g. 'pdf', 'jpg'
    file_size INT,
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

-- 10. Form Audit
CREATE TABLE form_audit (
    id SERIAL PRIMARY KEY,
    submission_id INT NOT NULL REFERENCES form_submissions(id),
    action VARCHAR(100), -- Values: 'CREATED', 'UPDATED', 'DELETED'
    remarks TEXT,
    tenant_id INT,
    created_on TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    updated_on TIMESTAMP,
    updated_by INT,
    is_deleted BOOLEAN DEFAULT FALSE
);

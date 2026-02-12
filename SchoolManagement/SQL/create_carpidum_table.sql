CREATE TABLE nutrition.carpidum (
    id SERIAL PRIMARY KEY,
    
    student_id INT NOT NULL,
    
    parent_type VARCHAR(20) NOT NULL
        CHECK (parent_type IN (
            'FATHER',
            'MOTHER',
            'GRANDFATHER',
            'GRANDMOTHER',
            'GUARDIAN',
            'OTHERS'
        )),
    
    guardian_name VARCHAR(100),
    
    qr_code VARCHAR(255) NOT NULL,
    
    email VARCHAR(255),
    
    mobile_number VARCHAR(20),
    
    tenant_id INT NOT NULL,

    student_name VARCHAR(200),
    gender VARCHAR(20),

    is_deleted BOOLEAN DEFAULT FALSE,
    
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    created_by INT,
    
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_by INT,
    
    -- QR must be unique
    CONSTRAINT uq_qr_code UNIQUE (qr_code),
    
    -- Prevent duplicate same role+name for one student
    CONSTRAINT uq_student_role_guardian
        UNIQUE (student_id, parent_type, guardian_name)
    
    -- Foreign key to students table (assuming students is in public or same schema? 
    -- If students is in public, use public.students)
    -- CONSTRAINT fk_carpidum_student
    --    FOREIGN KEY (student_id)
    --    REFERENCES public.students(id)
    --    ON DELETE CASCADE
);

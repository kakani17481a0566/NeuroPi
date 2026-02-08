-- =====================================================
-- TEMPORARY TABLE FOR BULK IMPORT
-- Purpose: Import students, parents, and contacts via CSV/Excel
-- =====================================================

DROP TABLE IF EXISTS temp_bulk_import;

CREATE TABLE temp_bulk_import (
    -- Row identifier
    row_id INT AUTO_INCREMENT PRIMARY KEY,
    
    -- Student Information (reg_number will be auto-generated)
    student_first_name VARCHAR(100),
    student_last_name VARCHAR(100),
    student_dob DATE,
    student_gender VARCHAR(10),
    student_blood_group VARCHAR(10),
    student_address TEXT,
    
    -- Course and Branch (IDs will be auto-fetched from names)
    course_name VARCHAR(100),
    branch_name VARCHAR(100),
    
    -- Father Information
    father_first_name VARCHAR(100),
    father_last_name VARCHAR(100),
    father_email VARCHAR(100),
    father_phone VARCHAR(20),
    father_occupation VARCHAR(100),
    father_address TEXT,
    
    -- Mother Information
    mother_first_name VARCHAR(100),
    mother_last_name VARCHAR(100),
    mother_email VARCHAR(100),
    mother_phone VARCHAR(20),
    mother_occupation VARCHAR(100),
    mother_address TEXT,
    
    -- Processing Status
    import_status VARCHAR(20) DEFAULT 'PENDING', -- PENDING, PROCESSED, ERROR
    error_message TEXT,
    processed_date DATETIME,
    
    -- Audit
    created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
    created_by VARCHAR(100)
);

-- =====================================================
-- FUNCTION TO GENERATE STUDENT REGISTRATION NUMBER
-- =====================================================

DELIMITER $$

DROP FUNCTION IF EXISTS fn_generate_student_reg_number$$

CREATE FUNCTION fn_generate_student_reg_number(p_branch_id INT, p_course_id INT)
RETURNS VARCHAR(50)
DETERMINISTIC
BEGIN
    DECLARE v_reg_number VARCHAR(50);
    DECLARE v_year VARCHAR(4);
    DECLARE v_sequence INT;
    DECLARE v_branch_code VARCHAR(10);
    DECLARE v_course_code VARCHAR(10);
    
    -- Get current year
    SET v_year = YEAR(CURDATE());
    
    -- Use branch ID directly (padded to 2 digits)
    SET v_branch_code = LPAD(p_branch_id, 2, '0');
    
    -- Use course ID directly (padded to 2 digits)
    SET v_course_code = LPAD(p_course_id, 2, '0');
    
    -- Get next sequence number for this year/branch/course combination
    SELECT COALESCE(MAX(CAST(SUBSTRING_INDEX(reg_number, '-', -1) AS UNSIGNED)), 0) + 1
    INTO v_sequence
    FROM students
    WHERE reg_number LIKE CONCAT(v_year, '-', v_branch_code, '-', v_course_code, '-%');
    
    -- Format: YYYY-BRANCH-COURSE-SEQUENCE (e.g., 2026-13-01-0001 for branch 13, course 1)
    SET v_reg_number = CONCAT(v_year, '-', v_branch_code, '-', v_course_code, '-', LPAD(v_sequence, 4, '0'));
    
    RETURN v_reg_number;
END$$

-- =====================================================
-- STORED PROCEDURE TO PROCESS TEMP TABLE
-- =====================================================

DROP PROCEDURE IF EXISTS sp_process_bulk_import$$

CREATE PROCEDURE sp_process_bulk_import()
BEGIN
    DECLARE done INT DEFAULT FALSE;
    DECLARE v_row_id INT;
    DECLARE v_student_user_id INT;
    DECLARE v_father_user_id INT;
    DECLARE v_mother_user_id INT;
    DECLARE v_student_id INT;
    DECLARE v_father_id INT;
    DECLARE v_mother_id INT;
    DECLARE v_error_msg TEXT;
    DECLARE v_student_reg_number VARCHAR(50);
    DECLARE v_course_id INT;
    DECLARE v_branch_id INT;
    DECLARE v_father_relationship_id INT;
    DECLARE v_mother_relationship_id INT;
    
    -- Cursor for pending records
    DECLARE cur CURSOR FOR 
        SELECT row_id FROM temp_bulk_import WHERE import_status = 'PENDING';
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = TRUE;
    
    OPEN cur;
    
    read_loop: LOOP
        FETCH cur INTO v_row_id;
        IF done THEN
            LEAVE read_loop;
        END IF;
        
        BEGIN
            DECLARE EXIT HANDLER FOR SQLEXCEPTION
            BEGIN
                GET DIAGNOSTICS CONDITION 1 v_error_msg = MESSAGE_TEXT;
                UPDATE temp_bulk_import 
                SET import_status = 'ERROR', 
                    error_message = v_error_msg,
                    processed_date = NOW()
                WHERE row_id = v_row_id;
            END;
            
            -- Start processing each row
            SET v_student_user_id = NULL;
            SET v_father_user_id = NULL;
            SET v_mother_user_id = NULL;
            SET v_student_id = NULL;
            SET v_father_id = NULL;
            SET v_mother_id = NULL;
            SET v_student_reg_number = NULL;
            SET v_course_id = NULL;
            SET v_branch_id = NULL;
            SET v_father_relationship_id = NULL;
            SET v_mother_relationship_id = NULL;
            
            -- 1. Lookup IDs from Names
            -- Get course_id from course_name
            SELECT c.id INTO v_course_id
            FROM course c
            WHERE c.name = (SELECT course_name FROM temp_bulk_import WHERE row_id = v_row_id)
              AND c.is_deleted = 0
            LIMIT 1;
            
            -- Get branch_id from branch_name
            SELECT b.id INTO v_branch_id
            FROM branch b
            WHERE b.name = (SELECT branch_name FROM temp_bulk_import WHERE row_id = v_row_id)
              AND b.is_deleted = 0
            LIMIT 1;
            
            -- Get father relationship_id (FATHER)
            SELECT m.id INTO v_father_relationship_id
            FROM masters m
            WHERE m.name = 'FATHER'
              AND m.masters_type_id = 43
            LIMIT 1;
            
            -- Get mother relationship_id (MOTHER)
            SELECT m.id INTO v_mother_relationship_id
            FROM masters m
            WHERE m.name = 'MOTHER'
              AND m.masters_type_id = 43
            LIMIT 1;
            
            -- 2. Generate Student Registration Number
            SELECT fn_generate_student_reg_number(v_branch_id, v_course_id) INTO v_student_reg_number;
            
            -- 3. Create Student User Account
            INSERT INTO users (username, password, email, is_deleted, created_at, created_by)
            SELECT 
                v_student_reg_number,
                'default123', -- Default password, should be changed on first login
                CONCAT(v_student_reg_number, '@school.com'),
                0,
                NOW(),
                t.created_by
            FROM temp_bulk_import t
            WHERE t.row_id = v_row_id;
            
            SET v_student_user_id = LAST_INSERT_ID();
            
            -- Assign USER role to student (role_id = 7)
            INSERT INTO user_roles (user_id, role_id, created_at, created_by)
            SELECT v_student_user_id, 7, NOW(), t.created_by
            FROM temp_bulk_import t
            WHERE t.row_id = v_row_id;
            
            -- 4. Create Student Record
            INSERT INTO students (user_id, reg_number, first_name, last_name, dob, gender, 
                                  blood_group, address, course_id, branch_id, 
                                  is_deleted, created_at, created_by)
            SELECT 
                v_student_user_id,
                v_student_reg_number,
                t.student_first_name,
                t.student_last_name,
                t.student_dob,
                t.student_gender,
                t.student_blood_group,
                t.student_address,
                v_course_id,
                v_branch_id,
                0,
                NOW(),
                t.created_by
            FROM temp_bulk_import t
            WHERE t.row_id = v_row_id;
            
            SET v_student_id = LAST_INSERT_ID();
            
            -- 5. Handle Father (Check for existing father by email)
            IF (SELECT father_email FROM temp_bulk_import WHERE row_id = v_row_id) IS NOT NULL THEN
                
                -- Check if father user already exists by email
                SELECT u.id INTO v_father_user_id
                FROM users u
                WHERE u.email = (SELECT father_email FROM temp_bulk_import WHERE row_id = v_row_id)
                  AND u.is_deleted = 0
                LIMIT 1;
                
                -- If father user doesn't exist, create new user account
                IF v_father_user_id IS NULL THEN
                    INSERT INTO users (username, password, email, is_deleted, created_at, created_by)
                    SELECT 
                        t.father_email,
                        'parent123', -- Default password
                        t.father_email,
                        0,
                        NOW(),
                        t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                    
                    SET v_father_user_id = LAST_INSERT_ID();
                    
                    -- Assign PARENT role (role_id = 5)
                    INSERT INTO user_roles (user_id, role_id, created_at, created_by)
                    SELECT v_father_user_id, 5, NOW(), t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                END IF;
                
                -- Check if father record already exists
                SELECT p.id INTO v_father_id
                FROM parents p
                WHERE p.user_id = v_father_user_id
                  AND p.is_deleted = 0
                LIMIT 1;
                
                -- If father record doesn't exist, create it
                IF v_father_id IS NULL THEN
                    INSERT INTO parents (user_id, first_name, last_name, email, phone, 
                                         occupation, address, is_deleted, created_at, created_by)
                    SELECT 
                        v_father_user_id,
                        t.father_first_name,
                        t.father_last_name,
                        t.father_email,
                        t.father_phone,
                        t.father_occupation,
                        t.father_address,
                        0,
                        NOW(),
                        t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                    
                    SET v_father_id = LAST_INSERT_ID();
                END IF;
                
                -- Link Father to Student (allow multiple students per father)
                -- Check if this father-student link already exists
                IF NOT EXISTS (
                    SELECT 1 FROM parent_student 
                    WHERE parent_id = v_father_id 
                      AND student_id = v_student_id
                      AND is_deleted = 0
                ) THEN
                    INSERT INTO parent_student (parent_id, student_id, is_deleted, created_at, created_by)
                    SELECT v_father_id, v_student_id, 0, NOW(), t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                END IF;
                
                -- Create Contact Record for Father
                INSERT INTO contact (student_id, name, relationship_id, phone, email, 
                                     is_primary, is_deleted, created_at, created_by)
                SELECT 
                    v_student_id,
                    CONCAT(t.father_first_name, ' ', t.father_last_name),
                    v_father_relationship_id,
                    t.father_phone,
                    t.father_email,
                    1, -- Primary contact
                    0,
                    NOW(),
                    t.created_by
                FROM temp_bulk_import t
                WHERE t.row_id = v_row_id;
            END IF;
            
            -- 6. Handle Mother (Check for existing mother by email)
            IF (SELECT mother_email FROM temp_bulk_import WHERE row_id = v_row_id) IS NOT NULL THEN
                
                -- Check if mother user already exists by email
                SELECT u.id INTO v_mother_user_id
                FROM users u
                WHERE u.email = (SELECT mother_email FROM temp_bulk_import WHERE row_id = v_row_id)
                  AND u.is_deleted = 0
                LIMIT 1;
                
                -- If mother user doesn't exist, create new user account
                IF v_mother_user_id IS NULL THEN
                    INSERT INTO users (username, password, email, is_deleted, created_at, created_by)
                    SELECT 
                        t.mother_email,
                        'parent123',
                        t.mother_email,
                        0,
                        NOW(),
                        t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                    
                    SET v_mother_user_id = LAST_INSERT_ID();
                    
                    -- Assign PARENT role
                    INSERT INTO user_roles (user_id, role_id, created_at, created_by)
                    SELECT v_mother_user_id, 5, NOW(), t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                END IF;
                
                -- Check if mother record already exists
                SELECT p.id INTO v_mother_id
                FROM parents p
                WHERE p.user_id = v_mother_user_id
                  AND p.is_deleted = 0
                LIMIT 1;
                
                -- If mother record doesn't exist, create it
                IF v_mother_id IS NULL THEN
                    INSERT INTO parents (user_id, first_name, last_name, email, phone, 
                                         occupation, address, is_deleted, created_at, created_by)
                    SELECT 
                        v_mother_user_id,
                        t.mother_first_name,
                        t.mother_last_name,
                        t.mother_email,
                        t.mother_phone,
                        t.mother_occupation,
                        t.mother_address,
                        0,
                        NOW(),
                        t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                    
                    SET v_mother_id = LAST_INSERT_ID();
                END IF;
                
                -- Link Mother to Student (allow multiple students per mother)
                -- Check if this mother-student link already exists
                IF NOT EXISTS (
                    SELECT 1 FROM parent_student 
                    WHERE parent_id = v_mother_id 
                      AND student_id = v_student_id
                      AND is_deleted = 0
                ) THEN
                    INSERT INTO parent_student (parent_id, student_id, is_deleted, created_at, created_by)
                    SELECT v_mother_id, v_student_id, 0, NOW(), t.created_by
                    FROM temp_bulk_import t
                    WHERE t.row_id = v_row_id;
                END IF;
                
                -- Create Contact Record for Mother
                INSERT INTO contact (student_id, name, relationship_id, phone, email, 
                                     is_primary, is_deleted, created_at, created_by)
                SELECT 
                    v_student_id,
                    CONCAT(t.mother_first_name, ' ', t.mother_last_name),
                    v_mother_relationship_id,
                    t.mother_phone,
                    t.mother_email,
                    0,
                    0,
                    NOW(),
                    t.created_by
                FROM temp_bulk_import t
                WHERE t.row_id = v_row_id;
            END IF;
            
            -- Mark as processed
            UPDATE temp_bulk_import 
            SET import_status = 'PROCESSED',
                processed_date = NOW()
            WHERE row_id = v_row_id;
            
        END;
    END LOOP;
    
    CLOSE cur;
    
    -- Return summary
    SELECT 
        import_status,
        COUNT(*) as count
    FROM temp_bulk_import
    GROUP BY import_status;
    
END$$

DELIMITER ;

-- =====================================================
-- SAMPLE CSV TEMPLATE STRUCTURE
-- =====================================================
-- Copy this header row to your CSV/Excel file:
/*
student_first_name,student_last_name,student_dob,student_gender,student_blood_group,student_address,course_name,branch_name,father_first_name,father_last_name,father_email,father_phone,father_occupation,father_address,mother_first_name,mother_last_name,mother_email,mother_phone,mother_occupation,mother_address,created_by

Sample Data Row:
John,Doe,2015-05-15,Male,O+,123 Main St,Nursery,Kondapur,Robert,Doe,robert.doe@email.com,1234567890,Engineer,123 Main St,Jane,Doe,jane.doe@email.com,0987654321,Teacher,123 Main St,admin

NOTE: 
- student_reg_number is AUTO-GENERATED in format: YYYY-BRANCH-COURSE-SEQUENCE (e.g., 2026-13-01-0001)
- All IDs (course_id, branch_id, relationship_ids) are AUTO-FETCHED from the names you provide
- Father relationship_id is automatically set to FATHER, Mother relationship_id is automatically set to MOTHER
*/

-- =====================================================
-- USAGE INSTRUCTIONS
-- =====================================================
/*
1. Load data into temp_bulk_import table using:
   - MySQL LOAD DATA INFILE
   - phpMyAdmin import
   - MySQL Workbench import wizard
   - Or any other bulk import tool

2. Example LOAD DATA command:
   LOAD DATA LOCAL INFILE 'C:/path/to/your/students.csv'
   INTO TABLE temp_bulk_import
   FIELDS TERMINATED BY ','
   ENCLOSED BY '"'
   LINES TERMINATED BY '\n'
   IGNORE 1 ROWS
   (student_first_name, student_last_name, student_dob, student_gender, 
    student_blood_group, student_address, course_name, branch_name, 
    father_first_name, father_last_name, father_email, father_phone, 
    father_occupation, father_address, mother_first_name, mother_last_name,
    mother_email, mother_phone, mother_occupation, mother_address,
    created_by);

3. Verify imported data:
   SELECT * FROM temp_bulk_import WHERE import_status = 'PENDING';

4. Process the import:
   CALL sp_process_bulk_import();

5. Check results:
   SELECT * FROM temp_bulk_import WHERE import_status = 'ERROR';
   
6. Clean up after successful import:
   DELETE FROM temp_bulk_import WHERE import_status = 'PROCESSED';
*/

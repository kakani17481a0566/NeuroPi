/*
 * Fixed Query: insert_family_passes.sql
 * Corrections:
 * 1. Added schema 'nutrition.carpidum'
 * 2. Added 'tenant_id' (Required NOT NULL column)
 * 3. Cast UUID to VARCHAR for qr_code
 * 4. Added DISTINCT ON (mobile_number, parent_type) to ensure 1 pass per parent (no duplicates for multiple kids)
 */

INSERT INTO nutrition.carpidum
(student_id, parent_type, guardian_name, qr_code, email, mobile_number, tenant_id, created_by)
SELECT DISTINCT ON (d.mobile_number, d.parent_type)
    d.student_id,
    d.parent_type,
    d.guardian_name,
    d.qr_code,
    d.email,
    d.mobile_number,
    d.tenant_id,
    1 AS created_by
FROM (
    -- FATHER
    SELECT
        s.id                        AS student_id,
        'FATHER'                    AS parent_type,
        NULL::varchar               AS guardian_name,
        gen_random_uuid()::varchar  AS qr_code,
        u.email                     AS email,
        u.mobile_number             AS mobile_number,
        s.tenant_id                 AS tenant_id
    FROM parent_student ps
    JOIN students s ON s.id = ps.student_id
    JOIN parents p ON p.id = ps.parent_id
    JOIN users u ON u.user_id = p.user_id
    WHERE ps.is_deleted = FALSE
      AND s.is_deleted = FALSE
      AND p.is_deleted = FALSE
      -- AND s.branch_id = 1
      
    UNION ALL
    
    -- MOTHER
    SELECT
        s.id                        AS student_id,
        'MOTHER'                    AS parent_type,
        NULL::varchar               AS guardian_name,
        gen_random_uuid()::varchar  AS qr_code,
        u.email                     AS email,
        u.mobile_number             AS mobile_number,
        s.tenant_id                 AS tenant_id
    FROM parent_student ps
    JOIN students s ON s.id = ps.student_id
    JOIN parents p ON p.id = ps.parent_id
    JOIN users u ON u.user_id = p.user_id
    WHERE ps.is_deleted = FALSE
      AND s.is_deleted = FALSE
      AND p.is_deleted = FALSE
      -- AND s.branch_id = 2

) AS d
WHERE NOT EXISTS (
    SELECT 1
    FROM nutrition.carpidum c
    WHERE c.mobile_number = d.mobile_number
      AND c.parent_type = d.parent_type
      AND c.is_deleted = FALSE
);

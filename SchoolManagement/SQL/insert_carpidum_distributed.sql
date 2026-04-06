INSERT INTO nutrition.carpidum
(student_id, parent_type, guardian_name, qr_code, email, mobile_number, tenant_id, created_by)
WITH FamilyGroups AS (
    -- Get unique student-parent mobile combinations
    SELECT DISTINCT
        s.id AS student_id,
        u.mobile_number,
        u.email,
        s.tenant_id
    FROM parent_student ps
    JOIN students s ON s.id = ps.student_id
    JOIN parents p ON p.id = ps.parent_id
    JOIN users u ON u.user_id = p.user_id
    WHERE ps.is_deleted = FALSE
      AND s.is_deleted = FALSE
      AND p.is_deleted = FALSE
),
RankedStudents AS (
    -- Rank students within each family (grouped by mobile number)
    SELECT 
        fg.*,
        DENSE_RANK() OVER (PARTITION BY fg.mobile_number ORDER BY fg.student_id) as student_rank,
        COUNT(fg.student_id) OVER (PARTITION BY fg.mobile_number) as family_size
    FROM FamilyGroups fg
)
SELECT DISTINCT ON (rs.student_id, pt.parent_type) -- Added DISTINCT ON to prevent duplicates within the batch if student belongs to multiple families
    rs.student_id,
    pt.parent_type,
    NULL::varchar               AS guardian_name,
    gen_random_uuid()::varchar  AS qr_code,
    rs.email,
    rs.mobile_number,
    rs.tenant_id,
    1 AS created_by
FROM RankedStudents rs
CROSS JOIN (VALUES ('FATHER'), ('MOTHER')) AS pt(parent_type)
WHERE 
    -- LOGIC:
    -- 1. If Single Child (Family Size = 1):
    --    Generate BOTH 'FATHER' and 'MOTHER' passes.
    (rs.family_size = 1)
    
    OR
    
    -- 2. If Multiple Children (Twins, Triplets, etc.):
    --    Distribute passes to avoid redundancy.
    (rs.family_size > 1 AND (
        -- Odd numbered students (1st, 3rd...) get 'FATHER' pass
        (pt.parent_type = 'FATHER' AND rs.student_rank % 2 != 0)
        OR 
        -- Even numbered students (2nd, 4th...) get 'MOTHER' pass
        (pt.parent_type = 'MOTHER' AND rs.student_rank % 2 = 0)
    ))

-- Prevent duplicates against EXISTING database records
ORDER BY rs.student_id, pt.parent_type, rs.mobile_number -- Consistent ordering for DISTINCT ON
)
WHERE NOT EXISTS (
    SELECT 1 
    FROM nutrition.carpidum c
    WHERE c.student_id = rs.student_id
      AND c.parent_type = pt.parent_type
      AND c.is_deleted = FALSE
);

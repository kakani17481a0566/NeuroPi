-- SQL Script to get User Details (Username, Email, Role, Branch, Course)
-- This script joins users, roles, employees, students, and parents tables.

SELECT DISTINCT 
    u.user_id AS "User ID",
    u.username AS "Username",
    u.email AS "Email",
    r.role_id AS "Role ID",
    r.name AS "Role",
    
    -- Branch Name: Priority -> Employee Branch -> Child's Branch
    COALESCE(b_emp.id, b_child.id) AS "Branch ID",
    COALESCE(b_emp.name, b_child.name) AS "Branch",
    
    -- Course Name: Priority -> Child's Course
    COALESCE(c_child.id) AS "Course ID",
    COALESCE(c_child.name) AS "Course",

    -- Additional context to identify source (Optional)
    CASE 
        WHEN emp.id IS NOT NULL THEN 'Employee'
        -- Student direct login does not exist
        WHEN p.id IS NOT NULL THEN 'Parent'
        ELSE 'User'
    END AS "User Type"

FROM users u

-- Join Roles
LEFT JOIN user_roles ur ON u.user_id = ur.user_id AND ur.is_deleted = false
LEFT JOIN roles r ON ur.role_id = r.role_id

-- Join Employee Details
LEFT JOIN employee emp ON u.user_id = emp.user_id
LEFT JOIN branch b_emp ON emp.branch_id = b_emp.id

-- Student Direct Join Removed (Students are not Users)

-- Join Parent Details
LEFT JOIN parents p ON u.user_id = p.user_id AND p.is_deleted = false
LEFT JOIN parent_student ps ON p.id = ps.parent_id AND ps.is_deleted = false
LEFT JOIN students child ON ps.student_id = child.id AND child.is_deleted = false
LEFT JOIN branch b_child ON child.branch_id = b_child.id
LEFT JOIN course c_child ON child.course_id = c_child.id

WHERE u.is_deleted = false
AND u.email NOT ILIKE '%test%'
AND u.email NOT ILIKE '%string%'
AND u.username NOT ILIKE 'test%'

-- Order by Username
ORDER BY r.name, u.username;
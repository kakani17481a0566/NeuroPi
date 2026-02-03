-- SQL Script to get User Details (Username, Email, Role, Branch, Course)
-- This script joins users, roles, employees, students, and parents tables.

SELECT 
    u.username AS "Username",
    u.email AS "Email",
    r.name AS "Role",
    
    -- Branch Name: Priority -> Employee Branch -> Student Branch -> Child's Branch
    COALESCE(b_emp.name, b_stu.name, b_child.name) AS "Branch",
    
    -- Course Name: Priority -> Student Course -> Child's Course
    COALESCE(c_stu.name, c_child.name) AS "Course",

    -- Additional context to identify source (Optional)
    CASE 
        WHEN emp.id IS NOT NULL THEN 'Employee'
        WHEN stu.id IS NOT NULL THEN 'Student'
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

-- Join Student Details (Assuming Username matches Reg Number for Students)
LEFT JOIN students stu ON u.username = stu.reg_number AND stu.is_deleted = false
LEFT JOIN branch b_stu ON stu.branch_id = b_stu.id
LEFT JOIN course c_stu ON stu.course_id = c_stu.id

-- Join Parent Details
LEFT JOIN parents p ON u.user_id = p.user_id AND p.is_deleted = false
LEFT JOIN parent_student ps ON p.id = ps.parent_id AND ps.is_deleted = false
LEFT JOIN students child ON ps.student_id = child.id AND child.is_deleted = false
LEFT JOIN branch b_child ON child.branch_id = b_child.id
LEFT JOIN course c_child ON child.course_id = c_child.id

WHERE u.is_deleted = false

-- Order by Username
ORDER BY u.username;

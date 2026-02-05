-- Check Roles
SELECT role_id, name, tenant_id FROM roles;

-- Check Main Menus
SELECT id, name, title, type, path, icon, tenant_id, transkey FROM main_menu;

-- Check Child Menus
SELECT id, main_menu_id, name, title, type, path, icon, tenant_id, transkey FROM menu;

-- Check Permissions Table (must align with Menu IDs)
SELECT permission_id, name, description, tenant_id FROM permissions;

-- Check Role Permissions (Link between Roles and Menus)
-- Note: Adjust join if role_permissions links to 'menu' or 'main_menu'. 
-- Schema said role_permissions has 'menu_id'. Assuming 'menu_id' refers to 'menu.id'.
SELECT 
    rp.role_permission_id,
    r.name as role_name,
    m.title as menu_title,
    m.path as menu_path,
    rp.permissions,
    rp.tenant_id
FROM role_permissions rp
JOIN roles r ON rp.role_id = r.role_id
JOIN menu m ON rp.menu_id = m.id;

-- Check for specific missing items
SELECT * FROM menu WHERE title IN ('Mark Attendance', 'Student Performance Tracker');

-- Script to add Carpidum Admin Dashboard to Dashboards Menu
-- This adds the dashboard at /dashboards/CarpidumAdmin

DO $$
DECLARE
    -- Change Tenant ID if necessary
    t_id INT := 1; 
    mm_dashboards INT;
    new_menu_id INT;
BEGIN
    -- 1. Find the 'dashboards' Main Menu ID (lowercase)
    SELECT id INTO mm_dashboards 
    FROM main_menu 
    WHERE name = 'dashboards' AND tenant_id = t_id 
    LIMIT 1;

    IF mm_dashboards IS NULL THEN
        RAISE NOTICE 'Dashboards main menu not found.';
        RETURN;
    END IF;

    -- 2. Insert 'Carpidum Admin' into Menu
    -- Check if it already exists to avoid duplicates
    SELECT id INTO new_menu_id 
    FROM menu 
    WHERE name = 'dashboards.carpidumadmin' AND main_menu_id = mm_dashboards AND tenant_id = t_id
    LIMIT 1;

    IF new_menu_id IS NULL THEN
        INSERT INTO menu (
            main_menu_id, 
            name, 
            title, 
            transkey, 
            icon, 
            path, 
            type, 
            is_deleted, 
            tenant_id
        )
        VALUES (
            mm_dashboards, 
            'dashboards.carpidumadmin', 
            'Carpidum Dashboard', 
            'nav.dashboards.carpidumadmin', 
            'QrCodeIcon', 
            '/dashboards/CarpidumAdmin', 
            'item', 
            false, 
            t_id
        )
        RETURNING id INTO new_menu_id;
        
        RAISE NOTICE 'Inserted new menu item ID: %', new_menu_id;
    ELSE
        RAISE NOTICE 'Menu item already exists ID: %', new_menu_id;
    END IF;

    -- 3. Assign Permission to 'admin' Role
    
    INSERT INTO role_permissions (role_id, menu_id, is_deleted, permissions, tenant_id)
    SELECT r.role_id, new_menu_id, false, '00000', t_id
    FROM roles r
    WHERE LOWER(r.name) = 'admin'
    ON CONFLICT DO NOTHING;
    
    RAISE NOTICE 'Permissions assigned to admin role.';

END $$;

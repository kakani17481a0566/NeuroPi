-- Script to add Carpidum Passes to Parent Sidebar

DO $$
DECLARE
    -- Change Tenant ID if necessary
    t_id INT := 1; 
    mm_parent INT;
    new_menu_id INT;
BEGIN
    -- 1. Find the 'Parent Dashboards' Main Menu ID
    SELECT id INTO mm_parent 
    FROM main_menu 
    WHERE name = 'parentDashboards' AND tenant_id = t_id 
    LIMIT 1;

    IF mm_parent IS NULL THEN
        RAISE NOTICE 'Parent Dashboards main menu not found.';
        RETURN;
    END IF;

    -- 2. Insert 'Carpidum Passes' into Menu
    -- Check if it already exists to avoid duplicates
    SELECT id INTO new_menu_id 
    FROM menu 
    WHERE name = 'parent.CarpidumPasses' AND main_menu_id = mm_parent AND tenant_id = t_id
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
            mm_parent, 
            'parent.CarpidumPasses', 
            'Carpidum Passes', 
            'nav.parent.CarpidumPasses', 
            'QrCodeIcon', 
            '/parent/CarpidumPasses', 
            'item', 
            false, 
            t_id
        )
        RETURNING id INTO new_menu_id;
        
        RAISE NOTICE 'Inserted new menu item ID: %', new_menu_id;
    ELSE
        RAISE NOTICE 'Menu item already exists ID: %', new_menu_id;
    END IF;

    -- 3. Assign Permission to 'parent' Role
    -- Assuming role_id for 'parent' is known or needs lookup. 
    -- The seed script used a helper function, here we do raw Insert.
    
    INSERT INTO role_permissions (role_id, menu_id, is_deleted, permissions, tenant_id)
    SELECT r.role_id, new_menu_id, false, '00000', t_id
    FROM roles r
    WHERE LOWER(r.name) = 'parent'
    ON CONFLICT DO NOTHING;
    
    -- Also assign to ADMIN for visibility
    INSERT INTO role_permissions (role_id, menu_id, is_deleted, permissions, tenant_id)
    SELECT r.role_id, new_menu_id, false, '00000', t_id
    FROM roles r
    WHERE LOWER(r.name) = 'admin'
    ON CONFLICT DO NOTHING;

    RAISE NOTICE 'Permissions assigned.';

END $$;

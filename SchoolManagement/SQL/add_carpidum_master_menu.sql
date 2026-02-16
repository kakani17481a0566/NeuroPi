-- Script to add Carpidum Master to Master Sidebar

DO $$
DECLARE
    -- Change Tenant ID if necessary
    t_id INT := 1; 
    mm_master INT;
    new_menu_id INT;
BEGIN
    -- 1. Find the 'Master' Main Menu ID
    SELECT id INTO mm_master 
    FROM main_menu 
    WHERE name = 'Master' AND tenant_id = t_id 
    LIMIT 1;

    IF mm_master IS NULL THEN
        RAISE NOTICE 'Master main menu not found.';
        RETURN;
    END IF;

    -- 2. Insert 'Carpidum' into Menu
    -- Check if it already exists to avoid duplicates
    SELECT id INTO new_menu_id 
    FROM menu 
    WHERE name = 'master.carpidum' AND main_menu_id = mm_master AND tenant_id = t_id
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
            mm_master, 
            'master.carpidum', 
            'Carpidum Master', 
            'nav.master.carpidum', 
            'QrCodeIcon', 
            '/master/carpidum', 
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
    
    RAISE NOTICE 'Permissions assigned.';

END $$;

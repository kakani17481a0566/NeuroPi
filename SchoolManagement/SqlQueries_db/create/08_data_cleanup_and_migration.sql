-- 1. Backfill 'size' from 'height'
-- Previously, 'height' was used to store size data. We are moving it to the correct column.
UPDATE items
SET size = height
WHERE size IS NULL AND height > 0;

-- 2. Fix Value Typos
UPDATE items SET name = 'Hoodie' WHERE name = 'Hoddy';

-- 3. Example Variant Linking (Consolidating Hoodies)
-- Let's assume we want to make a new Master Item or use an existing one.
-- Case: We have ID 4 (Hoodie, Size 21) and ID 20 (Hoodie, Size 25).
-- We can make ID 4 the "Master" (generic) and link ID 20 to it, OR create a new master.

-- Strategy: Create a NEW Master "School Hoodie" and link both existing items to it.
DO $$
DECLARE
    new_parent_id integer;
BEGIN
    -- Only insert if "School Hoodie Master" doesn't exist
    IF NOT EXISTS (SELECT 1 FROM items WHERE name = 'School Hoodie Master') THEN
        
        -- Insert Master Item
        INSERT INTO items (
            name, category_id, tenant_id, description, item_code, 
            base_uom, is_batch_tracked, is_serialized, 
            created_on, created_by, is_deleted
        )
        VALUES (
            'School Hoodie Master', 
            (SELECT category_id FROM items WHERE id = 4), -- Copy category from existing
            1, 
            'Standard School Hoodie', 
            'HOODIE-M', 
            'EA', false, false, 
            NOW(), 1, false
        )
        RETURNING id INTO new_parent_id;

        -- Link existing variants to this new master
        -- Link ID 4 (Size 21)
        UPDATE items 
        SET parent_item_id = new_parent_id, 
            name = 'Hoodie - Size 21' 
        WHERE id = 4;

        -- Link ID 20 (Size 25)
        UPDATE items 
        SET parent_item_id = new_parent_id, 
            name = 'Hoodie - Size 25' 
        WHERE id = 20;

        RAISE NOTICE 'Created Master ID % and linked variants 4 and 20', new_parent_id;
    END IF;
END $$;

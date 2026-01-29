-- 1. Ensure all Items have Size (Backfill from Height if missing)
UPDATE items
SET size = height
WHERE size IS NULL AND height > 0;

-- 2. Backfill 'size' in 'inventory_transfers' table
-- Logic: If transfer size is NULL, but the Item has a size, copy it.
UPDATE inventory_transfers t
SET size = i.size
FROM items i
WHERE t.item_id = i.id 
  AND t.size IS NULL 
  AND i.size IS NOT NULL;

-- 3. Backfill 'size' in 'inventory_transfers' from Item Height (fallback)
-- If Item Size is still NULL but has Height, use that.
UPDATE inventory_transfers t
SET size = i.height
FROM items i
WHERE t.item_id = i.id 
  AND t.size IS NULL 
  AND i.size IS NULL
  AND i.height > 0;

-- 4. Clean up "Unknown" naming in legacy transfers (Optional but good for cleanliness)
-- If we wanted to, we could update names here, but usually names are dynamic in the view model.
-- This script focuses on the "proper size" request.

RAISE NOTICE 'Data cleanup for Size columns completed.';

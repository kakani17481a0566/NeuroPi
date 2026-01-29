-- 1. Check Items & Variants (with Category)
-- See if Parent/Child links are set and Sizes are populated
SELECT 
    i.id, 
    i.name, 
    c.name as category, 
    i.size, 
    i.parent_item_id, 
    p.name as parent_name,
    i.height as legacy_height_col
FROM items i
LEFT JOIN item_category c ON i.category_id = c.id
LEFT JOIN items p ON i.parent_item_id = p.id
ORDER BY i.id DESC;

-- 2. Check Stock Levels (Item Branch)
-- See unique quantities per branch and price data
SELECT 
    ib.id,
    b.name as branch_name,
    i.name as item_name,
    ib.item_quantity,
    ib.item_price,
    i.size
FROM item_branch ib
JOIN items i ON ib.item_id = i.id
JOIN branch b ON ib.branch_id = b.id
WHERE ib.is_deleted = false
ORDER BY i.name, b.name;

-- 3. Check Transfers (with Size and Supplier)
-- Verify if Size is being recorded in transfers
SELECT 
    t.id,
    t.transfer_type,
    i.name as item_name,
    t.size as transfer_size,
    t.quantity,
    fb.name as from_branch,
    tb.name as to_branch,
    s.name as supplier,
    t.status
FROM inventory_transfers t
JOIN items i ON t.item_id = i.id
LEFT JOIN branch fb ON t.from_branch_id = fb.id
JOIN branch tb ON t.to_branch_id = tb.id
LEFT JOIN supplier s ON t.supplier_id = s.id
ORDER BY t.created_on DESC;

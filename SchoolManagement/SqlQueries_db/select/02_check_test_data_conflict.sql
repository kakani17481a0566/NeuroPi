-- Check if the test items we plan to insert already exist
SELECT id, name, category_id, size, parent_item_id, is_active 
FROM items 
WHERE name IN (
    'School Shirt Master', 
    'School Shirt - Size 30', 
    'School Shirt - Size 32', 
    'School Shirt - Size 34',
    'Running Shoes Master',
    'Running Shoes - Size 6',
    'Running Shoes - Size 7',
    'Running Shoes - Size 8'
);

-- Check if they have stock in branches
SELECT ib.id, i.name, b.name as branch_name, ib.item_quantity 
FROM item_branch ib
JOIN items i ON ib.item_id = i.id
JOIN branch b ON ib.branch_id = b.id
WHERE i.name IN (
    'School Shirt Master', 
    'School Shirt - Size 30', 
    'School Shirt - Size 32', 
    'School Shirt - Size 34',
    'Running Shoes Master',
    'Running Shoes - Size 6',
    'Running Shoes - Size 7',
    'Running Shoes - Size 8'
);

-- 10_create_test_data.sql
-- Purpose: Add new Items (Parents & Variants) and populate initial stock in branches for testing.

BEGIN;

-- 1. Create Parent Item: "School Shirt Master"
INSERT INTO items (name, category_id, tenant_id, is_deleted, created_on, created_by, is_group, description)
VALUES ('School Shirt Master', (SELECT id FROM item_category WHERE name = 'Uniform' LIMIT 1), 1, false, NOW(), 1, true, 'Master item for School Shirts')
RETURNING id;

-- (Assume ID returned is X, captured in variable if script allows, but for manual SQL we use subqueries or hardcode logic)
-- For a robust script, we'll straight insert variants using subquery for parent_id

-- 2. Create Variants: Size 30, 32, 34
INSERT INTO items (name, category_id, tenant_id, size, parent_item_id, is_deleted, created_on, created_by, is_group)
VALUES 
('School Shirt - Size 30', (SELECT id FROM item_category WHERE name = 'Uniform' LIMIT 1), 1, 30, (SELECT id FROM items WHERE name = 'School Shirt Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false),
('School Shirt - Size 32', (SELECT id FROM item_category WHERE name = 'Uniform' LIMIT 1), 1, 32, (SELECT id FROM items WHERE name = 'School Shirt Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false),
('School Shirt - Size 34', (SELECT id FROM item_category WHERE name = 'Uniform' LIMIT 1), 1, 34, (SELECT id FROM items WHERE name = 'School Shirt Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false);


-- 3. Create Parent Item: "Running Shoes Master"
INSERT INTO items (name, category_id, tenant_id, is_deleted, created_on, created_by, is_group, description)
VALUES ('Running Shoes Master', (SELECT id FROM item_category WHERE name = 'Accessories' LIMIT 1), 1, false, NOW(), 1, true, 'Master item for Running Shoes');

-- 4. Create Variants: Size 6, 7, 8
INSERT INTO items (name, category_id, tenant_id, size, parent_item_id, is_deleted, created_on, created_by, is_group)
VALUES 
('Running Shoes - Size 6', (SELECT id FROM item_category WHERE name = 'Accessories' LIMIT 1), 1, 6, (SELECT id FROM items WHERE name = 'Running Shoes Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false),
('Running Shoes - Size 7', (SELECT id FROM item_category WHERE name = 'Accessories' LIMIT 1), 1, 7, (SELECT id FROM items WHERE name = 'Running Shoes Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false),
('Running Shoes - Size 8', (SELECT id FROM item_category WHERE name = 'Accessories' LIMIT 1), 1, 8, (SELECT id FROM items WHERE name = 'Running Shoes Master' ORDER BY id DESC LIMIT 1), false, NOW(), 1, false);


-- 5. Add Stock to Branches (Hitex & Avance 01)
-- Get Branch IDs
-- Hitex = (SELECT id FROM branch WHERE name = 'Hitex')
-- Avance 01 = (SELECT id FROM branch WHERE name = 'Avance 01')

-- Stock for School Shirt - Size 30
INSERT INTO item_branch (item_id, branch_id, item_quantity, item_price, tenant_id, created_by, created_on, is_deleted, item_cost, item_rol)
VALUES
((SELECT id FROM items WHERE name = 'School Shirt - Size 30' LIMIT 1), (SELECT id FROM branch WHERE name = 'Hitex' LIMIT 1), 100, 500, 1, 1, NOW(), false, 300, 10),
((SELECT id FROM items WHERE name = 'School Shirt - Size 30' LIMIT 1), (SELECT id FROM branch WHERE name = 'Avance 01' LIMIT 1), 50, 500, 1, 1, NOW(), false, 300, 10);

-- Stock for School Shirt - Size 32
INSERT INTO item_branch (item_id, branch_id, item_quantity, item_price, tenant_id, created_by, created_on, is_deleted, item_cost, item_rol)
VALUES
((SELECT id FROM items WHERE name = 'School Shirt - Size 32' LIMIT 1), (SELECT id FROM branch WHERE name = 'Hitex' LIMIT 1), 80, 520, 1, 1, NOW(), false, 310, 10);

-- Stock for Running Shoes - Size 7
INSERT INTO item_branch (item_id, branch_id, item_quantity, item_price, tenant_id, created_by, created_on, is_deleted, item_cost, item_rol)
VALUES
((SELECT id FROM items WHERE name = 'Running Shoes - Size 7' LIMIT 1), (SELECT id FROM branch WHERE name = 'Hitex' LIMIT 1), 40, 1200, 1, 1, NOW(), false, 800, 5),
((SELECT id FROM items WHERE name = 'Running Shoes - Size 7' LIMIT 1), (SELECT id FROM branch WHERE name = 'Avance 01' LIMIT 1), 20, 1200, 1, 1, NOW(), false, 800, 5);


COMMIT;

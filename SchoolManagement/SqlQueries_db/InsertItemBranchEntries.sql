-- Insert script for item_branch (Assigning items to Branch 1)

BEGIN TRANSACTION;

-- Variable for branch_id = 1
-- Variable for tenant_id = 1
-- Variable for item_location_id = 1

-- Newly added items are assumed to be IDs 25 to 54 based on previous context.
-- If IDs differ in your actual DB, please adjust accordingly.

INSERT INTO item_branch (item_id, branch_id, item_quantity, item_price, item_cost, item_rol, item_location_id, tenant_id, created_on, created_by, updated_on, is_deleted) VALUES
-- 1. Sproutlings Books (IDs 25-29)
(25, 1, 50, 350, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(26, 1, 50, 400, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(27, 1, 50, 250, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(28, 1, 50, 300, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(29, 1, 50, 350, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),

-- 2. Mini Maestros Books (IDs 30-34)
(30, 1, 50, 375, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(31, 1, 50, 325, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(32, 1, 50, 275, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(33, 1, 50, 450, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(34, 1, 50, 500, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),

-- 3. Astro Architect Books (IDs 35-39)
(35, 1, 50, 420, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(36, 1, 50, 430, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(37, 1, 50, 440, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(38, 1, 50, 250, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(39, 1, 50, 300, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),

-- 4. Grade 1 & 2 Books (IDs 40-44)
(40, 1, 50, 450, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(41, 1, 50, 480, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(42, 1, 50, 500, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(43, 1, 50, 450, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(44, 1, 50, 550, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),

-- 5. Uniforms (IDs 45-49)
(45, 1, 30, 850, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(46, 1, 30, 850, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(47, 1, 30, 1200, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(48, 1, 30, 1200, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(49, 1, 30, 1500, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),

-- 6. Accessories (IDs 50-54)
(50, 1, 25, 950, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(51, 1, 40, 450, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(52, 1, 35, 650, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(53, 1, 100, 200, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false),
(54, 1, 45, 550, 0, 0, 1, 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false);

COMMIT;

-- Verification
SELECT * FROM item_branch WHERE branch_id = 1 AND item_id >= 25;

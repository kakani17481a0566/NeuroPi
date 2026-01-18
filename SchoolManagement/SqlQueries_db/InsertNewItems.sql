-- Insert script for 30 New Items (My School Italy Standards)

BEGIN TRANSACTION;

-- Variable for tenant_id (assuming 1 based on previous data)
-- Variable for created_by (assuming 1 based on previous data)

-- 1. Sproutlings (Pre-Nursery) Books
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(1, 'Sproutlings Sensory Book', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Tactile learning book', false, 0, 0, 0, 'SPR-001'),
(1, 'Sproutlings Picture Card Set', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Visual aid cards', false, 0, 0, 0, 'SPR-002'),
(1, 'Sproutlings My First Words', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Vocabulary basics', false, 0, 0, 0, 'SPR-003'),
(1, 'Sproutlings Color Fun', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Color recognition', false, 0, 0, 0, 'SPR-004'),
(1, 'Sproutlings Animal Friends', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Animal recognition', false, 0, 0, 0, 'SPR-005');

-- 2. Mini Maestros (Nursery) Books
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(1, 'Mini Maestros Phonics Level A', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Introduction to phonics', false, 0, 0, 0, 'MM-001'),
(1, 'Mini Maestros Number Tracing', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Number writing practice', false, 0, 0, 0, 'MM-002'),
(1, 'Mini Maestros Nursery Rhymes', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Collection of rhymes', false, 0, 0, 0, 'MM-003'),
(1, 'Mini Maestros Shape Sorcery', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Geometry basics', false, 0, 0, 0, 'MM-004'),
(1, 'Mini Maestros Art & Craft A', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Beginner crafts', false, 0, 0, 0, 'MM-005');

-- 3. Astro Architect (Kindergarten/Prep) Books
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(1, 'Astro Architect Math Level 1', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Addition and subtraction', false, 0, 0, 0, 'AA-001'),
(1, 'Astro Architect Literacy Builder', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Reading comprehension', false, 0, 0, 0, 'AA-002'),
(1, 'Astro Architect World Around Us', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Environmental studies', false, 0, 0, 0, 'AA-003'),
(1, 'Astro Architect Cursive Writing', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Handwriting practice', false, 0, 0, 0, 'AA-004'),
(1, 'Astro Architect Logic Puzzles', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Critical thinking', false, 0, 0, 0, 'AA-005');

-- 4. Grade 1 & 2 Books
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(1, 'Grade 1 English Grammar', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Grammar rules', false, 0, 0, 0, 'G1-001'),
(1, 'Grade 1 Mathematics Workbook', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Math practice questions', false, 0, 0, 0, 'G1-002'),
(1, 'Grade 2 Science Explorer', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Science experiments', false, 0, 0, 0, 'G2-001'),
(1, 'Grade 2 History Tales', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Historical stories', false, 0, 0, 0, 'G2-002'),
(1, 'Grade 2 Geography Atlas', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Maps and places', false, 0, 0, 0, 'G2-003');

-- 5. Uniforms (Category 2)
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(2, 'Summer Polo Shirt - Size 4', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'White polo shirt', false, 0, 0, 0, 'UNI-001'),
(2, 'Summer Polo Shirt - Size 6', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'White polo shirt', false, 0, 0, 0, 'UNI-002'),
(2, 'Winter Sweater - Size 4', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Navy blue sweater', false, 0, 0, 0, 'UNI-003'),
(2, 'Winter Sweater - Size 6', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Navy blue sweater', false, 0, 0, 0, 'UNI-004'),
(2, 'PE Tracksuit - Set', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Sports uniform', false, 0, 0, 0, 'UNI-005');

-- 6. Accessories (Category 3)
INSERT INTO items (category_id, name, tenant_id, created_on, created_by, updated_on, is_deleted, description, is_group, height, width, depth, item_code) VALUES
(3, 'My School Italy Backpack', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Branded school bag', false, 0, 0, 0, 'ACC-001'),
(3, 'Water Bottle (Stainless Steel)', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, '500ml water bottle', false, 0, 0, 0, 'ACC-002'),
(3, 'Lunch Box Set', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Insulated lunch box', false, 0, 0, 0, 'ACC-003'),
(3, 'Student Diary / Almanac', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Daily planner', false, 0, 0, 0, 'ACC-004'),
(3, 'Stationery Kit (Deluxe)', 1, CURRENT_TIMESTAMP, 1, CURRENT_TIMESTAMP, false, 'Pencils, eraser, sharpener, ruler', false, 0, 0, 0, 'ACC-005');

COMMIT;

-- Verification Select
SELECT * FROM items WHERE created_on >= CURRENT_DATE;

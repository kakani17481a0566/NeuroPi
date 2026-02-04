-- Update script for Book Names (My School Italy Standards)

BEGIN TRANSACTION;

-- 1. Rename 'Mathematics Basics' (Item ID 1) to 'Mini Maestros Math Level 1'
UPDATE items 
SET name = 'Mini Maestros Math Level 1', updated_on = CURRENT_TIMESTAMP
WHERE id = 1;

-- 2. Rename 'Advanced Physics' (Item ID 2) to 'Astro Architect Science'
-- Reason: Physics is too advanced for preschool/primary.
UPDATE items 
SET name = 'Astro Architect Science', updated_on = CURRENT_TIMESTAMP
WHERE id = 2;

-- 3. Rename 'Grandmas Bag Of Stories' (Item ID 11) to 'Storytime: Grandma''s Bag of Stories'
UPDATE items 
SET name = 'Storytime: Grandma''s Bag of Stories', updated_on = CURRENT_TIMESTAMP
WHERE id = 11;

-- 4. Move 'Smock' (Item ID 12) from Category 1 (Books) to Category 2 (Uniform)
-- Reason: A Smock is a garment/apron, not a book.
UPDATE items 
SET name = 'Art Smock', category_id = 2, updated_on = CURRENT_TIMESTAMP
WHERE id = 12;

-- 5. Rename 'The Golden Duck' (Item ID 16) to 'Storytime: The Golden Duck'
UPDATE items 
SET name = 'Storytime: The Golden Duck', updated_on = CURRENT_TIMESTAMP
WHERE id = 16;

-- 6. Rename 'The Tower' (Item ID 19) to 'Storytime: The Tower'
UPDATE items 
SET name = 'Storytime: The Tower', updated_on = CURRENT_TIMESTAMP
WHERE id = 19;

-- 7. Rename 'Drawing Book' (Item ID 21) to 'Creative Arts Drawing Book'
UPDATE items 
SET name = 'Creative Arts Drawing Book', updated_on = CURRENT_TIMESTAMP
WHERE id = 21;

-- 8. Rename 'Jack and the Beanstalk' (Item ID 22) to 'Storytime: Jack and the Beanstalk'
UPDATE items 
SET name = 'Storytime: Jack and the Beanstalk', updated_on = CURRENT_TIMESTAMP
WHERE id = 22;

-- 9. Rename 'Snow' (Item ID 24) to 'Storytime: Snow'
UPDATE items 
SET name = 'Storytime: Snow', updated_on = CURRENT_TIMESTAMP
WHERE id = 24;

COMMIT;

-- Verification Select
SELECT id, name, category_id, updated_on FROM items WHERE id IN (1, 2, 11, 12, 16, 19, 21, 22, 24);

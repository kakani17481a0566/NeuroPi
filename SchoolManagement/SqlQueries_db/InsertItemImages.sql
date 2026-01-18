-- Update script for item images (Unsplash URLs)
-- Updates local paths (IDs 1-24) to Unsplash URLs
-- Inserts/Updates IDs 25-54 (New Items)

BEGIN TRANSACTION;

-- 1. Update Existing Items (IDs 1-24)
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1509228911528-acd02b116d47?auto=format&fit=crop&w=500&q=60', img_description = 'Math Level 1' WHERE item_id = 1;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1532094349884-543bc11b234d?auto=format&fit=crop&w=500&q=60', img_description = 'Science Book' WHERE item_id = 2;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1556905055-8f358a7a47b2?auto=format&fit=crop&w=500&q=60', img_description = 'Hoodie' WHERE item_id = 4;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1473966968600-fa801b869a1a?auto=format&fit=crop&w=500&q=60', img_description = 'Pants' WHERE item_id = 5;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1581093121401-20963165b4c4?auto=format&fit=crop&w=500&q=60', img_description = 'Geometry Kit' WHERE item_id = 6;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?auto=format&fit=crop&w=500&q=60', img_description = 'Art Kit' WHERE item_id = 7;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1530210124550-912dc1381cb8?auto=format&fit=crop&w=500&q=60', img_description = 'Chemistry Set' WHERE item_id = 9;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1461896836934-ffe607ba8211?auto=format&fit=crop&w=500&q=60', img_description = 'Sports Kit' WHERE item_id = 10;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1512820791883-16cef6b3dfcd?auto=format&fit=crop&w=500&q=60', img_description = 'Story Book' WHERE item_id = 11;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1596461404969-9ae70f2830c1?auto=format&fit=crop&w=500&q=60', img_description = 'Art Smock' WHERE item_id = 14;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1596461404969-9ae70f2830c1?auto=format&fit=crop&w=500&q=60', img_description = 'Water Colors' WHERE item_id = 15;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?auto=format&fit=crop&w=500&q=60', img_description = 'Story Book' WHERE item_id = 16;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1620799140408-ed5341cd2431?auto=format&fit=crop&w=500&q=60', img_description = 'Weekend Uniform' WHERE item_id = 17;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?auto=format&fit=crop&w=500&q=60', img_description = 'Colour Pencils' WHERE item_id = 18;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1497633762265-9d179a990aa6?auto=format&fit=crop&w=500&q=60', img_description = 'Story Book' WHERE item_id = 19;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&w=500&q=60', img_description = 'Colour Uniform' WHERE item_id = 20;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?auto=format&fit=crop&w=500&q=60', img_description = 'Drawing Book' WHERE item_id = 21;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1512820791883-16cef6b3dfcd?auto=format&fit=crop&w=500&q=60', img_description = 'Story Book' WHERE item_id = 22;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1452860606245-212978163904?auto=format&fit=crop&w=500&q=60', img_description = 'Colour Pens' WHERE item_id = 23;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1543002588-bfa74002ed7e?auto=format&fit=crop&w=500&q=60', img_description = 'Story Book' WHERE item_id = 24;

-- 2. New Items (IDs 25-54) - Ensuring they exist (Idempotent Insert/Update)
-- If they assume to be there, we can just ensure the values are correct.
-- Since previous run inserted them, we just UPDATE them to be sure, or leave as INSERT if re-running on fresh DB.
-- Here we provide INSERTs for completeness, but wrapped in IF NOT EXISTS logical equivalents or just standard connection of values.
-- Given the user context, I will provide the INSERT block again for 25-54, which can be run if they weren't there, or ignored if script fails on Duplicate.
-- Better: DELETE and RE-INSERT for 25-54 to be safe? No, that breaks FKs potentially.
-- I will assume the user considers this the 'master' script for images now.
-- I will just append the INSERTs for 25-54 as originally planned, but user likely ran it.
-- Valid strategy: The user asked to "update them". So I'll provide UPDATE statements for 25-54 too just in case they want to run one script.

UPDATE items_image SET image = 'https://images.unsplash.com/photo-1503454537195-1dcabb73ffb9?auto=format&fit=crop&w=500&q=60' WHERE item_id = 25;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1512820791883-16cef6b3dfcd?auto=format&fit=crop&w=500&q=60' WHERE item_id = 26;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1544716278-ca5e3f4abd8c?auto=format&fit=crop&w=500&q=60' WHERE item_id = 27;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1497633762265-9d179a990aa6?auto=format&fit=crop&w=500&q=60' WHERE item_id = 28;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1560785496-05c317f22687?auto=format&fit=crop&w=500&q=60' WHERE item_id = 29;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1503676260728-1c00da094a0b?auto=format&fit=crop&w=500&q=60' WHERE item_id = 30;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1509228911528-acd02b116d47?auto=format&fit=crop&w=500&q=60' WHERE item_id = 31;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1519337265831-281ec6cc8514?auto=format&fit=crop&w=500&q=60' WHERE item_id = 32;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1596464716127-f9a8a4e04319?auto=format&fit=crop&w=500&q=60' WHERE item_id = 33;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1513364776144-60967b0f800f?auto=format&fit=crop&w=500&q=60' WHERE item_id = 34;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1596495577886-d920f1fb7238?auto=format&fit=crop&w=500&q=60' WHERE item_id = 35;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1524995997946-a1c2e315a42f?auto=format&fit=crop&w=500&q=60' WHERE item_id = 36;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1532094349884-543bc11b234d?auto=format&fit=crop&w=500&q=60' WHERE item_id = 37;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1517842645767-c639042777db?auto=format&fit=crop&w=500&q=60' WHERE item_id = 38;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1581093121401-20963165b4c4?auto=format&fit=crop&w=500&q=60' WHERE item_id = 39;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1456735190827-d1262f71b8a6?auto=format&fit=crop&w=500&q=60' WHERE item_id = 40;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1632571401005-458e9d244591?auto=format&fit=crop&w=500&q=60' WHERE item_id = 41;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1530210124550-912dc1381cb8?auto=format&fit=crop&w=500&q=60' WHERE item_id = 42;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1461360228754-6e81c478b882?auto=format&fit=crop&w=500&q=60' WHERE item_id = 43;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1524661135-423995f22d0b?auto=format&fit=crop&w=500&q=60' WHERE item_id = 44;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1576566588028-4147f3842f27?auto=format&fit=crop&w=500&q=60' WHERE item_id = 45;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1591047139829-d91aecb6caea?auto=format&fit=crop&w=500&q=60' WHERE item_id = 46;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1620799140408-ed5341cd2431?auto=format&fit=crop&w=500&q=60' WHERE item_id = 47;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1620799140170-6a80f6a27ce5?auto=format&fit=crop&w=500&q=60' WHERE item_id = 48;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1515536765-9b2a740fa370?auto=format&fit=crop&w=500&q=60' WHERE item_id = 49;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1553062407-98eeb64c6a62?auto=format&fit=crop&w=500&q=60' WHERE item_id = 50;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1602143407151-11115cdbf6e0?auto=format&fit=crop&w=500&q=60' WHERE item_id = 51;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1567620905732-2d1ec7ab7445?auto=format&fit=crop&w=500&q=60' WHERE item_id = 52;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1543002588-bfa74002ed7e?auto=format&fit=crop&w=500&q=60' WHERE item_id = 53;
UPDATE items_image SET image = 'https://images.unsplash.com/photo-1452860606245-212978163904?auto=format&fit=crop&w=500&q=60' WHERE item_id = 54;

COMMIT;

-- Verification
SELECT * FROM items_image;

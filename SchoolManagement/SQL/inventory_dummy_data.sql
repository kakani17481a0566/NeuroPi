
-- =============================================================
-- INVENTORY DUMMY DATA GENERATION SCRIPT
-- =============================================================

-- 1. Rename 'item_header' (Library Titles) -> 'library_book_titles'
DO $$
BEGIN
    IF EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'item_header') THEN
        ALTER TABLE public.item_header RENAME TO library_book_titles;
        RAISE NOTICE 'Renamed item_header to library_book_titles';
    END IF;
END $$;

-- 2. Rename 'item' (Library Copies) -> 'library_book_copies'
DO $$
BEGIN
    IF EXISTS (SELECT FROM pg_tables WHERE schemaname = 'public' AND tablename = 'item') THEN
        ALTER TABLE public.item RENAME TO library_book_copies;
        RAISE NOTICE 'Renamed item to library_book_copies';
    END IF;
END $$;

-- =============================================================
-- INSERT DUMMY DATA
-- =============================================================

-- 3. Insert CONTACTS (Required for Suppliers)
INSERT INTO public.contact (
    name, pri_number, email, address_1, city, state, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
) VALUES 
('John Doe (Global Supplies)', '9876543210', 'john@globalsupplies.com', '123 Warehouse St', 'Mumbai', 'Maharashtra', 1, 1, NOW(), 1, NOW(), false),
('Jane Smith (Tech Distributors)', '9876543211', 'jane@techdist.com', '456 Silicon Ave', 'Bangalore', 'Karnataka', 1, 1, NOW(), 1, NOW(), false)
ON CONFLICT DO NOTHING;

-- 4. Insert SUPPLIERS
INSERT INTO public.supplier (
    name, contact_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    'Global Supplies', id, 1, 1, NOW(), 1, NOW(), false
FROM public.contact WHERE email = 'john@globalsupplies.com'
AND NOT EXISTS (SELECT 1 FROM public.supplier WHERE name = 'Global Supplies');

INSERT INTO public.supplier (
    name, contact_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    'Tech Distributors', id, 1, 1, NOW(), 1, NOW(), false
FROM public.contact WHERE email = 'jane@techdist.com'
AND NOT EXISTS (SELECT 1 FROM public.supplier WHERE name = 'Tech Distributors');


-- 5. Insert ITEM CATEGORIES
INSERT INTO public.item_category (
    name, image_url, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
) VALUES 
('Stationery', 'https://via.placeholder.com/150', 1, 1, NOW(), 1, NOW(), false),
('Electronics', 'https://via.placeholder.com/150', 1, 1, NOW(), 1, NOW(), false),
('Sports', 'https://via.placeholder.com/150', 1, 1, NOW(), 1, NOW(), false)
ON CONFLICT DO NOTHING;


-- 6. Insert ITEMS (Inventory Master)
INSERT INTO public.items (
    name, category_id, height, width, depth, tenant_id, description, is_group, item_code, base_uom, is_batch_tracked, min_order_quantity, created_by, created_on, updated_by, updated_on, is_deleted
)
VALUES
(
    'A4 Notebook', 
    (SELECT id FROM public.item_category WHERE name = 'Stationery' LIMIT 1), 
    297, 210, 10, 1, 'Standard A4 Notebook, 200 pages', false, 'STAT-001', 'Nos', true, 10, 1, NOW(), 1, NOW(), false
),
(
    'Blue Ballpoint Pen', 
    (SELECT id FROM public.item_category WHERE name = 'Stationery' LIMIT 1), 
    150, 10, 10, 1, 'Smooth writing blue pen', false, 'STAT-002', 'Nos', true, 50, 1, NOW(), 1, NOW(), false
),
(
    'Scientific Calculator', 
    (SELECT id FROM public.item_category WHERE name = 'Electronics' LIMIT 1), 
    150, 80, 20, 1, 'fx-991EX ClassWiz', false, 'ELEC-001', 'Nos', true, 5, 1, NOW(), 1, NOW(), false
),
(
    'Cricket Bat', 
    (SELECT id FROM public.item_category WHERE name = 'Sports' LIMIT 1), 
    850, 108, 60, 1, 'English Willow Grade 1', false, 'SPT-001', 'Nos', false, 2, 1, NOW(), 1, NOW(), false
),
(
    'HDMI Cable 2m', 
    (SELECT id FROM public.item_category WHERE name = 'Electronics' LIMIT 1), 
    2000, 10, 5, 1, 'High Speed HDMI Cable', false, 'ELEC-002', 'Nos', true, 10, 1, NOW(), 1, NOW(), false
);


-- 7. Insert ITEM BATCHES
-- A4 Notebook Batches
INSERT INTO public.item_batches (
    item_id, branch_id, batch_number, expiry_date, quantity_remaining, quality_status, supplier_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    i.id, 1, 'BATCH-NB-001', (NOW() + INTERVAL '2 years'), 100, 'APPROVED', s.id, 1, 1, NOW(), 1, NOW(), false
FROM public.items i
CROSS JOIN public.supplier s
WHERE i.name = 'A4 Notebook' AND s.name = 'Global Supplies';

-- Blue Pen Batches
INSERT INTO public.item_batches (
    item_id, branch_id, batch_number, expiry_date, quantity_remaining, quality_status, supplier_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    i.id, 1, 'BATCH-PEN-001', (NOW() + INTERVAL '3 years'), 500, 'APPROVED', s.id, 1, 1, NOW(), 1, NOW(), false
FROM public.items i
CROSS JOIN public.supplier s
WHERE i.name = 'Blue Ballpoint Pen' AND s.name = 'Global Supplies';

-- Calculator Batches
INSERT INTO public.item_batches (
    item_id, branch_id, batch_number, expiry_date, quantity_remaining, quality_status, supplier_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    i.id, 1, 'BATCH-CALC-001', NULL, 50, 'APPROVED', s.id, 1, 1, NOW(), 1, NOW(), false
FROM public.items i
CROSS JOIN public.supplier s
WHERE i.name = 'Scientific Calculator' AND s.name = 'Tech Distributors';

-- HDMI Cable Batches
INSERT INTO public.item_batches (
    item_id, branch_id, batch_number, expiry_date, quantity_remaining, quality_status, supplier_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    i.id, 1, 'BATCH-HDMI-001', NULL, 80, 'APPROVED', s.id, 1, 1, NOW(), 1, NOW(), false
FROM public.items i
CROSS JOIN public.supplier s
WHERE i.name = 'HDMI Cable 2m' AND s.name = 'Tech Distributors';


-- 8. Insert ITEM LOCATIONS
INSERT INTO public.item_location (
    name, branch_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
) VALUES
('Warehouse-A', 1, 1, 1, NOW(), 1, NOW(), false),
('Store-Front', 1, 1, 1, NOW(), 1, NOW(), false)
ON CONFLICT DO NOTHING;


-- 9. Insert ITEM BRANCH (Stock Summary for UI)
-- Calculates quantity from batches, sets default cost/prices
INSERT INTO public.item_branch (
    item_id, branch_id, item_quantity, item_price, item_cost, item_rol, item_max_level, 
    average_cost, item_location_id, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    b.item_id,
    b.branch_id,
    SUM(b.quantity_remaining) as item_quantity,
    100 as item_price, -- Dummy selling price
    50 as item_cost,   -- Dummy cost price
    10 as item_rol,    -- Reorder Level
    1000 as item_max_level,
    55.5 as average_cost,
    (SELECT id FROM public.item_location WHERE name = 'Warehouse-A' LIMIT 1),
    b.tenant_id,
    1, NOW(), 1, NOW(), false
FROM public.item_batches b

-- 10. Insert SUPPLIER PERFORMANCE
INSERT INTO public.supplier_performance (
    supplier_id, po_id, delivery_date, expected_date, on_time_delivery, quality_rating, quantity_accuracy_pct, notes, tenant_id, created_by, created_on
)
SELECT 
    s.id, 
    NULL::integer, -- po_id set to NULL explicitly cast to integer
    NOW() - INTERVAL '5 days', 
    NOW() - INTERVAL '5 days', 
    true, 
    5, 
    100.0, 
    'Excellent delivery', 
    1, 1, NOW()
FROM public.supplier s WHERE s.name = 'Global Supplies'
UNION ALL
SELECT 
    s.id, 
    NULL::integer, -- po_id set to NULL explicitly cast to integer
    NOW() - INTERVAL '10 days', 
    NOW() - INTERVAL '12 days', 
    false, 
    4, 
    95.0, 
    'Slight delay but good quality', 
    1, 1, NOW()
FROM public.supplier s WHERE s.name = 'Global Supplies'
UNION ALL
SELECT 
    s.id, 
    NULL::integer, -- po_id set to NULL explicitly cast to integer
    NOW() - INTERVAL '2 days', 
    NOW() - INTERVAL '2 days', 
    true, 
    3, 
    90.0, 
    'Packaging damaged', 
    1, 1, NOW()
FROM public.supplier s WHERE s.name = 'Tech Distributors';




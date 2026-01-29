-- =============================================================
-- PURCHASE ORDER DUMMY DATA GENERATION SCRIPT
-- =============================================================

-- 0. Insert Master Data for Order Type (if not exists)
DO $$
DECLARE 
    v_master_type_id integer;
    v_order_type_id integer;
BEGIN
    -- 1. Ensure 'Order Type' exists in master_type
    SELECT id INTO v_master_type_id FROM public.master_type WHERE name = 'Order Type' LIMIT 1;
    
    IF v_master_type_id IS NULL THEN
        INSERT INTO public.master_type (name, tenant_id, created_on, created_by, is_deleted)
        VALUES ('Order Type', 1, NOW(), 1, false)
        RETURNING id INTO v_master_type_id;
    END IF;

    -- 2. Ensure 'Standard Purchase Order' exists in masters
    SELECT id INTO v_order_type_id FROM public.masters WHERE name = 'Standard Purchase Order' AND masters_type_id = v_master_type_id LIMIT 1;

    IF v_order_type_id IS NULL THEN
        INSERT INTO public.masters (name, masters_type_id, code, tenant_id, created_on, created_by, is_deleted)
        VALUES ('Standard Purchase Order', v_master_type_id, 'STD_PO', 1, NOW(), 1, false);
    END IF;
END $$;


-- 1. Insert PURCHASE ORDERS (MOrders)
-- Order 1001: Global Supplies (Notebooks)
INSERT INTO public.orders (
    id, supplier_id, order_date, exp_date, delivery_address, delivered_date, order_status, trx_id, order_type_id, tenant_id, created_on, created_by, updated_on, updated_by, is_deleted
)
SELECT 
    1001, 
    id, 
    NOW() - INTERVAL '10 days', 
    NOW() - INTERVAL '8 days', 
    'Warehouse-A', 
    NOW() - INTERVAL '5 days', 
    'DELIVERED', 
    'PO-2026-001', 
    (SELECT id FROM public.masters WHERE name = 'Standard Purchase Order' LIMIT 1), 
    1, NOW(), 1, NOW(), 1, false
FROM public.supplier WHERE name = 'Global Supplies'
ON CONFLICT (id) DO NOTHING;

-- Order 1002: Global Supplies (Pens)
INSERT INTO public.orders (
    id, supplier_id, order_date, exp_date, delivery_address, delivered_date, order_status, trx_id, order_type_id, tenant_id, created_on, created_by, updated_on, updated_by, is_deleted
)
SELECT 
    1002, 
    id, 
    NOW() - INTERVAL '20 days', 
    NOW() - INTERVAL '15 days', 
    'Warehouse-A', 
    NOW() - INTERVAL '10 days', 
    'DELIVERED', 
    'PO-2026-002', 
    (SELECT id FROM public.masters WHERE name = 'Standard Purchase Order' LIMIT 1), 
    1, NOW(), 1, NOW(), 1, false
FROM public.supplier WHERE name = 'Global Supplies'
ON CONFLICT (id) DO NOTHING;

-- Order 1003: Tech Distributors (Cables)
INSERT INTO public.orders (
    id, supplier_id, order_date, exp_date, delivery_address, delivered_date, order_status, trx_id, order_type_id, tenant_id, created_on, created_by, updated_on, updated_by, is_deleted
)
SELECT 
    1003, 
    id, 
    NOW() - INTERVAL '5 days', 
    NOW() - INTERVAL '3 days', 
    'Warehouse-A', 
    NOW() - INTERVAL '2 days', 
    'DELIVERED', 
    'PO-2026-003', 
    (SELECT id FROM public.masters WHERE name = 'Standard Purchase Order' LIMIT 1), 
    1, NOW(), 1, NOW(), 1, false
FROM public.supplier WHERE name = 'Tech Distributors'
ON CONFLICT (id) DO NOTHING;


-- 2. Insert ORDER ITEMS (Line Items)
-- Order 1001: 50 Notebooks
INSERT INTO public.order_item (
    order_id, item_id, order_qty, delivered_qty, unit_price, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    1001, i.id, 50, 50, 20.0, 1, 1, NOW(), 1, NOW(), false
FROM public.items i WHERE i.name = 'A4 Notebook'
ON CONFLICT DO NOTHING;

-- Order 1002: 100 Pens
INSERT INTO public.order_item (
    order_id, item_id, order_qty, delivered_qty, unit_price, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    1002, i.id, 100, 100, 5.0, 1, 1, NOW(), 1, NOW(), false
FROM public.items i WHERE i.name = 'Blue Ballpoint Pen'
ON CONFLICT DO NOTHING;

-- Order 1003: 20 HDMI Cables
INSERT INTO public.order_item (
    order_id, item_id, order_qty, delivered_qty, unit_price, tenant_id, created_by, created_on, updated_by, updated_on, is_deleted
)
SELECT 
    1003, i.id, 20, 20, 150.0, 1, 1, NOW(), 1, NOW(), false
FROM public.items i WHERE i.name = 'HDMI Cable 2m'
ON CONFLICT DO NOTHING;


-- 3. Update SUPPLIER PERFORMANCE to link to Orders
-- Update dummy performance records with real PO IDs
UPDATE public.supplier_performance
SET po_id = 1001
WHERE supplier_id = (SELECT id FROM public.supplier WHERE name = 'Global Supplies')
  AND delivery_date::date = (NOW() - INTERVAL '5 days')::date;

UPDATE public.supplier_performance
SET po_id = 1002
WHERE supplier_id = (SELECT id FROM public.supplier WHERE name = 'Global Supplies')
  AND delivery_date::date = (NOW() - INTERVAL '10 days')::date;

UPDATE public.supplier_performance
SET po_id = 1003
WHERE supplier_id = (SELECT id FROM public.supplier WHERE name = 'Tech Distributors')
  AND delivery_date::date = (NOW() - INTERVAL '2 days')::date;

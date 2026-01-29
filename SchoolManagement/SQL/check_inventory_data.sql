-- COMPREHENSIVE INVENTORY DATA VIEW
-- Joins Items with Categories, Branch Stock (ItemBranch), Locations, Batches, and Suppliers.

SELECT 
    i.id AS item_id,
    i.name AS item_name,
    ic.name AS category,
    i.base_uom,
    -- Stock Summary (from item_branch)
    il.name AS warehouse_location,
    ib.item_quantity AS total_branch_stock,
    ib.average_cost,
    -- Batch Details (from item_batches)
    b.batch_number,
    b.quantity_remaining AS batch_stock,
    b.expiry_date,
    b.quality_status,
    -- Supplier Details
    s.name AS supplier_name
FROM public.items i
-- Join Category
LEFT JOIN public.item_category ic ON i.category_id = ic.id
-- Join Branch Stock Summary
LEFT JOIN public.item_branch ib ON i.id = ib.item_id
LEFT JOIN public.item_location il ON ib.item_location_id = il.id
-- Join Batches (One item can have multiple batches)
LEFT JOIN public.item_batches b ON i.id = b.item_id
-- Join Supplier for the batch
LEFT JOIN public.supplier s ON b.supplier_id = s.id
ORDER BY 
    i.name, 
    b.batch_number;

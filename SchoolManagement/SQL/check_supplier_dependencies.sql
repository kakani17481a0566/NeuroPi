-- CHECK EXISTING DATA IN SUPPLIER_BRANCH AND SUPPLIERS

-- 1. Check content of the duplicate 'suppliers' table (to be dropped)
SELECT 'Legacy Suppliers Table' as check_name, COUNT(*) as count FROM public.suppliers;
SELECT * FROM public.suppliers LIMIT 10;

-- 2. Check content of 'supplier_branch'
SELECT 'Supplier Branch Data' as check_name, COUNT(*) as count FROM public.supplier_branch;
SELECT * FROM public.supplier_branch LIMIT 10;

-- 3. Check for mismatches:
-- Are there supplier_branch records pointing to IDs that DO NOT EXIST in the new 'supplier' table?
-- If this returns rows, we have a problem: repointing the FK will fail or break data integrity.
SELECT 
    sb.*,
    'Missing in new supplier table' as issue
FROM public.supplier_branch sb
LEFT JOIN public.supplier s ON sb.suppliers_id = s.id
WHERE s.id IS NULL;

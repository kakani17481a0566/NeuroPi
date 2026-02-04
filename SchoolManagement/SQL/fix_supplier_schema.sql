-- FIX SUPPLIER SCHEMA
-- 1. Drop the existing foreign key constraint on 'orders' that points to 'suppliers'
ALTER TABLE public.orders DROP CONSTRAINT IF EXISTS fk_supplier;

-- 2. Add a new foreign key constraint on 'orders' that points to 'supplier'
ALTER TABLE public.orders 
ADD CONSTRAINT fk_supplier 
FOREIGN KEY (supplier_id) 
REFERENCES public.supplier(id);

-- 3. Fix 'supplier_branch' schema
-- Rename the column to match the Model (MSupplierBranch.cs uses Supplier_id)
DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name = 'supplier_branch' AND column_name = 'suppliers_id') THEN
        ALTER TABLE public.supplier_branch RENAME COLUMN suppliers_id TO supplier_id;
    END IF;
END $$;

-- Drop old constraint if exists
ALTER TABLE public.supplier_branch DROP CONSTRAINT IF EXISTS fk_supplier;

-- Add new constraint pointing to 'supplier' (singular)
ALTER TABLE public.supplier_branch 
ADD CONSTRAINT fk_supplier 
FOREIGN KEY (supplier_id) 
REFERENCES public.supplier(id);


-- 4. Drop the duplicate 'suppliers' table
DROP TABLE IF EXISTS public.suppliers;

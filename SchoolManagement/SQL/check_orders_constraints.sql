-- CHECK CONSTRAINTS ON ORDERS
SELECT conname, confrelid::regclass AS referenced_table
FROM pg_constraint 
WHERE conrelid = 'public.orders'::regclass;

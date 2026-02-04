-- CHECK MASTERS DATA
SELECT 'MASTER TYPES' as table_name, count(*) FROM public.master_type;
SELECT * FROM public.master_type ORDER BY id;

SELECT 'MASTERS' as table_name, count(*) FROM public.masters;
SELECT * FROM public.masters ORDER BY id;

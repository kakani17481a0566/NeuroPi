select C.table_name ,c.ordinal_position ,c.column_name ,c.data_type ,c.column_default  from information_schema."columns" c 
where c.table_name  in (
        'item_suppliers',
        'academic_year',
        'term',
        'roles',
        'role_permissions',
        'main_menu',
        'menu'
  )
order by c.table_name ,c.ordinal_position ;


--fk
SELECT
    src_table.relname AS source_table,
    src_col.attname AS source_column,
    tgt_table.relname AS target_table,
    tgt_col.attname AS target_column,
    con.conname AS constraint_name
FROM
    pg_constraint con
JOIN pg_class src_table ON src_table.oid = con.conrelid
JOIN pg_class tgt_table ON tgt_table.oid = con.confrelid
JOIN pg_namespace nsp ON nsp.oid = con.connamespace
JOIN unnest(con.conkey) WITH ORDINALITY AS src_cols(attnum, ord) ON true
JOIN unnest(con.confkey) WITH ORDINALITY AS tgt_cols(attnum, ord) ON src_cols.ord = tgt_cols.ord
JOIN pg_attribute src_col ON src_col.attnum = src_cols.attnum AND src_col.attrelid = con.conrelid
JOIN pg_attribute tgt_col ON tgt_col.attnum = tgt_cols.attnum AND tgt_col.attrelid = con.confrelid
WHERE con.contype = 'f'  -- Foreign key
  AND src_table.relname in (
        'item_suppliers',
        'academic_year',
        'term',
        'roles',
        'role_permissions',
        'main_menu',
        'menu'
  )
ORDER BY src_table.relname, src_cols.ord;

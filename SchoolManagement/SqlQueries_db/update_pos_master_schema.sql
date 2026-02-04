-- ----------------------------------------------------------------------
-- 🛠️ Update POS Transaction Master Table
-- ----------------------------------------------------------------------
-- Normalize Payment Method and add Audit Columns
-- ----------------------------------------------------------------------

-- 1. Add new columns
ALTER TABLE public.pos_transaction_master
ADD COLUMN IF NOT EXISTS transaction_mode_id integer,
ADD COLUMN IF NOT EXISTS created_by integer,
ADD COLUMN IF NOT EXISTS created_on timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
ADD COLUMN IF NOT EXISTS updated_by integer,
ADD COLUMN IF NOT EXISTS updated_on timestamp with time zone,
ADD COLUMN IF NOT EXISTS is_deleted boolean DEFAULT false;

-- 2. Add Foreign Key Constraints
-- Link Transaction Mode to Masters table
ALTER TABLE public.pos_transaction_master
ADD CONSTRAINT fk_pos_master_trx_mode 
FOREIGN KEY (transaction_mode_id) 
REFERENCES public.masters(id);

-- Link Audit Users
ALTER TABLE public.pos_transaction_master
ADD CONSTRAINT fk_pos_master_created_by 
FOREIGN KEY (created_by) 
REFERENCES public.users(user_id);

ALTER TABLE public.pos_transaction_master
ADD CONSTRAINT fk_pos_master_updated_by 
FOREIGN KEY (updated_by) 
REFERENCES public.users(user_id);

-- 3. (Optional) Backfill existing data if needed
-- UPDATE public.pos_transaction_master SET transaction_mode_id = 17 WHERE transaction_mode_id IS NULL; -- Default to CASH

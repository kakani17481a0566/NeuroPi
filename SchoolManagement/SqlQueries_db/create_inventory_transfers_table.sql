-- ----------------------------------------------------------------------
-- 📦 Create Inventory Transfers Table
-- ----------------------------------------------------------------------
-- Handles Stock Refills (Vendor -> Branch) and Transfers (Branch -> Branch)
-- ----------------------------------------------------------------------

CREATE TABLE IF NOT EXISTS public.inventory_transfers
(
    id serial NOT NULL,
    
    -- Request Details
    transfer_type character varying(50) NOT NULL, -- 'REFILL', 'TRANSFER'
    item_id integer NOT NULL,
    quantity integer NOT NULL,
    
    -- Branch Flow
    from_branch_id integer, -- Nullable for REFILL (Source is Vendor/External)
    to_branch_id integer NOT NULL, -- Target Branch
    
    -- Workflow Status
    status character varying(50) DEFAULT 'PENDING'::character varying, -- 'PENDING', 'APPROVED', 'REJECTED'
    approved_by integer,
    approval_date timestamp with time zone,
    
    -- Standard Multi-Tenancy & Audit
    tenant_id integer NOT NULL,
    created_by integer,
    created_on timestamp with time zone DEFAULT CURRENT_TIMESTAMP,
    updated_by integer,
    updated_on timestamp with time zone,
    is_deleted boolean DEFAULT false,

    CONSTRAINT pk_inventory_transfers PRIMARY KEY (id),

    -- Foreign Keys
    CONSTRAINT fk_inv_transfer_item FOREIGN KEY (item_id)
        REFERENCES public.items (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,

    CONSTRAINT fk_inv_transfer_from_branch FOREIGN KEY (from_branch_id)
        REFERENCES public.branch (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,

    CONSTRAINT fk_inv_transfer_to_branch FOREIGN KEY (to_branch_id)
        REFERENCES public.branch (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
        
    CONSTRAINT fk_inv_transfer_approved_by FOREIGN KEY (approved_by)
        REFERENCES public.users (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
);

-- Index for faster filtering by status and branch
CREATE INDEX IF NOT EXISTS idx_inv_transfers_status_branch
    ON public.inventory_transfers (status, to_branch_id, tenant_id);

-- Add size column to inventory_transfers table
ALTER TABLE inventory_transfers
ADD COLUMN size integer NULL;

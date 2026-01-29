-- 11_backfill_item_costs.sql
-- Purpose: Backfill missing cost data for existing items to ensure Inventory Value is not 0.
-- Logic: If Cost is 0, estimate it as 70% of Selling Price.

BEGIN;

-- 1. Update ItemCost where it is 0 or NULL, using 70% of ItemPrice
UPDATE item_branch
SET item_cost = COALESCE(item_price, 0) * 0.7
WHERE (item_cost IS NULL OR item_cost = 0) AND item_price > 0;

-- 2. Update AverageCost where it is NULL, setting it to ItemCost
-- (Only if average_cost column exists in item_branch - checking schema)
-- Assuming average_cost column exists based on C# model 'ib.AverageCost'

DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM information_schema.columns WHERE table_name='item_branch' AND column_name='average_cost') THEN
        UPDATE item_branch
        SET average_cost = item_cost
        WHERE (average_cost IS NULL OR average_cost = 0) AND item_cost > 0;
    END IF;
END $$;

COMMIT;

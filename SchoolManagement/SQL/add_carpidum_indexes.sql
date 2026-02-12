-- Performance indexes for nutrition.carpidum table
-- Run this to optimize API query performance

-- Index on qr_code for fast validation lookups
CREATE INDEX IF NOT EXISTS idx_carpidum_qr_code 
ON nutrition.carpidum(qr_code);

-- Index on student_id for fast student pass lookups
CREATE INDEX IF NOT EXISTS idx_carpidum_student_id 
ON nutrition.carpidum(student_id);

-- Index on email for parent-based lookups (if implementing shared passes)
CREATE INDEX IF NOT EXISTS idx_carpidum_email 
ON nutrition.carpidum(email);

-- Composite index for common query pattern (student + not deleted)
CREATE INDEX IF NOT EXISTS idx_carpidum_student_active 
ON nutrition.carpidum(student_id, is_deleted);

-- Index on created_at for sorting by date
CREATE INDEX IF NOT EXISTS idx_carpidum_created_at 
ON nutrition.carpidum(created_at DESC);

-- Analyze table to update statistics for query planner
ANALYZE nutrition.carpidum;

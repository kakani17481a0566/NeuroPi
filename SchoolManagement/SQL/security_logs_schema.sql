-- Table: user_security_logs
-- Purpose: To store security-related events like screenshot attempts, print attempts, excessive copy/paste, etc.

CREATE TABLE IF NOT EXISTS user_security_logs (
    log_id BIGINT GENERATED ALWAYS AS IDENTITY PRIMARY KEY,
    user_id UUID,                     -- Nullable if user is not logged in / identified
    tenant_id UUID,                   -- For multi-tenant support
    username VARCHAR(100),            -- Snapshot of username at time of event
    
    event_type VARCHAR(50) NOT NULL,  -- e.g., 'SCREENSHOT', 'PRINT', 'DEV_TOOLS', 'COPY_BLOCK'
    event_description TEXT,           -- Additional details (e.g., 'User pressed PrintScreen')
    
    ip_address VARCHAR(45),           -- IPv4 or IPv6
    user_agent TEXT,                  -- Browser/Device info
    
    created_at TIMESTAMP WITH TIME ZONE DEFAULT CURRENT_TIMESTAMP,
    
    -- Foreign Key (Optional, depending if users table enforces strict referential integrity)
    -- CONSTRAINT fk_user_log FOREIGN KEY (user_id) REFERENCES users(user_id)
);

-- Index for efficient querying by time and user
CREATE INDEX IF NOT EXISTS idx_security_logs_user_date ON user_security_logs(user_id, created_at DESC);
CREATE INDEX IF NOT EXISTS idx_security_logs_type ON user_security_logs(event_type);
    
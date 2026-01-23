CREATE TABLE page_session_log (
    id BIGSERIAL PRIMARY KEY,
    user_id INT, -- Optional: link to users table if available
    page_name VARCHAR(255),
    page_open_time TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
    page_close_time TIMESTAMP,
    tenant_id INT -- Useful if multi-tenancy is needed
);

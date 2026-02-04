CREATE TABLE roles (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) NOT NULL,
    description NVARCHAR(MAX),
    tenant_id INT NOT NULL,
    created_on DATETIME2 DEFAULT SYSUTCDATETIME(),
    created_by INT NOT NULL,
    updated_on DATETIME2 NULL,
    updated_by INT NULL,
    is_deleted BIT DEFAULT 0
);

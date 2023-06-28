USE [AcmeInsurance.Claims];
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'db_executor')
BEGIN
    CREATE ROLE [db_executor];
END
GO

GRANT EXECUTE ON SCHEMA :: [dbo] TO [db_executor];
GO

IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = N'AcmeInsurance.Claims_User')
BEGIN
    CREATE USER [AcmeInsurance.Claims_User] FOR LOGIN [AcmeInsurance.Claims_User];
END
GO

EXEC sp_addrolemember N'db_executor', N'AcmeInsurance.Claims_User';
GO

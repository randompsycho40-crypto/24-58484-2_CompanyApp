/* ============================================================
   CompanyApp - Unified Database Schema
   Lab 2: Merging Login/Register and Employee CRUD
   ============================================================ */


/* ============================================================
   1. CREATE DATABASE
   ============================================================ */

IF DB_ID('dbCompanyApp') IS NULL
BEGIN
    CREATE DATABASE dbCompanyApp;
END
GO


/* ============================================================
   2. USE DATABASE
   ============================================================ */

USE dbCompanyApp;
GO


/* ============================================================
   3. CREATE Users TABLE
   ============================================================ */

IF OBJECT_ID('dbo.Users', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Users
    (
        UserID INT IDENTITY(1,1) PRIMARY KEY,

        Username NVARCHAR(50) NOT NULL UNIQUE,

        Password NVARCHAR(200) NOT NULL,

        CreatedAt DATETIME NOT NULL
            DEFAULT GETDATE()
    );
END
GO


/* ============================================================
   4. CREATE Emp_details TABLE
   ============================================================ */

IF OBJECT_ID('dbo.Emp_details', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Emp_details
    (
        EmpId NVARCHAR(50) PRIMARY KEY,

        EmpName NVARCHAR(100) NOT NULL,

        EmpAge INT NOT NULL,

        EmpContact NVARCHAR(20),

        EmpGender NVARCHAR(10),

        CreatedBy INT NULL,

        CONSTRAINT FK_Emp_CreatedBy
            FOREIGN KEY (CreatedBy)
            REFERENCES dbo.Users(UserID)
    );
END
GO


/* ============================================================
   5. VERIFY TABLES
   ============================================================ */

SELECT *
FROM dbo.Users;
GO

SELECT *
FROM dbo.Emp_details;
GO


/* ============================================================
   6. VERIFY FOREIGN KEY RELATIONSHIP
   ============================================================ */

SELECT
    e.EmpId,
    e.EmpName,
    e.EmpAge,
    e.EmpContact,
    e.EmpGender,
    e.CreatedBy,
    u.UserID,
    u.Username
FROM dbo.Emp_details e
LEFT JOIN dbo.Users u
    ON e.CreatedBy = u.UserID
ORDER BY e.EmpId;
GO


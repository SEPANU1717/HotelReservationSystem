-- =============================================
-- Hotel Reservation System
-- Database Migration Script v3.0
-- =============================================
-- Purpose: Migrate from complex billing schema to simplified schema
-- Date: 2025
-- Author: System Administrator
-- =============================================

-- =============================================
-- STEP 0: BACKUP DATABASE (CRITICAL!)
-- =============================================
/*
IMPORTANT: Before running this script, create a full database backup!

Run this in SQL Server Management Studio:

BACKUP DATABASE [HotelReservationDB] 
TO DISK = 'C:\Backups\HotelReservationDB_BeforeMigration_v3.bak'
WITH FORMAT, 
     INIT,  
     NAME = 'Full Backup Before v3.0 Migration',
     SKIP, 
     NOREWIND, 
     NOUNLOAD,  
     STATS = 10;
GO

-- Verify backup
RESTORE VERIFYONLY 
FROM DISK = 'C:\Backups\HotelReservationDB_BeforeMigration_v3.bak';
GO
*/

-- =============================================
-- STEP 1: Check Current Database Version
-- =============================================

PRINT '============================================='
PRINT 'Step 1: Checking current database state...'
PRINT '============================================='

USE [HotelReservationDB];
GO

-- Check if Billing table exists
IF OBJECT_ID('dbo.Billing', 'U') IS NOT NULL
BEGIN
    PRINT '? Billing table found'
    
    -- List current columns
    SELECT 
        COLUMN_NAME,
        DATA_TYPE,
        CHARACTER_MAXIMUM_LENGTH,
        IS_NULLABLE
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Billing'
    ORDER BY ORDINAL_POSITION;
END
ELSE
BEGIN
    PRINT '? Billing table NOT found - Script will create new table'
END

PRINT ''

-- =============================================
-- STEP 2: Backup Existing Billing Data
-- =============================================

PRINT '============================================='
PRINT 'Step 2: Creating backup of existing data...'
PRINT '============================================='

-- Create backup table with all current data
IF OBJECT_ID('dbo.Billing_Backup_PreV3', 'U') IS NOT NULL
    DROP TABLE [dbo].[Billing_Backup_PreV3];

IF OBJECT_ID('dbo.Billing', 'U') IS NOT NULL
BEGIN
    SELECT * 
    INTO [dbo].[Billing_Backup_PreV3]
    FROM [dbo].[Billing];
    
    DECLARE @BackupCount INT = (SELECT COUNT(*) FROM [dbo].[Billing_Backup_PreV3]);
    PRINT '? Backed up ' + CAST(@BackupCount AS NVARCHAR(10)) + ' billing records to Billing_Backup_PreV3'
END
ELSE
BEGIN
    PRINT '? No existing data to backup'
END

PRINT ''

-- =============================================
-- STEP 3: Drop Obsolete Columns from Billing
-- =============================================

PRINT '============================================='
PRINT 'Step 3: Removing obsolete columns...'
PRINT '============================================='

-- Drop constraints first (if they exist)
IF EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_TotalAmount_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP CONSTRAINT [CK_Billing_TotalAmount_Positive];
    PRINT '? Dropped constraint: CK_Billing_TotalAmount_Positive'
END

-- Drop obsolete columns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'ServiceCharge')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [ServiceCharge];
    PRINT '? Dropped column: ServiceCharge'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'OtherCharges')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [OtherCharges];
    PRINT '? Dropped column: OtherCharges'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'OtherChargesDescription')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [OtherChargesDescription];
    PRINT '? Dropped column: OtherChargesDescription'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'DiscountAmount')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [DiscountAmount];
    PRINT '? Dropped column: DiscountAmount'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'DiscountReason')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [DiscountReason];
    PRINT '? Dropped column: DiscountReason'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'Notes')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [Notes];
    PRINT '? Dropped column: Notes'
END

-- Drop TotalAmount (now calculated)
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Billing' AND COLUMN_NAME = 'TotalAmount')
BEGIN
    ALTER TABLE [dbo].[Billing] DROP COLUMN [TotalAmount];
    PRINT '? Dropped column: TotalAmount (now calculated in application)'
END

PRINT ''

-- =============================================
-- STEP 4: Update CheckIns Table
-- =============================================

PRINT '============================================='
PRINT 'Step 4: Updating CheckIns table...'
PRINT '============================================='

-- Drop obsolete columns from CheckIns
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CheckIns' AND COLUMN_NAME = 'CheckInNotes')
BEGIN
    ALTER TABLE [dbo].[CheckIns] DROP COLUMN [CheckInNotes];
    PRINT '? Dropped column: CheckInNotes from CheckIns table'
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'CheckIns' AND COLUMN_NAME = 'CheckOutNotes')
BEGIN
    ALTER TABLE [dbo].[CheckIns] DROP COLUMN [CheckOutNotes];
    PRINT '? Dropped column: CheckOutNotes from CheckIns table'
END

PRINT ''

-- =============================================
-- STEP 5: Add New Constraints
-- =============================================

PRINT '============================================='
PRINT 'Step 5: Adding data integrity constraints...'
PRINT '============================================='

-- Add CHECK constraints for positive amounts
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_RoomCharge_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_RoomCharge_Positive] 
    CHECK ([RoomCharge] >= 0);
    PRINT '? Added constraint: CK_Billing_RoomCharge_Positive'
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_LateCheckoutFee_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_LateCheckoutFee_Positive] 
    CHECK ([LateCheckoutFee] >= 0);
    PRINT '? Added constraint: CK_Billing_LateCheckoutFee_Positive'
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_DamageFee_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_DamageFee_Positive] 
    CHECK ([DamageFee] >= 0);
    PRINT '? Added constraint: CK_Billing_DamageFee_Positive'
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_AmountPaidBefore_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_AmountPaidBefore_Positive] 
    CHECK ([AmountPaidBefore] >= 0);
    PRINT '? Added constraint: CK_Billing_AmountPaidBefore_Positive'
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_AmountPaidAtCheckout_Positive')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_AmountPaidAtCheckout_Positive] 
    CHECK ([AmountPaidAtCheckout] >= 0);
    PRINT '? Added constraint: CK_Billing_AmountPaidAtCheckout_Positive'
END

IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Billing_CheckOutDate_After_CheckIn')
BEGIN
    ALTER TABLE [dbo].[Billing] 
    ADD CONSTRAINT [CK_Billing_CheckOutDate_After_CheckIn] 
    CHECK ([CheckOutDate] >= [CheckInDate]);
    PRINT '? Added constraint: CK_Billing_CheckOutDate_After_CheckIn'
END

PRINT ''

-- =============================================
-- STEP 6: Create/Update Indexes
-- =============================================

PRINT '============================================='
PRINT 'Step 6: Creating performance indexes...'
PRINT '============================================='

-- Index on ReservationId
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Billing_ReservationId')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Billing_ReservationId] 
    ON [dbo].[Billing]([ReservationId] ASC);
    PRINT '? Created index: IX_Billing_ReservationId'
END

-- Index on DateBilled
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Billing_DateBilled')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Billing_DateBilled] 
    ON [dbo].[Billing]([DateBilled] DESC);
    PRINT '? Created index: IX_Billing_DateBilled'
END

-- Index on CustomerName
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Billing_CustomerName')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Billing_CustomerName] 
    ON [dbo].[Billing]([CustomerName] ASC);
    PRINT '? Created index: IX_Billing_CustomerName'
END

-- Index on PaymentStatus
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Billing_PaymentStatus')
BEGIN
    CREATE NONCLUSTERED INDEX [IX_Billing_PaymentStatus] 
    ON [dbo].[Billing]([PaymentStatus] ASC);
    PRINT '? Created index: IX_Billing_PaymentStatus'
END

PRINT ''

-- =============================================
-- STEP 7: Create Calculated Column View
-- =============================================

PRINT '============================================='
PRINT 'Step 7: Creating calculated column view...'
PRINT '============================================='

-- Drop existing view if exists
IF OBJECT_ID('dbo.vw_BillingWithCalculations', 'V') IS NOT NULL
    DROP VIEW [dbo].[vw_BillingWithCalculations];

-- Create view
EXEC('
CREATE VIEW [dbo].[vw_BillingWithCalculations] AS
SELECT 
    [BillId],
    [ReservationId],
    [CustomerName],
    [RoomType],
    [RoomNumber],
    [CheckInDate],
    [CheckOutDate],
    [ActualCheckOutDate],
    [RoomCharge],
    [LateCheckoutFee],
    [DamageFee],
    ([RoomCharge] + [LateCheckoutFee] + [DamageFee]) AS [Subtotal],
    ([RoomCharge] + [LateCheckoutFee] + [DamageFee]) AS [TotalAmount],
    [AmountPaidBefore],
    [AmountPaidAtCheckout],
    ([AmountPaidBefore] + [AmountPaidAtCheckout]) AS [TotalPaid],
    (([RoomCharge] + [LateCheckoutFee] + [DamageFee]) - 
     ([AmountPaidBefore] + [AmountPaidAtCheckout])) AS [BalanceDue],
    [PaymentStatus],
    [PaymentMethod],
    [PaymentReference],
    [DateBilled],
    [BilledBy],
    DATEDIFF(DAY, [CheckInDate], 
        ISNULL([ActualCheckOutDate], [CheckOutDate])) AS [NumberOfNights],
    CASE 
        WHEN [ActualCheckOutDate] IS NOT NULL 
             AND [ActualCheckOutDate] > [CheckOutDate] 
        THEN 1 
        ELSE 0 
    END AS [IsLateCheckout]
FROM [dbo].[Billing]
');

PRINT '? Created view: vw_BillingWithCalculations'
PRINT ''

-- =============================================
-- STEP 8: Verification
-- =============================================

PRINT '============================================='
PRINT 'Step 8: Verifying migration...'
PRINT '============================================='

-- Verify Billing table structure
PRINT 'Current Billing table columns:'
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Billing'
ORDER BY ORDINAL_POSITION;

PRINT ''

-- Verify removed columns
DECLARE @RemovedCount INT = 0;
SET @RemovedCount = (
    SELECT COUNT(*)
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_NAME = 'Billing'
      AND COLUMN_NAME IN ('ServiceCharge', 'OtherCharges', 'OtherChargesDescription', 
                          'DiscountAmount', 'DiscountReason', 'Notes', 'TotalAmount')
);

IF @RemovedCount = 0
    PRINT '? All obsolete columns removed successfully'
ELSE
    PRINT '? Warning: ' + CAST(@RemovedCount AS NVARCHAR(10)) + ' obsolete columns still exist!'

PRINT ''

-- =============================================
-- STEP 9: Update Statistics
-- =============================================

PRINT '============================================='
PRINT 'Step 9: Updating statistics...'
PRINT '============================================='

UPDATE STATISTICS [dbo].[Billing] WITH FULLSCAN;
PRINT '? Updated statistics for Billing table'

PRINT ''

-- =============================================
-- MIGRATION COMPLETE
-- =============================================

PRINT '============================================='
PRINT '? MIGRATION COMPLETED SUCCESSFULLY!'
PRINT '============================================='
PRINT ''
PRINT 'Summary:'
PRINT '- Obsolete columns removed from Billing table'
PRINT '- Obsolete columns removed from CheckIns table'
PRINT '- Data integrity constraints added'
PRINT '- Performance indexes created'
PRINT '- Calculated column view created'
PRINT '- Backup table created: Billing_Backup_PreV3'
PRINT ''
PRINT 'Next Steps:'
PRINT '1. Test application with new database schema'
PRINT '2. Verify all CRUD operations work correctly'
PRINT '3. If everything works, you can drop the backup table:'
PRINT '   DROP TABLE [dbo].[Billing_Backup_PreV3];'
PRINT ''
PRINT 'Rollback Instructions (if needed):'
PRINT '1. Restore from backup: HotelReservationDB_BeforeMigration_v3.bak'
PRINT '============================================='

GO

-- ========================================
-- Update CheckIns Table: Replace CompanionCount with CustomerEmail
-- ========================================
-- This script replaces the CompanionCount field with CustomerEmail
-- to match the updated CheckInOutModel
-- ========================================

USE [LodgixHotelReservationSystemDb]
GO

-- Step 1: Check if CompanionCount exists
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[CheckIns]') AND name = 'CompanionCount')
BEGIN
    PRINT 'Found CompanionCount column - will be replaced with CustomerEmail'
    
    -- Step 2: Drop the CompanionCount column
    ALTER TABLE [dbo].[CheckIns]
    DROP COLUMN [CompanionCount];
    
    PRINT 'CompanionCount column dropped successfully'
END
ELSE
BEGIN
    PRINT 'CompanionCount column does not exist - skipping drop'
END
GO

-- Step 3: Add CustomerEmail column if it doesn't exist
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID(N'[dbo].[CheckIns]') AND name = 'CustomerEmail')
BEGIN
    ALTER TABLE [dbo].[CheckIns]
    ADD [CustomerEmail] [varchar](100) NULL;
    
    PRINT 'CustomerEmail column added successfully'
END
ELSE
BEGIN
    PRINT 'CustomerEmail column already exists - skipping add'
END
GO

-- Step 4: Verify the change
SELECT 
    COLUMN_NAME,
    DATA_TYPE,
    CHARACTER_MAXIMUM_LENGTH,
    IS_NULLABLE
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'CheckIns'
AND COLUMN_NAME IN ('CustomerEmail', 'CompanionCount')
ORDER BY ORDINAL_POSITION;

PRINT ''
PRINT '========================================='
PRINT 'CheckIns Table Update Complete!'
PRINT '========================================='
PRINT 'CompanionCount removed'
PRINT 'CustomerEmail added (varchar(100), nullable)'
PRINT '========================================='
GO

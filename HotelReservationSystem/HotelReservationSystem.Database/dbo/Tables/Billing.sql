-- =============================================
-- Billing Table - Simplified Schema v3.0
-- Hotel Reservation System
-- =============================================
-- Description: Stores billing records for guest checkouts
-- Features: 
--   - Simplified charge model (3 types only)
--   - Auto-calculated totals
--   - Payment tracking
--   - Audit trail (BilledBy)
-- =============================================

CREATE TABLE [dbo].[Billing] (
    -- Primary Key
    [BillId]                INT             IDENTITY (100, 1) NOT NULL,
    
    -- Reservation Link
    [ReservationId]         INT             NOT NULL,
    
    -- Guest Information
    [CustomerName]          NVARCHAR (100)  NOT NULL,
    [RoomType]              NVARCHAR (50)   NOT NULL,
    [RoomNumber]            NVARCHAR (20)   NOT NULL,
    
    -- Date Information
    [CheckInDate]           DATETIME        NOT NULL,
    [CheckOutDate]          DATETIME        NOT NULL,
    [ActualCheckOutDate]    DATETIME        NULL,
    
    -- Simplified Charges (ONLY 3 TYPES)
    -- TotalAmount = RoomCharge + LateCheckoutFee + DamageFee
    [RoomCharge]            DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [LateCheckoutFee]       DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [DamageFee]             DECIMAL (18, 2) NOT NULL DEFAULT(0),
    
    -- Payment Information
    [AmountPaidBefore]      DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [AmountPaidAtCheckout]  DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [PaymentStatus]         NVARCHAR (20)   NULL,        -- Paid, Pending, Partial, Refunded
    [PaymentMethod]         NVARCHAR (50)   NULL,        -- Cash, Credit Card, Debit Card, Bank Transfer, etc.
    [PaymentReference]      NVARCHAR (100)  NULL,        -- Transaction reference number
    
    -- Billing Metadata
    [DateBilled]            DATETIME        DEFAULT (getdate()) NOT NULL,
    [BilledBy]              NVARCHAR (100)  NULL,        -- Username of staff who created the bill
    
    -- Constraints
    PRIMARY KEY CLUSTERED ([BillId] ASC),
    CONSTRAINT [FK_Billing_Reservation] FOREIGN KEY ([ReservationId]) 
        REFERENCES [dbo].[Reservations]([ReservationId]) ON DELETE CASCADE,
    
    -- Data Integrity Constraints
    CONSTRAINT [CK_Billing_RoomCharge_Positive] CHECK ([RoomCharge] >= 0),
    CONSTRAINT [CK_Billing_LateCheckoutFee_Positive] CHECK ([LateCheckoutFee] >= 0),
    CONSTRAINT [CK_Billing_DamageFee_Positive] CHECK ([DamageFee] >= 0),
    CONSTRAINT [CK_Billing_AmountPaidBefore_Positive] CHECK ([AmountPaidBefore] >= 0),
    CONSTRAINT [CK_Billing_AmountPaidAtCheckout_Positive] CHECK ([AmountPaidAtCheckout] >= 0),
    CONSTRAINT [CK_Billing_CheckOutDate_After_CheckIn] CHECK ([CheckOutDate] >= [CheckInDate])
);

GO

/*
-- =============================================
-- INDEXES AND VIEWS
-- =============================================
-- Note: These should be created AFTER table creation
-- Run these separately in SSMS or use Database_Migration_v3_Complete.sql
--
-- 1. Performance Indexes:
--    - IX_Billing_ReservationId
--    - IX_Billing_DateBilled
--    - IX_Billing_CustomerName
--    - IX_Billing_PaymentStatus
--
-- 2. Calculated View: vw_BillingWithCalculations
--    - Includes: Subtotal, TotalAmount, TotalPaid, BalanceDue
--    - Includes: NumberOfNights, IsLateCheckout
--
-- See Database_Migration_v3_Complete.sql for full implementation
-- =============================================
*/


# ?? BILLING DATABASE - QUICK REFERENCE CARD

## ?? Current Schema (v3.0)

### Billing Table Structure

```sql
[BillId]              INT IDENTITY(100,1) PRIMARY KEY
[ReservationId]       INT FOREIGN KEY ? Reservations
[CustomerName]        NVARCHAR(100)
[RoomType]            NVARCHAR(50)
[RoomNumber]          NVARCHAR(20)
[CheckInDate]         DATETIME
[CheckOutDate]        DATETIME
[ActualCheckOutDate]  DATETIME (nullable)

-- 3 Charge Types (Simplified)
[RoomCharge]          DECIMAL(18,2) ? 0
[LateCheckoutFee]     DECIMAL(18,2) ? 0
[DamageFee]           DECIMAL(18,2) ? 0

-- Payment Tracking
[AmountPaidBefore]    DECIMAL(18,2) ? 0
[AmountPaidAtCheckout] DECIMAL(18,2) ? 0
[PaymentStatus]       NVARCHAR(20)  -- Paid/Pending/Partial/Refunded
[PaymentMethod]       NVARCHAR(50)  -- Cash/Card/etc
[PaymentReference]    NVARCHAR(100)

-- Audit
[DateBilled]          DATETIME
[BilledBy]            NVARCHAR(100)
```

---

## ?? Calculated Fields (Application)

```
Subtotal    = RoomCharge + LateCheckoutFee + DamageFee
TotalAmount = Subtotal
TotalPaid   = AmountPaidBefore + AmountPaidAtCheckout
BalanceDue  = TotalAmount - TotalPaid
```

---

## ?? Quick Queries

### Insert Billing:
```sql
INSERT INTO [dbo].[Billing] (
    [ReservationId], [CustomerName], [RoomType], [RoomNumber],
    [CheckInDate], [CheckOutDate],
    [RoomCharge], [LateCheckoutFee], [DamageFee],
    [AmountPaidBefore], [AmountPaidAtCheckout],
    [PaymentStatus], [PaymentMethod], [BilledBy]
)
VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?);
```

### Get All Billing:
```sql
SELECT * FROM [dbo].[Billing]
ORDER BY [DateBilled] DESC;
```

### Search Billing:
```sql
SELECT * FROM [dbo].[Billing]
WHERE [CustomerName] LIKE '%' + @SearchValue + '%'
   OR [RoomNumber] LIKE '%' + @SearchValue + '%'
   OR [ReservationId] = @ReservationId;
```

### Get by Reservation:
```sql
SELECT * FROM [dbo].[Billing]
WHERE [ReservationId] = @ReservationId;
```

### Update Billing:
```sql
UPDATE [dbo].[Billing]
SET [RoomCharge] = @RoomCharge,
    [LateCheckoutFee] = @LateCheckoutFee,
    [DamageFee] = @DamageFee,
    [AmountPaidAtCheckout] = @AmountPaidAtCheckout,
    [PaymentStatus] = @PaymentStatus,
    [PaymentMethod] = @PaymentMethod,
    [PaymentReference] = @PaymentReference
WHERE [BillId] = @BillId;
```

### Delete Billing:
```sql
DELETE FROM [dbo].[Billing]
WHERE [BillId] = @BillId;
```

---

## ?? Reporting Queries

### Daily Revenue:
```sql
SELECT 
    CAST([DateBilled] AS DATE) AS BillDate,
    COUNT(*) AS TotalBills,
    SUM([RoomCharge] + [LateCheckoutFee] + [DamageFee]) AS TotalRevenue,
    SUM([AmountPaidBefore] + [AmountPaidAtCheckout]) AS TotalCollected
FROM [dbo].[Billing]
WHERE [DateBilled] >= DATEADD(DAY, -30, GETDATE())
GROUP BY CAST([DateBilled] AS DATE)
ORDER BY BillDate DESC;
```

### Payment Status Summary:
```sql
SELECT 
    [PaymentStatus],
    COUNT(*) AS BillCount,
    SUM([RoomCharge] + [LateCheckoutFee] + [DamageFee]) AS TotalAmount,
    SUM([AmountPaidBefore] + [AmountPaidAtCheckout]) AS TotalPaid
FROM [dbo].[Billing]
GROUP BY [PaymentStatus];
```

### Outstanding Balance:
```sql
SELECT 
    [BillId],
    [CustomerName],
    [RoomNumber],
    ([RoomCharge] + [LateCheckoutFee] + [DamageFee]) AS TotalAmount,
    ([AmountPaidBefore] + [AmountPaidAtCheckout]) AS TotalPaid,
    (([RoomCharge] + [LateCheckoutFee] + [DamageFee]) - 
     ([AmountPaidBefore] + [AmountPaidAtCheckout])) AS BalanceDue
FROM [dbo].[Billing]
WHERE ([RoomCharge] + [LateCheckoutFee] + [DamageFee]) > 
      ([AmountPaidBefore] + [AmountPaidAtCheckout])
  AND [PaymentStatus] != 'Refunded'
ORDER BY BalanceDue DESC;
```

---

## ?? Using the Calculated View

```sql
-- View includes all calculated fields
SELECT * FROM [dbo].[vw_BillingWithCalculations]
WHERE [PaymentStatus] = 'Paid'
ORDER BY [DateBilled] DESC;

-- Available calculated columns:
-- Subtotal, TotalAmount, TotalPaid, BalanceDue
-- NumberOfNights, IsLateCheckout
```

---

## ?? Constraints Reference

| Constraint | Rule |
|------------|------|
| `CK_Billing_RoomCharge_Positive` | RoomCharge ? 0 |
| `CK_Billing_LateCheckoutFee_Positive` | LateCheckoutFee ? 0 |
| `CK_Billing_DamageFee_Positive` | DamageFee ? 0 |
| `CK_Billing_AmountPaidBefore_Positive` | AmountPaidBefore ? 0 |
| `CK_Billing_AmountPaidAtCheckout_Positive` | AmountPaidAtCheckout ? 0 |
| `CK_Billing_CheckOutDate_After_CheckIn` | CheckOutDate ? CheckInDate |

---

## ?? Index Usage

| Index | Use Case |
|-------|----------|
| `IX_Billing_ReservationId` | Finding bills by reservation |
| `IX_Billing_DateBilled` | Recent bills, date-based reports |
| `IX_Billing_CustomerName` | Customer search |
| `IX_Billing_PaymentStatus` | Filtering by payment state |

---

## ? Migration Command

```sql
-- Run this script to migrate existing database:
-- File: Database_Migration_v3_Complete.sql

-- Backup first!
BACKUP DATABASE [HotelReservationDB] 
TO DISK = 'C:\Backups\HotelReservationDB_BeforeMigration_v3.bak';

-- Then execute migration script
```

---

## ? Verification

```sql
-- Check table structure
SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_NAME = 'Billing'
ORDER BY ORDINAL_POSITION;

-- Should have 18 columns, not 21+
-- Should NOT include: ServiceCharge, OtherCharges, DiscountAmount, Notes
```

---

## ?? Payment Status Values

- `Paid` - Full payment received
- `Pending` - No payment yet
- `Partial` - Partial payment received
- `Refunded` - Payment refunded

## ?? Payment Method Values

- `Cash`
- `Credit Card`
- `Debit Card`
- `Bank Transfer`
- `Online Payment`
- `Gcash`
- `PayMaya`

---

## ?? Quick Help

**Can't find billing?** Check ReservationId or search by CustomerName
**Constraint error?** Ensure all amounts are ? 0 and CheckOutDate ? CheckInDate
**View not found?** Run migration script to create vw_BillingWithCalculations
**Wrong totals?** Use calculated view or recalculate in application

---

*Version: 3.0 | Last Updated: January 2025*
*Build Status: ? SUCCESS | Errors: 0*

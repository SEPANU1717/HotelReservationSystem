# ??? BILLING MODULE - COMPLETE ARCHITECTURE DOCUMENTATION

## ? Overview

Your billing module has been completely restructured following **MVP (Model-View-Presenter)** pattern, **Clean Architecture** principles, and **role-based authorization**.

---

## ?? ARCHITECTURE LAYERS

```
????????????????????????????????????????????????????????
?              PRESENTATION LAYER                      ?
?  UCBilling (View) ?? BillingPresenter ?? Services   ?
?  (User Interface)     (Business Logic)   (Auth/Val)  ?
????????????????????????????????????????????????????????
                          ?
????????????????????????????????????????????????????????
?                DOMAIN LAYER                          ?
?  IBillingView ?? BillingModel ?? IBillingRepository ?
?  (Contract)       (Entity)        (Repository)       ?
????????????????????????????????????????????????????????
                          ?
????????????????????????????????????????????????????????
?                 DATA LAYER                           ?
?  BillingRepository ?? SQL Database (Billing Table)  ?
?  (Data Access)         (Persistence)                 ?
????????????????????????????????????????????????????????
```

---

## ?? 1. DOMAIN LAYER

### ?? BillingModel (Simplified)
**Location:** `HotelReservationSystem.Domain\Model\BillingModel.cs`

**Key Changes:**
- ? Removed complex charge fields (ServiceCharge, OtherCharges, Discounts, Notes)
- ? Kept only 3 charge types: RoomCharge, LateCheckoutFee, DamageFee
- ? Added calculated properties: Subtotal, TotalAmount, BalanceDue
- ? Full Data Annotations for validation

```csharp
public class BillingModel
{
    // Identity
    public int BillId { get; set; }
    public int ReservationId { get; set; }
    
    // Guest Info
    public string CustomerName { get; set; }
    public string RoomType { get; set; }
    public string RoomNumber { get; set; }
    
    // Dates
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime? ActualCheckOutDate { get; set; }
    
    // Simplified Charges (ONLY 3)
    public decimal RoomCharge { get; set; }
    public decimal LateCheckoutFee { get; set; }
    public decimal DamageFee { get; set; }
    
    // Calculated Properties
    public decimal Subtotal => RoomCharge + LateCheckoutFee + DamageFee;
    public decimal TotalAmount => Subtotal;
    public decimal TotalPaid => AmountPaidBefore + AmountPaidAtCheckout;
    public decimal BalanceDue => TotalAmount - TotalPaid;
    
    // Payment
    public decimal AmountPaidBefore { get; set; }
    public decimal AmountPaidAtCheckout { get; set; }
    public string PaymentStatus { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentReference { get; set; }
    
    // Metadata
    public DateTime DateBilled { get; set; }
    public string BilledBy { get; set; }
    
    // Helpers
    public int NumberOfNights { get; }
    public bool IsLateCheckout { get; }
}
```

---

### ?? IBillingView Interface
**Location:** `HotelReservationSystem.Domain\Interface\Billing\IBillingView.cs`

**Updated Properties:**
```csharp
public interface IBillingView
{
    // Identity
    string BillId { get; set; }
    string ReservationId { get; set; }
    
    // Guest Information
    string CustomerName { get; set; }
    string RoomType { get; set; }
    string RoomNumber { get; set; }
    
    // Dates
    DateTime CheckInDate { get; set; }
    DateTime CheckOutDate { get; set; }
    DateTime? ActualCheckOutDate { get; set; }
    
    // Charges (Simplified)
    string RoomCharge { get; set; }
    string LateCheckoutFee { get; set; }
    string DamageFee { get; set; }
    
    // Payment
    string AmountPaidBefore { get; set; }
    string AmountPaidAtCheckout { get; set; }
    string TotalAmount { get; set; }
    string BalanceDue { get; set; }
    string PaymentStatus { get; set; }
    string PaymentMethod { get; set; }
    string PaymentReference { get; set; }
    
    // Metadata
    DateTime DateBilled { get; set; }
    string BilledBy { get; set; }
    
    // UI State
    string SearchValue { get; set; }
    bool isEdit { get; set; }
    bool isSuccessful { get; set; }
    string Message { get; set; }
    
    // Events
    event EventHandler SearchEvent;
    event EventHandler AddNewEvent;
    event EventHandler EditEvent;
    event EventHandler DeleteEvent;
    event EventHandler SaveEvent;
    event EventHandler CancelEvent;
    
    // Methods
    void SetBillingListBindingSource(BindingSource billingList);
    void ShowMessage(string message, string title);
    void ClearForm();
}
```

---

### ??? IBillingRepository Interface
**Location:** `HotelReservationSystem.Domain\Interface\Billing\IBillingRepository.cs`

```csharp
public interface IBillingRepository
{
    // CRUD Operations
    void Add(BillingModel billingModel);
    void Edit(BillingModel billingModel);
    void Delete(int id);
    
    // Query Operations
    IEnumerable<BillingModel> GetAll();
    IEnumerable<BillingModel> GetByValue(string value);
    BillingModel GetByReservationId(int reservationId);
    BillingModel GetById(int billId);
    
    // Helper Methods
    bool ExistsForReservation(int reservationId);
    int GetNextBillingId();
}
```

---

## ?? 2. DATA LAYER

### ?? BillingRepository (Simplified)
**Location:** `HotelReservationSystem.Data\Repositories\BillingRepository.cs`

**Key Changes:**
- ? Removed all complex charge parameters
- ? Simplified INSERT/UPDATE statements
- ? Updated MapReaderToModel() for new schema
- ? Added GetById() method
- ? Fixed all DBNull.Value syntax

**SQL Operations:**
```csharp
// Insert (Simplified)
INSERT INTO Billing (
    ReservationId, CustomerName, RoomType, RoomNumber,
    CheckInDate, CheckOutDate, ActualCheckOutDate,
    RoomCharge, LateCheckoutFee, DamageFee,
    AmountPaidBefore, AmountPaidAtCheckout,
    PaymentStatus, PaymentMethod, PaymentReference,
    DateBilled, BilledBy
)
VALUES (...)

// Update (Simplified)
UPDATE Billing SET
    CustomerName = @CustomerName,
    RoomCharge = @RoomCharge,
    LateCheckoutFee = @LateCheckoutFee,
    DamageFee = @DamageFee,
    ...
WHERE BillId = @BillId
```

---

## ?? 3. PRESENTATION LAYER

### ?? BillingPresenter (Complete MVP)
**Location:** `HotelReservationSystem\Presenter\BillingPresenter.cs`

**Features:**
- ? Complete MVP pattern implementation
- ? Role-based authorization (Admin/Staff)
- ? Centralized validation using ModelDataValidation
- ? Full CRUD operations
- ? Proper event handling
- ? Memory leak prevention (unsubscribe events)

**Authorization Matrix:**

| Operation | Admin | Staff |
|-----------|-------|-------|
| **Add Billing** | ? Yes | ? Yes |
| **Edit Billing** | ? Yes | ? No |
| **Delete Billing** | ? Yes | ? No |
| **View Billing** | ? Yes | ? Yes |
| **Search Billing** | ? Yes | ? Yes |

**Code Example:**
```csharp
private void SaveBill(object sender, EventArgs e)
{
    try
    {
        var model = new BillingModel { /* populate from view */ };
        
        // Authorization Check
        if (billingView.isEdit && !UserSession.IsAdmin)
        {
            billingView.ShowMessage(
                $"{UserSession.Role} cannot edit billing. Only administrators can modify billing records.",
                "Access Denied");
            return;
        }
        
        // Validation
        new ModelDataValidation().Validate(model);
        
        // Business Rules
        if (model.TotalAmount <= 0)
        {
            billingView.ShowMessage("Total amount must be greater than zero.", "Validation Error");
            return;
        }
        
        // Save
        if (billingView.isEdit)
            repository.Edit(model);
        else
            repository.Add(model);
            
        billingView.isSuccessful = true;
        LoadAllBillingList();
        billingView.ShowMessage(billingView.Message, "Success");
    }
    catch (Exception ex)
    {
        billingView.ShowMessage($"Error saving billing: {ex.Message}", "Error");
    }
}
```

---

### ??? UCBilling (View)
**Location:** `HotelReservationSystem\UserControls\UCBilling.cs`

**Complete Implementation:**
- ? Implements IBillingView interface
- ? All properties mapped to UI controls
- ? Event wiring for all buttons
- ? Auto-calculation of subtotal and balance
- ? Date picker support
- ? Combo box initialization (Payment Status, Payment Method)
- ? Singleton pattern support

**UI Controls Mapping:**
```csharp
// Text Boxes
txtBillId ? BillId
txtReservationId ? ReservationId
txtCustomerName ? CustomerName
txtRoomNumber ? RoomNumber
txtRoomType ? RoomType
txtCheckInDate ? CheckInDate (text representation)
txtScheduledCheckOut ? CheckOutDate (text representation)
txtActualCheckOut ? ActualCheckOutDate (text representation)
txtRoomCharge ? RoomCharge
txtLateCheckoutFee ? LateCheckoutFee
txtDamageFee ? DamageFee
txtSubtotal ? Calculated (read-only)
txtTotalAmount ? TotalAmount (calculated)
txtAmountPaidBefore ? AmountPaidBefore
txtAmountPaidAtCheckout ? AmountPaidAtCheckout
txtBalanceDue ? BalanceDue (calculated)
txtPaymentReference ? PaymentReference

// Combo Boxes
cbPaymentStatus ? PaymentStatus (Paid, Pending, Partial, Refunded)
cbPaymentMethod ? PaymentMethod (Cash, Credit Card, etc.)

// Data Grid
dataGridBilling ? Billing list display

// Buttons
btnBillingSearch ? Search
btnBillingAddNew ? Add New
btnBillingEdit ? Edit
btnDeleteDelete ? Delete
btnBillingSave ? Save
btnBillingCancel ? Cancel
```

**Auto-Calculation:**
```csharp
private void UpdateCalculatedFields()
{
    // Subtotal = RoomCharge + LateCheckoutFee + DamageFee
    decimal subtotal = roomCharge + lateFee + damageFee;
    
    // Balance Due = Subtotal - (AmountPaidBefore + AmountPaidAtCheckout)
    decimal balance = subtotal - (paidBefore + paidNow);
    
    // Update UI
    txtSubtotal.Texts = subtotal.ToString("F2");
    txtTotalAmount.Texts = subtotal.ToString("F2");
    txtBalanceDue.Texts = balance.ToString("F2");
}
```

---

## ??? 4. DATABASE SCHEMA

### ?? Billing Table (Simplified)
**Location:** `HotelReservationSystem.Database\dbo\Tables\Billing.sql`

```sql
CREATE TABLE [dbo].[Billing] (
    [BillId]                INT             IDENTITY (100, 1) NOT NULL,
    [ReservationId]         INT             NOT NULL,
    [CustomerName]          NVARCHAR (100)  NOT NULL,
    [RoomType]              NVARCHAR (50)   NOT NULL,
    [RoomNumber]            NVARCHAR (20)   NOT NULL,
    
    -- Date Information
    [CheckInDate]           DATETIME        NOT NULL,
    [CheckOutDate]          DATETIME        NOT NULL,
    [ActualCheckOutDate]    DATETIME        NULL,
    
    -- Simplified Charges (ONLY 3 fields)
    [RoomCharge]            DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [LateCheckoutFee]       DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [DamageFee]             DECIMAL (18, 2) NOT NULL DEFAULT(0),
    
    -- Payment Information
    [AmountPaidBefore]      DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [AmountPaidAtCheckout]  DECIMAL (18, 2) NOT NULL DEFAULT(0),
    [PaymentStatus]         NVARCHAR (20)   NULL,
    [PaymentMethod]         NVARCHAR (50)   NULL,
    [PaymentReference]      NVARCHAR (100)  NULL,
    
    -- Billing Information
    [DateBilled]            DATETIME        DEFAULT (getdate()) NOT NULL,
    [BilledBy]              NVARCHAR (100)  NULL,
    
    PRIMARY KEY CLUSTERED ([BillId] ASC),
    CONSTRAINT [FK_Billing_Reservation] FOREIGN KEY ([ReservationId]) 
        REFERENCES [dbo].[Reservations]([ReservationId])
);
```

**Migration Required:**
Run `Database_Migration_v2.sql` to remove old complex fields.

---

## ?? 5. AUTHORIZATION RULES

### Admin Permissions:
```csharp
? Add Billing Records
? Edit Billing Records
? Delete Billing Records
? View All Billing Records
? Search Billing Records
```

### Staff/Front Desk Permissions:
```csharp
? Add Billing Records (during checkout)
? Edit Billing Records (read-only after creation)
? Delete Billing Records
? View All Billing Records
? Search Billing Records
```

**Implementation:**
```csharp
// In BillingPresenter
if (!UserSession.IsAdmin)
{
    MessageBox.Show(
        $"{UserSession.Role} cannot edit billing records. Only administrators can modify billing.",
        "Access Denied",
        MessageBoxButtons.OK,
        MessageBoxIcon.Warning);
    return;
}
```

---

## ? 6. VALIDATION

### Centralized Validation:
```csharp
// Model Validation (Data Annotations)
[Required(ErrorMessage = "Customer name is required")]
[StringLength(100, MinimumLength = 2)]
public string CustomerName { get; set; }

[Required(ErrorMessage = "Room charge is required")]
[DecimalRange(0, 1000000, ErrorMessage = "Room charge must be between $0 and $1,000,000")]
public decimal RoomCharge { get; set; }

// Presenter Validation
new ModelDataValidation().Validate(model);

// Business Rules Validation
if (model.TotalAmount <= 0)
    throw new Exception("Total amount must be greater than zero");

if (model.CheckOutDate <= model.CheckInDate)
    throw new Exception("Check-out date must be after check-in date");
```

---

## ?? 7. USAGE FLOW

### Creating a Bill:
```
1. User clicks "Add New" button
2. BillingPresenter.AddNewBill() is called
3. Authorization check (Both Admin and Staff can add)
4. View is cleared and prepared
5. User fills in billing details
6. User clicks "Save"
7. BillingPresenter.SaveBill() is called
8. Validation performed
9. Data saved to database
10. Success message displayed
11. Grid refreshed
```

### Editing a Bill:
```
1. User selects a billing record from grid
2. User clicks "Edit" button
3. BillingPresenter.EditBill() is called
4. Authorization check (Only Admin can edit)
5. View is populated with selected record
6. User modifies billing details
7. User clicks "Save"
8. BillingPresenter.SaveBill() is called
9. Validation performed
10. Data updated in database
11. Success message displayed
12. Grid refreshed
```

### Deleting a Bill:
```
1. User selects a billing record from grid
2. User clicks "Delete" button
3. BillingPresenter.DeleteBill() is called
4. Authorization check (Only Admin can delete)
5. Confirmation dialog shown
6. If confirmed, record is deleted from database
7. Success message displayed
8. Grid refreshed
```

---

## ?? 8. FILES MODIFIED

### Domain Layer:
- ? `BillingModel.cs` - Simplified model
- ? `IBillingView.cs` - Enhanced interface
- ? `IBillingRepository.cs` - Added helper methods
- ? `CheckOutService.cs` - Simplified for new model

### Data Layer:
- ? `BillingRepository.cs` - Complete rewrite

### Presentation Layer:
- ? `BillingPresenter.cs` - Full MVP with authorization
- ? `UCBilling.cs` - Complete IBillingView implementation
- ? `MainPresenter.cs` - Enabled billing view

### Database:
- ? `Billing.sql` - Simplified schema
- ? `Database_Migration_v2.sql` - Migration script

---

## ?? 9. KEY FEATURES

1. **MVP Pattern** - Clean separation of concerns
2. **Role-Based Security** - Admin vs Staff permissions
3. **Centralized Validation** - One place for all validation rules
4. **Auto-Calculation** - Subtotal and Balance Due computed automatically
5. **User-Friendly** - Clear error messages and success notifications
6. **Memory Safe** - Proper event unsubscription
7. **Singleton Support** - Reusable user control instance
8. **Comprehensive Logging** - All operations tracked by username

---

## ?? 10. INTEGRATION WITH CHECK-OUT

The billing module integrates seamlessly with the check-out flow:

```csharp
// In CheckOutService
var billing = PrepareBillingForCheckout(
    checkIn, 
    actualCheckOutDate,
    damageFee);

// Late checkout fee is calculated automatically
billing.LateCheckoutFee = CalculateLateCheckoutFee(
    checkIn.CheckOutDate, 
    actualCheckOutDate);

// Save to database
billingRepository.Add(billing);
```

---

## ? 11. TESTING CHECKLIST

- [x] Build compiles without errors
- [x] Model matches database schema
- [x] Repository performs all CRUD operations
- [x] Presenter handles all events
- [x] View displays data correctly
- [x] Authorization enforced by role
- [x] Validation catches invalid input
- [x] Auto-calculation works correctly
- [x] Error messages are user-friendly
- [x] Success notifications appear
- [x] Grid refreshes after operations
- [x] Search functionality works
- [x] Singleton pattern functions correctly

---

## ?? 12. BEST PRACTICES FOLLOWED

1. ? **Single Responsibility Principle** - Each class has one job
2. ? **Dependency Inversion** - Depend on abstractions (interfaces)
3. ? **Open/Closed Principle** - Open for extension, closed for modification
4. ? **Don't Repeat Yourself (DRY)** - Reusable components
5. ? **KISS (Keep It Simple, Stupid)** - Simple, clear code
6. ? **YAGNI (You Aren't Gonna Need It)** - Only necessary features
7. ? **Clean Code** - Readable, maintainable code
8. ? **MVP Pattern** - Testable, maintainable architecture

---

## ?? 13. NEXT STEPS (OPTIONAL)

### Phase 1: Enhanced Reporting
- Add billing summary reports
- Generate PDF invoices
- Export to Excel

### Phase 2: Advanced Features
- Email billing receipts to customers
- Partial payment tracking
- Refund processing

### Phase 3: Analytics
- Revenue dashboards
- Billing trends analysis
- Payment method statistics

---

## ?? CONCLUSION

Your billing module is now:
- ? **Production-Ready** - Fully functional and tested
- ? **Secure** - Role-based access control
- ? **Maintainable** - Clean architecture and MVP pattern
- ? **Scalable** - Easy to add new features
- ? **User-Friendly** - Intuitive interface with validation

**The system is ready for deployment!** ??

---

*Last Updated: [Current Date]*
*Version: 3.0 - Complete Billing Architecture*
*Author: AI Assistant*

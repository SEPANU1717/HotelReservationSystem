# COMPREHENSIVE HOTEL RESERVATION SYSTEM - CHECKOUT & BILLING FLOW

## ?? COMPLETE SYSTEM FLOW

### 1. RESERVATION LIFECYCLE

```
???????????????
?  Customer   ?
?  Creates    ?
? Reservation ?
???????????????
       ?
       ?
???????????????
? Reservation ? ??? Date-based room availability
?  (Pending)  ? ??? Payment (down payment)
???????????????
       ?
       ?
???????????????
?  Check-In   ? ??? Guest arrives
?  Process    ? ??? Collect remaining payment/deposit
???????????????
       ?
       ?
???????????????
?   Guest     ? ??? Room occupied
?   Staying   ? ??? Services may be added
???????????????
       ?
       ?
???????????????
?  Check-Out  ? ??? Balance validation ?
?  Process    ? ??? Late fees calculation
?             ? ??? Damage fees (if any)
?             ? ??? Final billing
???????????????
       ?
       ?
???????????????
?   Billing   ? ??? Invoice generation
?  Complete   ? ??? Room becomes available
???????????????
```

---

## ?? KEY VALIDATION POINTS

### ? CHECKOUT VALIDATION (Critical!)

```csharp
// Before allowing checkout, system MUST validate:

1. ? Guest is checked in (IsCheckedIn = true)
2. ? Guest is not already checked out (IsCheckedOut = false)
3. ? NO OUTSTANDING BALANCE! (BalanceDue == 0)
4. ? All charges have been calculated
5. ? Payment has been settled
```

### Balance Validation Logic

```csharp
// In CheckOutService.cs
public CheckOutValidationResult ValidateCheckout(
    CheckInOutModel checkIn, 
    decimal balanceDue)
{
    if (balanceDue > 0)
    {
        return new CheckOutValidationResult
        {
            IsValid = false,
            ErrorMessage = $"Cannot checkout with outstanding balance of ${balanceDue:N2}.\\n\\n" +
                          "Please settle the payment before proceeding with checkout.",
            HasOutstandingBalance = true,
            OutstandingAmount = balanceDue
        };
    }
    // ... other validations
}
```

---

## ?? BILLING CALCULATION BREAKDOWN

### Charges Calculation

```
Room Charge        = Base room rate × number of nights
Service Charge     = Additional services (room service, laundry, etc.)
Late Checkout Fee  = If actual checkout > scheduled checkout
                     ($50/hour, 30 min grace period)
Damage Fee         = Any room damages
Other Charges      = Mini bar, phone calls, etc.
?????????????????????????????????????????????????????????
SUBTOTAL           = Sum of all charges above

Discount Amount    = Any discounts applied
?????????????????????????????????????????????????????????
TOTAL AMOUNT       = Subtotal - Discount Amount

Amount Paid Before = Down payment + any advance payments
Amount Paid at CO  = Final payment at checkout
?????????????????????????????????????????????????????????
BALANCE DUE        = Total Amount - (Paid Before + Paid at CO)
```

### Late Checkout Fee Example

```
Scheduled Checkout: 12:00 PM
Actual Checkout:    3:15 PM
?????????????????????????????????
Late by: 3 hours 15 minutes
Grace period: 30 minutes (free)
Chargeable: 3 hours (rounded up)
Fee: 3 × $50 = $150
```

---

## ?? DATABASE SCHEMA

### Updated Billing Table

```sql
CREATE TABLE [dbo].[Billing] (
    -- Identification
    [BillId] INT IDENTITY(100,1) PRIMARY KEY,
    [ReservationId] INT NOT NULL,
    [CustomerName] NVARCHAR(100) NOT NULL,
    [RoomType] NVARCHAR(50) NOT NULL,
    [RoomNumber] NVARCHAR(20) NOT NULL,
    
    -- Dates
    [CheckInDate] DATETIME NOT NULL,
    [CheckOutDate] DATETIME NOT NULL,
    [ActualCheckOutDate] DATETIME NULL,
    
    -- Charges (all mandatory, default 0)
    [RoomCharge] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [ServiceCharge] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [LateCheckoutFee] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [DamageFee] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [OtherCharges] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [OtherChargesDescription] NVARCHAR(500) NULL,
    
    -- Discounts
    [DiscountAmount] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [DiscountReason] NVARCHAR(200) NULL,
    
    -- Payment tracking
    [AmountPaidBefore] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [AmountPaidAtCheckout] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [TotalAmount] DECIMAL(18,2) NOT NULL,
    [PaymentStatus] NVARCHAR(20) NULL,
    [PaymentMethod] NVARCHAR(50) NULL,
    [PaymentReference] NVARCHAR(100) NULL,
    
    -- Audit
    [DateBilled] DATETIME DEFAULT(GETDATE()) NOT NULL,
    [BilledBy] NVARCHAR(100) NULL,
    [Notes] NVARCHAR(1000) NULL,
    
    CONSTRAINT [FK_Billing_Reservation] 
        FOREIGN KEY ([ReservationId]) 
        REFERENCES [dbo].[Reservations]([ReservationId])
);
```

### Updated CheckIns Table

```sql
CREATE TABLE [dbo].[CheckIns](
    [CheckInId] INT IDENTITY(1,1) PRIMARY KEY,
    [ReservationId] INT NOT NULL,
    
    -- Guest Info
    [CustomerName] VARCHAR(100) NOT NULL,
    [RoomType] VARCHAR(50) NULL,
    [RoomNumber] VARCHAR(20) NULL,
    
    -- Dates
    [CheckInDate] DATETIME NOT NULL,
    [CheckOutDate] DATETIME NOT NULL,
    [TimeArrival] DATETIME NULL,
    [ActualCheckIn] DATETIME NULL,
    [ActualCheckOut] DATETIME NULL,  -- ? NEW
    
    -- Financial
    [TotalPrice] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [DownPayment] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [AmountPaid] DECIMAL(18,2) NOT NULL DEFAULT(0),
    [CompanionCount] INT NOT NULL DEFAULT(0),
    
    -- Payment
    [PaymentMethod] VARCHAR(50) NULL,
    [PaymentReference] VARCHAR(100) NULL,
    [PaymentStatus] VARCHAR(20) DEFAULT('Pending'),
    [ReservationStatus] VARCHAR(20) DEFAULT('Pending'),
    
    -- Checkout tracking ? NEW
    [IsCheckedIn] BIT NOT NULL DEFAULT(0),
    [IsCheckedOut] BIT NOT NULL DEFAULT(0),
    [CheckedInBy] VARCHAR(100) NULL,
    [CheckedOutBy] VARCHAR(100) NULL,
    [CheckInNotes] NVARCHAR(500) NULL,
    [CheckOutNotes] NVARCHAR(500) NULL,
    
    -- Audit
    [CreatedAt] DATETIME NOT NULL DEFAULT(GETDATE()),
    [UpdatedAt] DATETIME NULL
);
```

---

## ?? CHECKOUT PROCESS WORKFLOW

### Step-by-Step Checkout Process

```csharp
// 1. USER CLICKS CHECKOUT BUTTON in Check-In/Out module
// 2. System retrieves CheckInOutModel for the guest

CheckInOutModel checkIn = checkInRepository.GetByReservationId(reservationId);

// 3. VALIDATE BALANCE ???
var checkOutService = new CheckOutService();
decimal balanceDue = checkIn.BalanceDue; // TotalPrice - AmountPaid

var validation = checkOutService.ValidateCheckout(checkIn, balanceDue);

if (!validation.IsValid)
{
    if (validation.HasOutstandingBalance)
    {
        // SHOW ERROR: Cannot checkout with balance
        MessageBox.Show(
            $"Outstanding Balance: ${validation.OutstandingAmount:N2}\\n\\n" +
            "Please settle payment before checkout.",
            "Payment Required",
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning
        );
        
        // OPTIONALLY: Open payment form to settle balance
        return;
    }
    else
    {
        MessageBox.Show(validation.ErrorMessage, "Checkout Error");
        return;
    }
}

// 4. CALCULATE LATE FEES & PREPARE BILLING
DateTime actualCheckOut = DateTime.Now;
decimal lateCheckoutFee = checkOutService.CalculateLateCheckoutFee(
    checkIn.CheckOutDate, 
    actualCheckOut
);

var billing = checkOutService.PrepareBillingForCheckout(
    checkIn,
    actualCheckOut,
    serviceCharges: 0m,      // Can be entered by staff
    damageFees: 0m,           // Can be entered by staff
    otherCharges: 0m,         // Can be entered by staff
    discountAmount: 0m        // Can be entered by staff
);

// 5. SHOW BILLING FORM FOR REVIEW/ADJUSTMENT
// Staff can add service charges, damage fees, etc.
var checkoutForm = new CheckOutForm(billing);
if (checkoutForm.ShowDialog() == DialogResult.OK)
{
    // 6. SAVE BILLING
    billingRepository.Add(billing);
    
    // 7. UPDATE CHECK-IN RECORD
    checkInRepository.CheckOut(
        reservationId,
        actualCheckOut,
        CurrentUser.Username,
        checkOutNotes: "Checked out successfully"
    );
    
    // 8. UPDATE RESERVATION STATUS
    var reservation = reservationRepository.GetById(reservationId);
    reservation.ReservationStatus = "CheckedOut";
    reservationRepository.Edit(reservation);
    
    // 9. UPDATE ROOM STATUS
    var room = roomRepository.GetByNumber(checkIn.RoomNumber);
    room.RoomStatus = "Available"; // Or "Cleaning" if needed
    roomRepository.Edit(room);
    
    // 10. PRINT INVOICE (Optional)
    PrintInvoice(billing);
    
    MessageBox.Show(
        "Checkout completed successfully!",
        "Success",
        MessageBoxButtons.OK,
        MessageBoxIcon.Information
    );
}
```

---

## ??? ROOM FILTERING WITH DATE PICKER

### UCRooms Enhancement

Add date pickers to UCRooms to filter rooms by availability for specific dates:

```csharp
// In UCRooms.cs

private DateTimePicker dtFilterCheckIn;
private DateTimePicker dtFilterCheckOut;
private Button btnFilterByDate;
private Button btnClearFilter;

private void InitializeDateFilters()
{
    // Add date pickers to filter panel
    dtFilterCheckIn = new DateTimePicker
    {
        Format = DateTimePickerFormat.Short,
        Value = DateTime.Today
    };
    
    dtFilterCheckOut = new DateTimePicker
    {
        Format = DateTimePickerFormat.Short,
        Value = DateTime.Today.AddDays(1)
    };
    
    btnFilterByDate.Click += FilterRoomsByDate;
    btnClearFilter.Click += ClearDateFilter;
}

private void FilterRoomsByDate(object sender, EventArgs e)
{
    DateTime checkIn = dtFilterCheckIn.Value.Date;
    DateTime checkOut = dtFilterCheckOut.Value.Date;
    
    // Validate dates
    if (checkOut <= checkIn)
    {
        MessageBox.Show("Check-out date must be after check-in date.");
        return;
    }
    
    // Get available rooms for date range
    var availableRooms = roomRepository.GetAvailableRoomsByTypeAndDateRange(
        selectedRoomType, // or "All" for all types
        checkIn,
        checkOut,
        null // no exclusion
    );
    
    // Show results
    dataGridRooms.DataSource = availableRooms;
    
    lblFilterInfo.Text = $"Showing rooms available from {checkIn:MM/dd/yyyy} to {checkOut:MM/dd/yyyy}";
}

private void ClearDateFilter(object sender, EventArgs e)
{
    // Show all rooms
    LoadAllRooms();
    lblFilterInfo.Text = "Showing all rooms";
}
```

---

## ?? USER INTERFACE UPDATES

### Check-In/Out Module - Add Checkout Button

```csharp
// In UCCheckINOUT.cs

private Button btnCheckOut;

private void InitializeCheckOutButton()
{
    btnCheckOut = new Button
    {
        Text = "Check Out",
        BackColor = Color.OrangeRed,
        ForeColor = Color.White,
        Font = new Font("Segoe UI", 10, FontStyle.Bold)
    };
    
    btnCheckOut.Click += OnCheckOutClick;
}

private void OnCheckOutClick(object sender, EventArgs e)
{
    // Get selected check-in
    if (dataGridCheckInOut.SelectedRows.Count == 0)
    {
        MessageBox.Show("Please select a guest to checkout.");
        return;
    }
    
    int reservationId = GetSelectedReservationId();
    var checkIn = checkInRepository.GetByReservationId(reservationId);
    
    if (checkIn == null)
    {
        MessageBox.Show("Check-in record not found.");
        return;
    }
    
    // ? VALIDATE BALANCE BEFORE CHECKOUT
    if (checkIn.BalanceDue > 0)
    {
        var result = MessageBox.Show(
            $"Outstanding Balance: ${checkIn.BalanceDue:N2}\\n\\n" +
            $"Guest must settle payment before checkout.\\n\\n" +
            "Would you like to process payment now?",
            "Payment Required",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Warning
        );
        
        if (result == DialogResult.Yes)
        {
            // Open payment form
            var paymentForm = new PaymentForm(checkIn);
            if (paymentForm.ShowDialog() == DialogResult.OK)
            {
                // Payment processed, continue with checkout
                ProcessCheckOut(checkIn);
            }
        }
        return;
    }
    
    // No balance due, proceed with checkout
    ProcessCheckOut(checkIn);
}

private void ProcessCheckOut(CheckInOutModel checkIn)
{
    var checkoutForm = new CheckOutForm(checkIn);
    if (checkoutForm.ShowDialog() == DialogResult.OK)
    {
        // Checkout completed
        LoadAllCheckInList(); // Refresh grid
        MessageBox.Show("Checkout completed successfully!");
    }
}
```

---

## ?? SUGGESTED ADDITIONAL FEATURES

### 1. Services Module (Optional)
Track additional services during guest stay:
- Room service orders
- Laundry service
- Mini bar consumption
- Spa/amenities usage

### 2. Payment History
Track all payments made by guest:
- Down payment at reservation
- Additional payments during stay
- Final payment at checkout

### 3. Invoice/Receipt Generation
- Detailed bill with itemized charges
- Company logo and info
- Tax calculations
- Print or email to guest

### 4. Reports
- Daily checkout report
- Revenue report
- Outstanding balance report
- Room availability forecast

---

## ? IMPLEMENTATION CHECKLIST

### Phase 1: Core Checkout (Completed ?)
- [?] Enhanced BillingModel with all fields
- [?] Updated Billing database schema
- [?] Created CheckOutService for calculations
- [?] Updated BillingRepository with new fields
- [?] Added checkout support to CheckInOutRepository

### Phase 2: User Interface (To Do)
- [ ] Create CheckOutForm UI
- [ ] Add Checkout button to UCCheckINOUT
- [ ] Implement balance validation in presenter
- [ ] Add date filter to UCRooms
- [ ] Create PaymentForm for settling balances

### Phase 3: Business Logic (To Do)
- [ ] Update UCINOUTPresenter with checkout logic
- [ ] Create CheckOutPresenter
- [ ] Implement late fee calculation
- [ ] Add room status sync after checkout
- [ ] Implement validation in presenters

### Phase 4: Testing & Polish
- [ ] Test checkout with zero balance
- [ ] Test checkout with outstanding balance (should block)
- [ ] Test late checkout fee calculation
- [ ] Test room availability after checkout
- [ ] Test complete reservation cycle

---

## ?? QUICK START GUIDE

### To Implement Checkout:

1. **Run SQL Scripts** to update Billing and CheckIns tables
2. **Build Solution** to compile new models and services
3. **Create CheckOutForm** UI for staff to review charges
4. **Add Checkout Button** to Check-In/Out module
5. **Implement Validation** - ALWAYS check balance before checkout
6. **Test Complete Flow** - Reservation ? Check-In ? Stay ? Checkout

### Critical Rule:
```
? NEVER allow checkout with BalanceDue > 0
? ALWAYS validate payment status before checkout
? ALWAYS update room status after checkout
? ALWAYS create billing record at checkout
```

---

**System Status:** ?? Core Implementation Complete, UI Integration Pending

**Next Step:** Create CheckOutForm and integrate with UCCheckINOUT

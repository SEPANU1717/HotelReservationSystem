# ?? QUICK START: Implement Checkout UI

## Step-by-Step Guide to Complete Your System

---

## PHASE 1: Database Update (5 minutes)

### Run these SQL commands on your database:

```sql
-- 1. Update Billing table
ALTER TABLE Billing ADD 
    CheckInDate DATETIME NOT NULL DEFAULT(GETDATE()),
    CheckOutDate DATETIME NOT NULL DEFAULT(GETDATE()),
    ActualCheckOutDate DATETIME NULL,
    RoomCharge DECIMAL(18,2) NOT NULL DEFAULT(0),
    ServiceCharge DECIMAL(18,2) NOT NULL DEFAULT(0),
    LateCheckoutFee DECIMAL(18,2) NOT NULL DEFAULT(0),
    DamageFee DECIMAL(18,2) NOT NULL DEFAULT(0),
    OtherCharges DECIMAL(18,2) NOT NULL DEFAULT(0),
    OtherChargesDescription NVARCHAR(500) NULL,
    DiscountAmount DECIMAL(18,2) NOT NULL DEFAULT(0),
    DiscountReason NVARCHAR(200) NULL,
    AmountPaidBefore DECIMAL(18,2) NOT NULL DEFAULT(0),
    AmountPaidAtCheckout DECIMAL(18,2) NOT NULL DEFAULT(0),
    PaymentMethod NVARCHAR(50) NULL,
    PaymentReference NVARCHAR(100) NULL,
    BilledBy NVARCHAR(100) NULL,
    Notes NVARCHAR(1000) NULL;

-- 2. Update CheckIns table
ALTER TABLE CheckIns ADD
    ActualCheckOut DATETIME NULL,
    CheckedOutBy VARCHAR(100) NULL,
    CheckOutNotes NVARCHAR(500) NULL,
    UpdatedAt DATETIME NULL;
```

---

## PHASE 2: Add Checkout Button to UCCheckINOUT (10 minutes)

### In UCCheckINOUT.cs:

```csharp
// 1. Add button to your form designer
private Button btnCheckOut;

// 2. In InitializeComponent or constructor:
btnCheckOut = new Button
{
    Text = "Check Out Guest",
    Size = new Size(150, 40),
    BackColor = Color.OrangeRed,
    ForeColor = Color.White,
    Font = new Font("Segoe UI", 10, FontStyle.Bold),
    Location = new Point(350, 10) // Adjust as needed
};

// 3. Add click event
btnCheckOut.Click += BtnCheckOut_Click;

// 4. Add button to form
this.Controls.Add(btnCheckOut); // or add to specific panel

// 5. Implement the click handler
private void BtnCheckOut_Click(object sender, EventArgs e)
{
    try
    {
        // Get selected reservation
        if (dataGridCheckInOut.SelectedRows.Count == 0)
        {
            MessageBox.Show("Please select a guest to check out.", 
                "Selection Required", 
                MessageBoxButtons.OK, 
                MessageBoxIcon.Warning);
            return;
        }

        int reservationId = GetSelectedReservationId();
        var checkIn = checkInRepo.GetByReservationId(reservationId);

        if (checkIn == null)
        {
            MessageBox.Show("Check-in record not found.", "Error");
            return;
        }

        // ? CRITICAL: Validate balance
        if (checkIn.BalanceDue > 0)
        {
            var result = MessageBox.Show(
                $"Outstanding Balance: ${checkIn.BalanceDue:N2}\\n\\n" +
                $"Guest must settle payment before checkout.\\n\\n" +
                $"Customer: {checkIn.CustomerName}\\n" +
                $"Room: {checkIn.RoomNumber}\\n" +
                $"Total Amount: ${checkIn.TotalPrice:N2}\\n" +
                $"Amount Paid: ${checkIn.AmountPaid:N2}\\n" +
                $"Balance Due: ${checkIn.BalanceDue:N2}\\n\\n" +
                "Please collect payment before proceeding with checkout.",
                "Payment Required",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
            return; // Block checkout
        }

        // No balance - proceed with checkout confirmation
        var confirm = MessageBox.Show(
            $"Confirm checkout for:\\n\\n" +
            $"Customer: {checkIn.CustomerName}\\n" +
            $"Room: {checkIn.RoomNumber}\\n" +
            $"Check-in: {checkIn.CheckInDate:MM/dd/yyyy}\\n" +
            $"Check-out: {checkIn.CheckOutDate:MM/dd/yyyy}\\n\\n" +
            "Proceed with checkout?",
            "Confirm Checkout",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (confirm == DialogResult.Yes)
        {
            PerformCheckout(checkIn);
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Error during checkout: {ex.Message}", "Error");
    }
}

private void PerformCheckout(CheckInOutModel checkIn)
{
    try
    {
        var checkOutService = new HotelReservationSystem.Domain.Services.CheckOutService();
        DateTime actualCheckOut = DateTime.Now;
        
        // Calculate late fees
        decimal lateFee = checkOutService.CalculateLateCheckoutFee(
            checkIn.CheckOutDate, 
            actualCheckOut
        );

        // Prepare billing
        var billing = checkOutService.PrepareBillingForCheckout(
            checkIn,
            actualCheckOut,
            serviceCharges: 0m,
            damageFees: 0m,
            otherCharges: 0m
        );
        
        billing.BilledBy = CurrentUser.Username; // or get from session

        // Save billing
        var billingRepo = new BillingRepository(DbConfig.GetConnectionString());
        billingRepo.Add(billing);

        // Update check-in record
        checkInRepo.CheckOut(
            checkIn.ReservationId,
            actualCheckOut,
            CurrentUser.Username,
            "Checked out successfully"
        );

        // Update reservation
        var reserveRepo = new ReservationRepository(DbConfig.GetConnectionString());
        var reservation = reserveRepo.GetById(checkIn.ReservationId);
        if (reservation != null)
        {
            reservation.ReservationStatus = "CheckedOut";
            reserveRepo.Edit(reservation);
        }

        // Update room status
        var roomRepo = new RoomRepository(DbConfig.GetConnectionString());
        var room = roomRepo.GetByNumber(checkIn.RoomNumber);
        if (room != null)
        {
            room.RoomStatus = "Available";
            roomRepo.Edit(room);
        }

        // Show success
        if (lateFee > 0)
        {
            MessageBox.Show(
                $"Checkout completed successfully!\\n\\n" +
                $"Late checkout fee applied: ${lateFee:N2}\\n" +
                $"Final total: ${billing.TotalAmount:N2}",
                "Checkout Complete",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
        else
        {
            MessageBox.Show(
                "Checkout completed successfully!",
                "Success",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        // Refresh grid
        LoadAllCheckInList();
    }
    catch (Exception ex)
    {
        MessageBox.Show(
            $"Error completing checkout: {ex.Message}",
            "Checkout Error",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error
        );
    }
}
```

---

## PHASE 3: Test the System (15 minutes)

### Test Scenarios:

#### ? Scenario 1: Successful Checkout
1. Create reservation
2. Check in guest
3. Pay full amount
4. Click "Check Out" ? Should succeed ?

#### ? Scenario 2: Checkout with Balance (Should Block)
1. Create reservation
2. Check in guest
3. Pay only down payment
4. Click "Check Out" ? Should show error ?
5. Message should show balance due

#### ? Scenario 3: Late Checkout Fee
1. Create reservation (checkout 12:00 PM)
2. Check in guest
3. Pay full amount
4. Checkout at 3:00 PM ? Late fee $150 (3 hours)

---

## PHASE 4: Optional Enhancements

### A. Add Date Filter to UCRooms

```csharp
// Add to UCRooms form
private DateTimePicker dtFilterStart;
private DateTimePicker dtFilterEnd;
private Button btnFilter;

private void InitializeDateFilter()
{
    // ... create controls
    btnFilter.Click += FilterRoomsByDate;
}

private void FilterRoomsByDate(object sender, EventArgs e)
{
    if (dtFilterEnd.Value <= dtFilterStart.Value)
    {
        MessageBox.Show("End date must be after start date.");
        return;
    }

    var available = roomRepository.GetAvailableRoomsByTypeAndDateRange(
        "All", // or selected type
        dtFilterStart.Value,
        dtFilterEnd.Value,
        null
    );

    dataGridRooms.DataSource = available;
}
```

### B. Create Detailed Checkout Form

```csharp
// New form: CheckOutDetailsForm.cs
// Allows staff to add:
// - Service charges
// - Damage fees
// - Discounts
// - Notes
// Then calls PerformCheckout
```

---

## TESTING CHECKLIST

- [ ] Can create reservation for future dates
- [ ] Can see only available rooms for selected dates
- [ ] Cannot book same room for overlapping dates
- [ ] Can check in guest
- [ ] Can pay down payment
- [ ] Can pay full amount
- [ ] **Cannot checkout with balance > 0** ?
- [ ] Can checkout with balance = 0
- [ ] Late checkout fee calculated correctly
- [ ] Room becomes available after checkout
- [ ] Billing record created
- [ ] Can view billing history

---

## QUICK REFERENCE

### Key Classes

```
CheckOutService          - Calculations and validations
BillingModel             - Enhanced billing data
CheckInOutModel          - Check-in/out tracking
ReservationModel         - Reservation data
```

### Key Methods

```
ValidateCheckout()                      - Checks if checkout allowed
CalculateLateCheckoutFee()              - Late fee calculation
PrepareBillingForCheckout()             - Creates billing record
HasOverlappingReservation()             - Checks date conflicts
GetAvailableRoomsByTypeAndDateRange()   - Date-based availability
```

### Critical Validation

```csharp
if (checkIn.BalanceDue > 0)
{
    // BLOCK CHECKOUT
    return;
}
```

---

## ?? EXPECTED RESULTS

After implementation:
- ? Checkout button visible in Check-In/Out module
- ? Clicking checkout validates balance
- ? If balance > 0: Shows error, blocks checkout
- ? If balance = 0: Proceeds with checkout
- ? Late fees calculated automatically
- ? Billing record created
- ? Room status updated to "Available"
- ? System ready for next reservation

---

## ?? TROUBLESHOOTING

### "Cannot find CheckOutService"
**Fix:** Add using statement:
```csharp
using HotelReservationSystem.Domain.Services;
```

### "Property TotalAmount is read-only"
**Fix:** Don't assign directly. Set RoomCharge instead:
```csharp
billing.RoomCharge = totalAmount;
// TotalAmount calculates automatically
```

### "Column ActualCheckOut does not exist"
**Fix:** Run Phase 1 SQL scripts to update database

---

## ?? NEED HELP?

1. Check `CHECKOUT_BILLING_COMPLETE_GUIDE.md` for detailed explanations
2. Check `VALIDATION_RULES_DOCUMENTATION.md` for all validation rules
3. Check `COMPLETE_SYSTEM_SUMMARY.md` for system overview

---

**You're almost done! Just add the UI button and test!** ??

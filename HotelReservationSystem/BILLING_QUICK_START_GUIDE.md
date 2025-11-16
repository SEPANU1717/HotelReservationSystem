# ?? BILLING MODULE - QUICK START GUIDE

## ?? Quick Start

### For Administrators

#### ? Create New Billing Record
1. Click **"Reservation"** button (formerly "Add New")
2. System generates Bill ID automatically
3. Enter or select Reservation ID
4. Review guest information (auto-populated)
5. Enter charge details:
   - Room Charge
   - Late Checkout Fee (if applicable)
   - Damage Fee (if applicable)
6. Enter payment information:
   - Amount Paid Before
   - Amount Paid at Checkout
   - Payment Method (Cash, Card, etc.)
   - Payment Status (Paid, Pending, etc.)
   - Payment Reference (optional)
7. Click **"Save"**

#### ?? Edit Existing Billing Record
1. Select billing record from grid
2. Click **Edit** button (pencil icon)
3. Modify necessary fields
4. Click **"Save"**

#### ??? Delete Billing Record
1. Select billing record from grid
2. Click **Delete** button (trash icon)
3. Confirm deletion

### For Staff/Front Desk

#### ? Create Billing (During Checkout)
1. Same steps as Administrator
2. **Cannot edit or delete** after creation

#### ?? Search & View Billing
1. Enter search term (Customer Name, Room Number, Bill ID)
2. Press Enter or click **"Search"** button
3. View results in grid

---

## ?? Important Notes

### Auto-Calculated Fields
- **Subtotal** = Room Charge + Late Checkout Fee + Damage Fee
- **Total Amount** = Subtotal (same as Subtotal in simplified model)
- **Balance Due** = Total Amount - (Amount Paid Before + Amount Paid at Checkout)

### Payment Status Options
- **Paid** - Full payment received
- **Pending** - Payment not yet received
- **Partial** - Partial payment received
- **Refunded** - Payment refunded to customer

### Payment Method Options
- Cash
- Credit Card
- Debit Card
- Bank Transfer
- Online Payment
- Gcash
- PayMaya

---

## ?? Validation Rules

### Required Fields:
- ? Reservation ID
- ? Customer Name
- ? Room Number
- ? Room Type
- ? Check-In Date
- ? Check-Out Date
- ? Room Charge
- ? Payment Status

### Business Rules:
- ?? Total Amount must be greater than zero
- ?? Check-Out Date must be after Check-In Date
- ?? Payment amounts cannot be negative
- ?? Customer name must be between 2-100 characters
- ?? Room charge must be between $0 and $1,000,000

---

## ?? Permission Matrix

| Action | Admin | Staff |
|--------|-------|-------|
| ? Add Billing | Yes | Yes |
| ?? Edit Billing | Yes | **No** |
| ??? Delete Billing | Yes | **No** |
| ?? View/Search | Yes | Yes |

---

## ?? Common Workflows

### Workflow 1: Standard Checkout Billing
```
1. Guest checks out ? Check-In/Out module
2. System calculates total charges
3. Staff creates billing record
4. Guest pays ? Update "Amount Paid at Checkout"
5. Set Payment Status to "Paid"
6. Click Save
```

### Workflow 2: Late Checkout Billing
```
1. Guest checks out late
2. System calculates Late Checkout Fee automatically
3. Add Late Checkout Fee to billing
4. Process payment
5. Save billing record
```

### Workflow 3: Damage Fee Billing
```
1. Inspect room at checkout
2. Identify damages
3. Calculate Damage Fee
4. Add to billing record
5. Collect payment or mark as Pending
6. Save billing record
```

### Workflow 4: Partial Payment
```
1. Create billing record with full amount
2. Enter partial payment in "Amount Paid at Checkout"
3. Set Payment Status to "Partial"
4. Balance Due shows remaining amount
5. Admin can later edit to add remaining payment
```

---

## ?? Troubleshooting

### Issue: Cannot Edit Billing
**Solution:** Only Administrators can edit billing records. If you're Staff, request Administrator assistance.

### Issue: Balance Due Not Calculating
**Solution:** Ensure all charge and payment fields contain valid numbers (no text, no blank).

### Issue: Cannot Find Billing Record
**Solution:** Try searching by:
- Customer Name
- Room Number
- Bill ID
- Reservation ID

### Issue: Validation Error on Save
**Solution:** Check that:
- All required fields are filled
- Total Amount > 0
- Check-Out Date > Check-In Date
- Payment amounts are not negative

---

## ?? Database Structure

### Billing Table Fields

| Field | Type | Required | Description |
|-------|------|----------|-------------|
| BillId | INT | Yes | Auto-generated unique ID |
| ReservationId | INT | Yes | Link to reservation |
| CustomerName | NVARCHAR(100) | Yes | Guest name |
| RoomType | NVARCHAR(50) | Yes | Room category |
| RoomNumber | NVARCHAR(20) | Yes | Room identifier |
| CheckInDate | DATETIME | Yes | Check-in date |
| CheckOutDate | DATETIME | Yes | Scheduled checkout |
| ActualCheckOutDate | DATETIME | No | Actual checkout time |
| RoomCharge | DECIMAL(18,2) | Yes | Base room charge |
| LateCheckoutFee | DECIMAL(18,2) | No | Late fee if applicable |
| DamageFee | DECIMAL(18,2) | No | Damage charges |
| AmountPaidBefore | DECIMAL(18,2) | No | Advance payment |
| AmountPaidAtCheckout | DECIMAL(18,2) | No | Checkout payment |
| PaymentStatus | NVARCHAR(20) | Yes | Payment state |
| PaymentMethod | NVARCHAR(50) | No | Payment type |
| PaymentReference | NVARCHAR(100) | No | Reference number |
| DateBilled | DATETIME | Yes | Billing creation date |
| BilledBy | NVARCHAR(100) | No | Staff username |

---

## ?? Integration Points

### Check-In/Out Module
- Billing created during checkout process
- Charges calculated from stay duration
- Late fees calculated automatically

### Reservation Module
- Reservation ID links to billing
- Customer information shared
- Room details populated automatically

### Room Module
- Room charges pulled from room rates
- Room type determines base charge

---

## ?? Reporting Capabilities

### Available Data:
- Total bills created
- Paid vs Pending bills
- Revenue by date range
- Payment method breakdown
- Average billing amount
- Late checkout statistics
- Damage fee tracking

---

## ?? Backup & Recovery

### Regular Backups
Database includes all billing records. Ensure regular SQL Server backups.

### Data Retention
All billing records are retained indefinitely for audit purposes.

---

## ?? Training Resources

### For New Staff:
1. Review this Quick Start Guide
2. Practice creating test billing records
3. Understand authorization limitations
4. Learn validation rules

### For Administrators:
1. Complete staff training
2. Learn edit/delete procedures
3. Understand reporting capabilities
4. Review audit trails

---

## ?? Support

### Technical Issues:
- Check build is successful
- Verify database connection
- Review error messages in UI

### Business Logic Questions:
- Refer to Business Rules section
- Check Validation Rules
- Review Permission Matrix

---

## ? Pre-Deployment Checklist

- [ ] Database migration completed (`Database_Migration_v2.sql`)
- [ ] Build compiles successfully
- [ ] Admin account tested (all operations)
- [ ] Staff account tested (limited operations)
- [ ] Validation messages tested
- [ ] Auto-calculation verified
- [ ] Search functionality tested
- [ ] Grid display working
- [ ] Authorization enforced
- [ ] User session working

---

## ?? Success Metrics

### For Management:
- Billing creation time < 2 minutes
- Error rate < 5%
- Staff satisfaction > 90%
- Audit trail complete

### For IT:
- Zero runtime errors
- Response time < 1 second
- Database queries optimized
- Memory leaks eliminated

---

*Version: 3.0*
*Last Updated: [Current Date]*
*For: Hotel Reservation System*

# Hotel Reservation System - Billing Model

This document describes the comprehensive billing model system for the Hotel Reservation System, providing detailed information about features, usage, and implementation.

## Overview

The billing model system provides a complete solution for managing hotel bills, payments, and financial transactions. It includes:

- **Comprehensive billing with itemized charges**
- **Multiple payment method support**
- **Advanced discount management**
- **Payment tracking and transaction history**
- **Automated calculations and validations**
- **Reporting and analytics capabilities**

## Core Components

### 1. BillingModel.cs
The main billing entity that represents a customer's bill.

**Key Features:**
- Room charges with automatic night calculation
- Itemized additional services
- Tax calculations with configurable rates
- Discount management (percentage and fixed amount)
- Payment tracking with balance due calculations
- Invoice number generation
- Audit fields (created/updated timestamps)

**Key Properties:**
```csharp
// Basic Information
int BillId, ReservationId, CustomerId
string CustomerName, RoomNumber, InvoiceNumber

// Stay Details
DateTime CheckInDate, CheckOutDate
int Nights
decimal RoomRate, RoomSubtotal

// Financial Calculations
decimal Subtotal, TaxRate, TaxAmount
decimal DiscountAmount, TotalAmount
decimal AmountPaid, BalanceDue

// Payment Information
string PaymentMethod, PaymentStatus
DateTime? DueDate, DatePaid

// Collections
List<BillingLineItem> LineItems
List<PaymentTransaction> PaymentTransactions
```

### 2. BillingLineItem.cs
Represents individual charges and services added to a bill.

**Features:**
- Flexible quantity and unit pricing
- Categorization for reporting
- Taxable/non-taxable items
- Service date tracking
- Automatic total calculation

### 3. PaymentTransaction.cs
Tracks individual payment transactions for complete payment history.

**Features:**
- Multiple payment methods support
- Authorization and reference tracking
- Processing fee calculations
- Card information storage (last 4 digits)
- Gateway integration support
- Transaction status tracking

### 4. BillingEnums.cs
Provides type-safe enumerations for:

- **PaymentMethod**: Cash, CreditCard, DebitCard, BankTransfer, etc.
- **PaymentStatus**: Pending, Partial, Paid, Overdue, Cancelled, etc.
- **TransactionType**: Payment, Refund, Chargeback, etc.
- **BillingItemType**: Room, FoodBeverage, SpaServices, Parking, etc.
- **DiscountType**: Percentage, FixedAmount, SeniorDiscount, etc.

### 5. BillingService.cs
Business logic service providing billing operations.

**Key Methods:**
- `CreateBillFromReservation()` - Generate bill from reservation
- `AddServiceCharge()` - Add additional services
- `ProcessPayment()` - Handle payment transactions
- `ApplyDiscount()` - Apply various discount types
- `ProcessRefund()` - Handle refund transactions
- `ValidateBill()` - Validate bill data
- `GetBillingSummary()` - Generate reporting summaries

## Usage Examples

### Creating a Bill from Reservation

```csharp
var billingService = new BillingService();

// Create bill from existing reservation, customer, and room data
var bill = billingService.CreateBillFromReservation(reservation, customer, room);

// Bill is automatically calculated with:
// - Room rate × nights
// - Tax calculations
// - Invoice number generation
// - Due date setting
```

### Adding Additional Services

```csharp
// Add room service charge
billingService.AddServiceCharge(bill, BillingItemType.RoomService, 
    "Breakfast - Continental", quantity: 2, unitPrice: 25.00m);

// Add spa service
billingService.AddServiceCharge(bill, BillingItemType.SpaServices, 
    "60-minute Swedish Massage", quantity: 1, unitPrice: 120.00m);

// Add parking with non-taxable option
billingService.AddServiceCharge(bill, BillingItemType.Parking, 
    "Valet Parking", quantity: 3, unitPrice: 25.00m, isTaxable: false);
```

### Processing Payments

```csharp
// Process credit card payment
var payment = billingService.ProcessPayment(bill, 
    PaymentMethod.CreditCard, 
    amount: 500.00m, 
    processedBy: "Front Desk Clerk",
    authorizationCode: "AUTH123456", 
    cardLastFour: "1234");

// Process cash payment for remaining balance
var cashPayment = billingService.ProcessPayment(bill, 
    PaymentMethod.Cash, 
    bill.BalanceDue, 
    "Manager");
```

### Applying Discounts

```csharp
// Apply percentage discount
billingService.ApplyDiscount(bill, DiscountType.LoyaltyMember, 
    discountValue: 10, reason: "Gold member discount");

// Apply fixed amount discount
billingService.ApplyDiscount(bill, DiscountType.FixedAmount, 
    discountValue: 50, reason: "Promotional coupon");

// Apply senior discount
billingService.ApplyDiscount(bill, DiscountType.SeniorDiscount, 
    discountValue: 15, reason: "Senior citizen discount");
```

### Processing Refunds

```csharp
// Process partial refund
var refund = billingService.ProcessRefund(bill, 
    refundAmount: 50.00m, 
    reason: "Minibar items not consumed", 
    processedBy: "Manager");

// Refund automatically updates bill status and payment history
```

## Validation and Business Rules

### Automatic Validations
- Check-out date must be after check-in date
- Room rate cannot be negative
- Number of nights must be at least 1
- Tax rate must be between 0 and 100%
- Discount cannot exceed subtotal
- Required fields validation

### Business Logic
- Automatic night calculation based on dates
- Tax calculations applied to taxable items only
- Balance due automatically calculated
- Payment status updates based on payments
- Processing fees calculated for card payments
- Invoice number generation with year prefix

## Financial Calculations

### Calculation Flow
1. **Room Subtotal** = Room Rate × Nights
2. **Additional Services** = Sum of all line items (excluding room)
3. **Subtotal** = Room Subtotal + Additional Services
4. **Tax Amount** = Subtotal × Tax Rate
5. **Total Amount** = Subtotal + Tax Amount - Discount Amount
6. **Balance Due** = Total Amount - Amount Paid

### Tax Handling
- Configurable tax rate per bill
- Individual line items can be marked as taxable/non-taxable
- Tax only applied to taxable items
- Tax calculated after discounts applied

## Reporting and Analytics

### BillingSummary Features
```csharp
var summary = billingService.GetBillingSummary(bills, startDate, endDate);

// Available metrics:
// - Total Bills, Total Revenue, Total Paid
// - Average Ticket Size
// - Payment Rate (% of bills fully paid)
// - Collection Rate (% of revenue collected)
// - Overdue Bills Count
```

## Integration Points

### With Existing Models
- **ReservationModel**: Bill creation from reservation data
- **CustomerModel**: Customer information and billing address
- **RoomModel**: Room pricing and details

### Database Considerations
The models are designed to work with Entity Framework or similar ORM:
- Primary key properties (BillId, LineItemId, TransactionId)
- Foreign key relationships (ReservationId, CustomerId, BillId)
- Data annotations for validation
- Computed properties for business logic

## Best Practices

### Usage Recommendations
1. **Always validate bills** before processing payments
2. **Use the BillingService** for all business operations
3. **Store payment references** for audit trails
4. **Apply discounts before processing payments**
5. **Use enums** instead of magic strings for consistency
6. **Implement proper error handling** for payment processing

### Security Considerations
1. **Never store full credit card numbers**
2. **Encrypt sensitive payment data**
3. **Log all financial transactions**
4. **Implement proper access controls**
5. **Use secure payment gateways**

### Performance Tips
1. **Load related data efficiently** (use includes for LineItems/Transactions)
2. **Consider pagination** for large bill lists
3. **Cache frequently accessed data** (tax rates, service prices)
4. **Use bulk operations** for reporting queries

## Extension Points

The billing system is designed for extensibility:

1. **Custom Payment Gateways**: Implement payment processing interfaces
2. **Additional Item Types**: Extend BillingItemType enum
3. **Custom Discount Rules**: Add new discount calculation logic
4. **Reporting Modules**: Build custom reports using BillingSummary
5. **Integration APIs**: Expose billing operations via REST/GraphQL

## Testing

See `BillingExample.cs` for comprehensive usage examples including:
- Complete billing workflow demonstration
- Refund processing examples
- Discount scenario testing
- Validation testing

## Support

For questions or issues with the billing model system:
1. Review the example code in `BillingExample.cs`
2. Check validation rules in `BillingService.ValidateBill()`
3. Ensure proper enum usage from `BillingEnums.cs`
4. Verify business logic in calculation methods
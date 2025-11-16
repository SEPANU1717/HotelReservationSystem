# Hotel Reservation System - Validation Rules Documentation

## Overview
This document outlines all validation rules implemented in the Hotel Reservation System following MVP (Model-View-Presenter) architecture.

---

## 1. RESERVATION VALIDATION

### Date Validations
? **Check-in Date**
- Cannot be in the past
- Must be today or a future date
- Format: MM/DD/YYYY

? **Check-Out Date**
- Must be after check-in date
- Maximum reservation period: 365 days (1 year)
- Minimum stay: 1 night

? **Date Range Overlap**
- System automatically checks for overlapping reservations
- Same room cannot be booked for overlapping dates
- Allows multiple reservations for the same room on different dates

### Room Validations
? **Room Selection**
- Room type is required
- Room number must be selected
- Only shows rooms available for selected date range
- Prevents double-booking automatically

### Payment Validations
? **Total Price**
- Must be greater than $0.00
- Range: $0.01 to $1,000,000.00
- Cannot be negative

? **Amount Paid**
- Cannot be negative
- Cannot exceed total price
- Must be between $0.00 and total price

? **Down Payment**
- Cannot be negative
- Cannot exceed total price
- Automatically calculated as 50% of total price
- Can be modified by user

? **Balance Due**
- Automatically calculated: Total Price - Amount Paid
- Read-only field

### Customer Validations
? **Customer Name**
- Required field
- Must be between 2 and 100 characters
- Cannot be empty or whitespace only

---

## 2. CUSTOMER VALIDATION

### Personal Information
? **First Name**
- Required field
- Length: 2-50 characters
- Only letters, spaces, hyphens, and apostrophes allowed
- Pattern: `^[a-zA-Z\s\-']+$`

? **Last Name**
- Required field
- Length: 2-50 characters
- Only letters, spaces, hyphens, and apostrophes allowed
- Pattern: `^[a-zA-Z\s\-']+$`

? **Middle Name**
- Optional field
- Maximum length: 50 characters
- Only letters, spaces, hyphens, and apostrophes allowed

### Age Requirement ?
? **Date of Birth**
- Required field
- **Customer must be at least 18 years old**
- Error message displays current age and birth date
- Validation message: "Customer must be at least 18 years old to make a reservation"

? **Age Calculation**
- Automatically calculated from birth date
- Accounts for leap years
- Accurate to the day

### Contact Information
? **Contact Number**
- Required field
- Must contain at least 10 digits
- Can include: numbers, spaces, hyphens, plus sign, parentheses
- Pattern: `^[\d\s\-\+\(\)]+$`
- Phone number format validation

? **Email**
- Required field
- Must be valid email format
- Maximum length: 100 characters
- Must be unique (no duplicates)
- Standard email validation pattern

? **Address**
- Required field
- Length: 5-200 characters
- Cannot be too short or too long

### Additional Fields
? **ID Type**
- Required field
- Common values: Passport, Driver's License, National ID

? **Gender**
- Required field
- Standard values: Male, Female, Other

? **Nationality**
- Required field
- Length: 2-50 characters

? **Notes**
- Optional field
- Maximum length: 500 characters

---

## 3. USER MANAGEMENT VALIDATION (Settings)

### Personal Information
? **First Name**
- Required field
- Length: 2-50 characters
- Only letters, spaces, hyphens, and apostrophes allowed
- Pattern: `^[a-zA-Z\s\-']+$`

? **Last Name**
- Required field
- Length: 2-50 characters
- Only letters, spaces, hyphens, and apostrophes allowed
- Pattern: `^[a-zA-Z\s\-']+$`

### Age Requirement ?
? **Birth Date**
- Required field
- **User must be at least 18 years old to register**
- Error message displays current age and birth date
- Validation message: "User must be at least 18 years old to register"

### Account Security
? **Username**
- Required field
- Length: 3-50 characters
- Only letters, numbers, and underscores allowed
- Pattern: `^[a-zA-Z0-9_]+$`
- Must be unique (no duplicates)
- Case-insensitive uniqueness check

? **Password**
- Required for new users
- Minimum length: 6 characters
- Maximum length: 100 characters
- Securely hashed before storage
- For existing users: optional to change

? **Email**
- Required field
- Must be valid email format
- Maximum length: 100 characters
- Email address validation

? **Gender**
- Required field

? **Role**
- Required field
- Determines user permissions

---

## 4. ROOM AVAILABILITY LOGIC

### Date-Based Availability ?
? **Smart Room Selection**
- Rooms shown based on selected check-in and check-out dates
- Same room can be reserved for different date ranges
- Example: Room 101 can be booked:
  - Nov 16-18, 2025 (Customer A)
  - Nov 20-22, 2025 (Customer B)
  - Nov 25-27, 2025 (Customer C)

? **Overlap Detection**
- Automatically prevents double-booking
- Checks for date conflicts before saving
- SQL query ensures no overlapping reservations
- Excludes cancelled and checked-out reservations

? **Room Status Sync**
- Room status updates based on current date and active reservations
- Available: No current reservations
- Reserved: Has reservation for current date
- Occupied: Guest has checked in
- Future reservations don't affect current availability

---

## 5. VALIDATION ARCHITECTURE (MVP Pattern)

### Model Layer
- **Data Annotations**: Attribute-based validation on model properties
- **Custom Attributes**:
  - `MinimumAgeAttribute`: Enforces 18+ age requirement
  - `DateRangeValidationAttribute`: Validates check-out after check-in
  - `FutureDateAttribute`: Prevents past dates
  - `DecimalRangeAttribute`: Validates monetary amounts

### Presenter Layer
- **Business Rule Validation**: Complex validation logic
- **Cross-field Validation**: Validates relationships between fields
- **Database Validation**: Checks for duplicates and conflicts
- **User-Friendly Error Messages**: Clear, actionable error messages

### View Layer
- **Input Validation**: Real-time feedback to user
- **Field Enabling/Disabling**: Prevents invalid state
- **Error Display**: MessageBox dialogs with clear explanations

---

## 6. ERROR MESSAGES

### User-Friendly Messages ?
All error messages follow these principles:
1. **Clear**: Explains what went wrong
2. **Specific**: Shows exact values that failed validation
3. **Actionable**: Tells user how to fix the issue
4. **Professional**: Polite and helpful tone

### Examples:

**Age Validation**
```
Customer must be at least 18 years old to make a reservation.

Current age: 16 years old
Date of birth: 03/15/2008

Customers under 18 are not permitted to book rooms.
```

**Date Overlap**
```
Room 101 is already reserved for the selected dates.

Check-in: 11/16/2025
Check-out: 11/18/2025

Please select a different room or change the dates.
```

**Payment Validation**
```
Amount paid ($1,500.00) cannot exceed total price ($1,200.00).
```

---

## 7. VALIDATION SUMMARY

| Category | Total Rules | Critical Rules |
|----------|-------------|----------------|
| Reservation | 15 | Date overlap, Payment amounts |
| Customer | 12 | **18+ Age requirement** |
| User Management | 10 | **18+ Age requirement**, Username uniqueness |
| Room Availability | 5 | Date-based availability |
| **TOTAL** | **42** | **4 Critical** |

---

## 8. TESTING CHECKLIST

### Reservation Module
- [ ] Try to book past dates
- [ ] Try to book same room for overlapping dates
- [ ] Try negative payment amounts
- [ ] Try amount paid > total price
- [ ] Book same room for different dates (should work)

### Customer Module
- [ ] Try to add customer under 18 years old
- [ ] Try invalid email format
- [ ] Try duplicate email
- [ ] Try invalid phone number

### User Management
- [ ] Try to register user under 18 years old
- [ ] Try duplicate username
- [ ] Try password less than 6 characters
- [ ] Try invalid username characters

---

## 9. IMPLEMENTATION NOTES

? **All validations follow MVP pattern**
? **Validation logic is in Presenter layer**
? **Models use Data Annotations**
? **Custom validation attributes for complex rules**
? **Database queries ensure data integrity**
? **User-friendly error messages**
? **Build successful - No errors**

---

**Last Updated**: December 2024
**Version**: 2.0
**Status**: ? Production Ready

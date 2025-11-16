# Quick Validation Reference Guide

## ?? CRITICAL: 18+ AGE REQUIREMENT

### Where It Applies:
1. **Customer Registration** ? (for hotel bookings)
2. **User Registration** ? (staff/admin accounts)

### Error Messages:
```
Customer Module:
"Customer must be at least 18 years old to make a reservation.
Current age: XX years old
Date of birth: MM/DD/YYYY
Customers under 18 are not permitted to book rooms."

User Management:
"User must be at least 18 years old to register.
Current age: XX years old  
Birth date: MM/DD/YYYY"
```

---

## ?? DATE-BASED ROOM RESERVATIONS

### How It Works:
? **Same room, different dates = ? ALLOWED**
- Room 101: Nov 16-18 (Guest A)
- Room 101: Nov 20-22 (Guest B)  ? This works!
- Room 101: Nov 17-19 (Guest C)  ? This fails (overlaps with A)

? **Automatic Conflict Detection**
- System checks database for overlapping dates
- Shows only available rooms for selected dates
- Clear error message if conflict exists

---

## ?? PAYMENT RULES

? **Amount Paid** ? **Total Price**
? **Down Payment** ? **Total Price**
? **Balance Due** = Total Price - Amount Paid (auto-calculated)
? All amounts must be ? $0.00

---

## ?? FIELD VALIDATION SUMMARY

### Required Fields:
| Module | Required Fields |
|--------|----------------|
| Reservation | Customer Name, Room Type, Room Number, Check-in, Check-out, Total Amount, Status |
| Customer | First Name, Last Name, ID Type, Contact, Address, Email, **Birth Date (18+)**, Gender, Nationality |
| User | First Name, Last Name, **Birth Date (18+)**, Username, Password, Email, Gender, Role |

### Field Length Limits:
- Names: 2-50 characters
- Email: Max 100 characters
- Address: 5-200 characters
- Username: 3-50 characters
- Password: Min 6 characters
- Notes: Max 500 characters

### Special Patterns:
- Names: Letters, spaces, hyphens, apostrophes only
- Username: Letters, numbers, underscores only
- Phone: Must have at least 10 digits
- Email: Valid email format

---

## ?? COMMON VALIDATION ERRORS & FIXES

### "User must be at least 18 years old"
**Fix:** Enter a birth date that makes the person 18+ years old

### "Room is already reserved for the selected dates"
**Fix:** 
1. Select different dates, OR
2. Select a different room

### "Amount paid cannot exceed total price"
**Fix:** Reduce the amount paid to be ? total price

### "Username is already taken"
**Fix:** Choose a different username

### "Email is already registered"
**Fix:** Use a different email address

### "Check-out date must be after check-in date"
**Fix:** Select a check-out date that's after check-in

### "Password must be at least 6 characters"
**Fix:** Enter a longer password (6+ characters)

---

## ?? VALIDATION FLOW

```
User Input ? View Layer ? Presenter Layer ? Validation
                              ?
                    ?????????????????????
                    ?                   ?
            Business Rules      Data Annotations
            (Presenter)         (Model)
                    ?                   ?
                    ?????????????????????
                              ?
                    ? Success: Save to DB
                    ? Error: Show Message
```

---

## ?? SUPPORT

If validation fails unexpectedly:
1. Check error message carefully
2. Verify all required fields are filled
3. Check date formats (MM/DD/YYYY)
4. Ensure age is 18+ for customers/users
5. Verify no date overlaps for room bookings

**All validation is working correctly! ?**

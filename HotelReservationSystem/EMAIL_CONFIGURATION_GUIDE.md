# Email Configuration Guide

## ? AUTOMATIC EMAIL CONFIGURATION (RECOMMENDED)

Your SMTP settings are now configured in `App.config` and will be used automatically!

### Current Configuration:
```xml
<appSettings>
    <add key="SmtpHost" value="smtp.gmail.com"/>
    <add key="SmtpPort" value="587"/>
    <add key="SmtpFromEmail" value="markmanalo1717@gmail.com"/>
    <add key="SmtpPassword" value="jctoqwnoidoryavh"/>
    <add key="SmtpEnableSsl" value="true"/>
    <add key="SmtpDisplayName" value="Hotel Reservation System"/>
</appSettings>
```

### How It Works:
1. ? **No manual entry needed** - Settings are read from App.config automatically
2. ? **Just double-click billing record** - Email is sent directly
3. ? **Fallback option** - If App.config is empty, manual entry dialog appears

---

## ?? CONFIGURATION OPTIONS

### Option 1: Use App.config (AUTOMATIC) ? RECOMMENDED

**Location:** `HotelReservationSystem/App.config`

```xml
<appSettings>
    <add key="SmtpHost" value="smtp.gmail.com"/>
    <add key="SmtpPort" value="587"/>
    <add key="SmtpFromEmail" value="your-email@gmail.com"/>
    <add key="SmtpPassword" value="your-app-password"/>
    <add key="SmtpEnableSsl" value="true"/>
    <add key="SmtpDisplayName" value="Lodgix Hotel"/>
</appSettings>
```

**Advantages:**
- ? No need to enter credentials every time
- ? One-time configuration
- ? Easy to change without recompiling
- ? Professional approach
- ? Secure (not in source code)

### Option 2: Manual Entry (FALLBACK)

If App.config settings are not configured, the system will show a dialog to enter settings manually.

---

## ?? GMAIL CONFIGURATION

### Step 1: Enable 2-Step Verification
1. Go to https://myaccount.google.com/security
2. Enable "2-Step Verification"

### Step 2: Generate App Password
1. Go to https://myaccount.google.com/apppasswords
2. Select "Mail" and your device
3. Click "Generate"
4. Copy the 16-character password (e.g., `jctoqwnoidoryavh`)

### Step 3: Update App.config
```xml
<add key="SmtpHost" value="smtp.gmail.com"/>
<add key="SmtpPort" value="587"/>
<add key="SmtpFromEmail" value="YOUR_EMAIL@gmail.com"/>
<add key="SmtpPassword" value="YOUR_APP_PASSWORD"/>
<add key="SmtpEnableSsl" value="true"/>
```

---

## ?? OUTLOOK/HOTMAIL CONFIGURATION

```xml
<add key="SmtpHost" value="smtp-mail.outlook.com"/>
<add key="SmtpPort" value="587"/>
<add key="SmtpFromEmail" value="your-email@outlook.com"/>
<add key="SmtpPassword" value="your-password"/>
<add key="SmtpEnableSsl" value="true"/>
```

---

## ?? YAHOO CONFIGURATION

```xml
<add key="SmtpHost" value="smtp.mail.yahoo.com"/>
<add key="SmtpPort" value="587"/>
<add key="SmtpFromEmail" value="your-email@yahoo.com"/>
<add key="SmtpPassword" value="your-app-password"/>
<add key="SmtpEnableSsl" value="true"/>
```

**Note:** Yahoo also requires App Password (not regular password)

---

## ?? OFFICE 365 CONFIGURATION

```xml
<add key="SmtpHost" value="smtp.office365.com"/>
<add key="SmtpPort" value="587"/>
<add key="SmtpFromEmail" value="your-email@yourdomain.com"/>
<add key="SmtpPassword" value="your-password"/>
<add key="SmtpEnableSsl" value="true"/>
```

---

## ?? USAGE AFTER CONFIGURATION

### Sending Invoice Email:

**Before Configuration:**
1. Select billing record
2. Double-click
3. Enter SMTP settings manually
4. Click Send

**After App.config Configuration (WITH CONFIRMATION):**
1. Select billing record
2. Double-click
3. ? **Confirmation dialog appears:**
   ```
   Send invoice email to customer?
   
   Customer: John Doe
   Email: john.doe@example.com
   Invoice #: 100
   Amount: $250.00
   
   Do you want to proceed?
   [Yes] [No]
   ```
4. Review details and click **Yes**
5. ? **Email sent automatically!**

**Safety Features:**
- ? Shows customer name and email before sending
- ? Shows invoice number and amount
- ? Requires explicit confirmation
- ? Can cancel at any time

No more accidental sends! ??

---

## ?? SECURITY BEST PRACTICES

### ? DO:
- Use App Password for Gmail/Yahoo (not your account password)
- Keep App.config secure (don't share publicly)
- Use strong passwords
- Enable 2-Step Verification

### ? DON'T:
- Don't commit App.config with real passwords to public repositories
- Don't share your App Password
- Don't use regular Gmail password (use App Password)

---

## ?? TROUBLESHOOTING

### "Email configuration is not set"
**Solution:** Check App.config has all required keys with values

### "SMTP Authentication Failed"
**Solution:** 
- For Gmail: Use App Password, not regular password
- Verify email and password are correct
- Check 2-Step Verification is enabled

### "Failed to send email via SMTP"
**Solution:**
- Check internet connection
- Verify SMTP server and port are correct
- Check if firewall is blocking port 587

### Emails not sending automatically
**Solution:**
1. Open `App.config`
2. Verify `SmtpFromEmail` and `SmtpPassword` have values
3. Rebuild the application

---

## ?? EXAMPLE: Testing Your Configuration

### Test 1: Check if configured
```csharp
var emailService = new EmailService();
if (emailService.IsConfigured())
{
    Console.WriteLine("? Email is configured!");
}
else
{
    Console.WriteLine("? Email is NOT configured");
}
```

### Test 2: Send test email
1. Go to Billing module
2. Select any billing record
3. Double-click the record
4. If configured correctly: Email sent automatically
5. If not configured: Manual entry dialog appears

---

## ?? SWITCHING BETWEEN ACCOUNTS

To switch from one email account to another:

1. Open `App.config`
2. Update these values:
```xml
<add key="SmtpFromEmail" value="NEW_EMAIL@gmail.com"/>
<add key="SmtpPassword" value="NEW_APP_PASSWORD"/>
```
3. Save the file
4. Restart the application

**No recompilation needed!**

---

## ?? CONFIGURATION COMPARISON

| Method | Setup Time | Ease of Use | Security | Recommended |
|--------|------------|-------------|----------|-------------|
| **App.config** | 5 minutes | ????? | ???? | ? YES |
| **Manual Entry** | Every time | ?? | ????? | ? NO |

---

## ? WHAT CHANGED

### Before:
```csharp
// User had to enter SMTP settings every time
EmailConfigForm form = new EmailConfigForm();
form.ShowDialog();
// ... send email
```

### After:
```csharp
// Settings automatically loaded from App.config
EmailService emailService = new EmailService();
emailService.SendInvoiceEmail(...);
// ? Done! No dialog needed
```

---

## ?? YOU'RE ALL SET!

Your current configuration:
- ? SMTP Host: `smtp.gmail.com`
- ? SMTP Port: `587`
- ? Sender Email: `markmanalo1717@gmail.com`
- ? Password: Configured (App Password)
- ? SSL: Enabled
- ? Display Name: `Hotel Reservation System`

**Just double-click any billing record to send invoice - no more manual entry! ??**

---

## ?? NEED HELP?

If emails are not sending:
1. Check App.config has correct values
2. Verify Gmail App Password is correct
3. Check internet connection
4. Verify customer has email address in database
5. Check Output window for error messages

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace HotelReservationSystem.Domain.Interface.UserManagement.Email_Service
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromPassword;
        private readonly bool _enableSsl;
        private readonly string _displayName;

        public SmtpEmailService()
        {
            _smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            _smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"] ?? "587");
            _fromEmail = ConfigurationManager.AppSettings["SmtpFromEmail"];
            _fromPassword = ConfigurationManager.AppSettings["SmtpPassword"];
            _enableSsl = bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"] ?? "true");
            _displayName = ConfigurationManager.AppSettings["SmtpDisplayName"] ?? "Hotel Reservation System";

            if (string.IsNullOrEmpty(_smtpHost) || string.IsNullOrEmpty(_fromEmail) || string.IsNullOrEmpty(_fromPassword))
            {
                throw new InvalidOperationException("SMTP configuration is missing in App.config");
            }
        }

        public async Task<bool> SendPasswordResetEmailAsync(string toEmail, string resetToken, string username)
        {
            string subject = "Password Reset Request - Hotel Reservation System";
            string body = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px; }}
        .container {{ background-color: #ffffff; padding: 30px; border-radius: 10px; max-width: 600px; margin: 0 auto; box-shadow: 0 2px 4px rgba(0,0,0,0.1); }}
        .header {{ background-color: #6558f5; color: white; padding: 20px; border-radius: 10px 10px 0 0; text-align: center; }}
        .content {{ padding: 20px; }}
        .code-box {{ background-color: #f8f9fa; border: 2px dashed #6558f5; padding: 20px; text-align: center; margin: 20px 0; border-radius: 5px; }}
        .code {{ font-size: 32px; font-weight: bold; color: #6558f5; letter-spacing: 5px; font-family: 'Courier New', monospace; }}
        .warning {{ color: #dc3545; font-size: 12px; margin-top: 10px; }}
        .footer {{ text-align: center; padding: 20px; color: #666; font-size: 12px; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>Password Reset Request</h1>
        </div>
        <div class='content'>
            <p>Hello <strong>{username}</strong>,</p>
            <p>You have requested to reset your password for your Hotel Reservation System account.</p>
            <p>Use the following verification code to reset your password:</p>
            
            <div class='code-box'>
                <div class='code'>{resetToken}</div>
                <div class='warning'>⏰ This code will expire in 15 minutes</div>
            </div>
            
            <p><strong>Security Tips:</strong></p>
            <ul>
                <li>Never share this code with anyone</li>
                <li>If you didn't request this reset, please ignore this email</li>
                <li>Change your password immediately if you suspect unauthorized access</li>
            </ul>
            
            <p>Best regards,<br/>
            <strong>Hotel Management Team</strong></p>
        </div>
        <div class='footer'>
            <p>This is an automated email. Please do not reply to this message.</p>
            <p>&copy; {DateTime.Now.Year} Hotel Reservation System. All rights reserved.</p>
        </div>
    </div>
</body>
</html>";

            return await SendEmailAsync(toEmail, subject, body);
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
        {
            try
            {
                using (var smtpClient = new SmtpClient(_smtpHost, _smtpPort))
                {
                    smtpClient.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
                    smtpClient.EnableSsl = _enableSsl;
                    smtpClient.Timeout = 30000; // 30 seconds

                    using (var mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(_fromEmail, _displayName);
                        mailMessage.To.Add(toEmail);
                        mailMessage.Subject = subject;
                        mailMessage.Body = body;
                        mailMessage.IsBodyHtml = true;
                        mailMessage.Priority = MailPriority.High;

                        await smtpClient.SendMailAsync(mailMessage);
                    }
                }
                return true;
            }
            catch (SmtpException smtpEx)
            {
                System.Diagnostics.Debug.WriteLine($"SMTP Error: {smtpEx.Message}");
                throw new InvalidOperationException($"Failed to send email: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Email send failed: {ex.Message}");
                throw new InvalidOperationException($"Email service error: {ex.Message}", ex);
            }
        }
    }
}

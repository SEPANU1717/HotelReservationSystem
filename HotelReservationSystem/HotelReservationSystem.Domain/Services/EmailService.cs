using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace HotelReservationSystem.Domain.Services
{
    public class EmailService
    {
        private readonly string smtpServer;
        private readonly int smtpPort;
        private readonly string senderEmail;
        private readonly string senderPassword;
        private readonly bool enableSsl;
        private readonly string displayName;

        public EmailService()
        {
            this.smtpServer = ConfigurationManager.AppSettings["SmtpHost"] ?? "smtp.gmail.com";
            this.smtpPort = int.TryParse(ConfigurationManager.AppSettings["SmtpPort"], out int port) ? port : 587;
            this.senderEmail = ConfigurationManager.AppSettings["SmtpFromEmail"] ?? string.Empty;
            this.senderPassword = ConfigurationManager.AppSettings["SmtpPassword"] ?? string.Empty;
            this.enableSsl = bool.TryParse(ConfigurationManager.AppSettings["SmtpEnableSsl"], out bool ssl) ? ssl : true;
            this.displayName = ConfigurationManager.AppSettings["SmtpDisplayName"] ?? "Lodgix Hotel";
        }

        public EmailService(string smtpServer, int smtpPort, string senderEmail, string senderPassword, bool enableSsl)
        {
            this.smtpServer = smtpServer;
            this.smtpPort = smtpPort;
            this.senderEmail = senderEmail;
            this.senderPassword = senderPassword;
            this.enableSsl = enableSsl;
            this.displayName = "Lodgix Hotel";
        }

        public bool IsConfigured()
        {
            return !string.IsNullOrEmpty(senderEmail) && !string.IsNullOrEmpty(senderPassword);
        }

        public void SendInvoiceEmail(string recipientEmail, string recipientName, string invoiceFilePath, string billId)
        {
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
            {
                throw new InvalidOperationException("Email configuration is not set. Please configure SMTP settings in App.config.");
            }

            if (!File.Exists(invoiceFilePath))
            {
                throw new FileNotFoundException("Invoice file not found.", invoiceFilePath);
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, displayName);
                mail.To.Add(new MailAddress(recipientEmail, recipientName));
                mail.Subject = $"Invoice #{billId} - Lodgix Hotel";
                mail.IsBodyHtml = true;

                mail.Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <div style='background-color: #6558f5; color: white; padding: 20px; text-align: center;'>
                                <h1 style='margin: 0;'>LODGIX HOTEL</h1>
                                <p style='margin: 5px 0;'>Hotel Reservation System</p>
                            </div>
                            
                            <div style='padding: 30px; background-color: #f9f9f9;'>
                                <h2 style='color: #6558f5;'>Dear {recipientName},</h2>
                                <p>Thank you for staying with us at Lodgix Hotel.</p>
                                <p>Please find attached your invoice (Invoice #{billId}) for your recent stay.</p>
                                <p>If you have any questions regarding this invoice, please don't hesitate to contact us.</p>

                                <div style='margin: 30px 0; padding: 20px; background-color: white; border-left: 4px solid #6558f5;'>
                                    <p style='margin: 5px 0;'><strong>Invoice Number:</strong> {billId}</p>
                                    <p style='margin: 5px 0;'><strong>Date:</strong> {DateTime.Now:MMMM dd, yyyy}</p>
                                </div>
                                
                                <p style='color: #666; font-size: 14px;'>
                                    We look forward to welcoming you back soon!
                                </p>
                            </div>
                            
                            <div style='background-color: #6558f5; color: white; padding: 15px; text-align: center; font-size: 12px;'>
                                <p style='margin: 5px 0;'>Lodgix Hotel Reservation System</p>
                                <p style='margin: 5px 0;'>This is an automated email. Please do not reply.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                ";

                Attachment attachment = new Attachment(invoiceFilePath);
                mail.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient(smtpServer);
                smtp.Port = smtpPort;
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = enableSsl;

                smtp.Send(mail);

                attachment.Dispose();
                mail.Dispose();
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"Failed to send email via SMTP: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send email: {ex.Message}", ex);
            }
        }

        public void SendReservationReceiptEmail(string recipientEmail, string recipientName, string receiptFilePath, string reservationId)
        {
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
            {
                throw new InvalidOperationException("Email configuration is not set. Please configure SMTP settings in App.config.");
            }

            if (!File.Exists(receiptFilePath))
            {
                throw new FileNotFoundException("Reservation receipt file not found.", receiptFilePath);
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, displayName);
                mail.To.Add(new MailAddress(recipientEmail, recipientName));
                mail.Subject = $"Reservation Confirmation #{reservationId} - Lodgix Hotel";
                mail.IsBodyHtml = true;

                mail.Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <div style='background-color: #6558f5; color: white; padding: 20px; text-align: center;'>
                                <h1 style='margin: 0;'>LODGIX HOTEL</h1>
                                <p style='margin: 5px 0;'>Hotel Reservation System</p>
                            </div>
                            
                            <div style='padding: 30px; background-color: #f9f9f9;'>
                                <h2 style='color: #6558f5;'>Dear {recipientName},</h2>
                                <p>Thank you for choosing Lodgix Hotel!</p>
                                <p>Your reservation has been confirmed. Please find attached your reservation receipt (Reservation #{reservationId}).</p>
                                <p>Please keep this receipt as proof of your reservation. Present it at check-in along with a valid ID.</p>

                                <div style='margin: 30px 0; padding: 20px; background-color: white; border-left: 4px solid #6558f5;'>
                                    <p style='margin: 5px 0;'><strong>Reservation Number:</strong> {reservationId}</p>
                                    <p style='margin: 5px 0;'><strong>Confirmation Date:</strong> {DateTime.Now:MMMM dd, yyyy}</p>
                                </div>
                                
                                <p style='color: #666; font-size: 14px;'>
                                    We look forward to welcoming you!
                                </p>
                            </div>
                            
                            <div style='background-color: #6558f5; color: white; padding: 15px; text-align: center; font-size: 12px;'>
                                <p style='margin: 5px 0;'>Lodgix Hotel Reservation System</p>
                                <p style='margin: 5px 0;'>This is an automated email. Please do not reply.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                ";

                Attachment attachment = new Attachment(receiptFilePath);
                mail.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient(smtpServer);
                smtp.Port = smtpPort;
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = enableSsl;

                smtp.Send(mail);

                attachment.Dispose();
                mail.Dispose();
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"Failed to send reservation receipt via SMTP: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send reservation receipt: {ex.Message}", ex);
            }
        }

        // Send a check-in receipt email with attachment
        public void SendCheckInReceiptEmail(string recipientEmail, string recipientName, string receiptFilePath, string reservationId)
        {
            if (string.IsNullOrEmpty(senderEmail) || string.IsNullOrEmpty(senderPassword))
            {
                throw new InvalidOperationException("Email configuration is not set. Please configure SMTP settings in App.config.");
            }

            if (!File.Exists(receiptFilePath))
            {
                throw new FileNotFoundException("Check-in receipt file not found.", receiptFilePath);
            }

            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(senderEmail, displayName);
                mail.To.Add(new MailAddress(recipientEmail, recipientName));
                mail.Subject = $"Check-In Receipt #{reservationId} - Lodgix Hotel";
                mail.IsBodyHtml = true;

                mail.Body = $@"
                    <html>
                    <body style='font-family: Arial, sans-serif;'>
                        <div style='max-width: 600px; margin: 0 auto; padding: 20px;'>
                            <div style='background-color: #6558f5; color: white; padding: 20px; text-align: center;'>
                                <h1 style='margin: 0;'>LODGIX HOTEL</h1>
                                <p style='margin: 5px 0;'>Hotel Reservation System</p>
                            </div>
                            <div style='padding: 30px; background-color: #f9f9f9;'>
                                <h2 style='color: #6558f5;'>Dear {recipientName},</h2>
                                <p>Thank you for checking in at Lodgix Hotel. Attached is your check-in receipt (Reservation #{reservationId}).</p>
                                <p>Please keep this receipt as proof of check-in during your stay.</p>
                                <div style='margin: 30px 0; padding: 20px; background-color: white; border-left: 4px solid #6558f5;'>
                                    <p style='margin: 5px 0;'><strong>Reservation Number:</strong> {reservationId}</p>
                                    <p style='margin: 5px 0;'><strong>Date:</strong> {DateTime.Now:MMMM dd, yyyy}</p>
                                </div>
                                <p style='color: #666; font-size: 14px;'>We hope you enjoy your stay!</p>
                            </div>
                            <div style='background-color: #6558f5; color: white; padding: 15px; text-align: center; font-size: 12px;'>
                                <p style='margin: 5px 0;'>Lodgix Hotel Reservation System</p>
                                <p style='margin: 5px 0;'>This is an automated email. Please do not reply.</p>
                            </div>
                        </div>
                    </body>
                    </html>
                ";

                Attachment attachment = new Attachment(receiptFilePath);
                mail.Attachments.Add(attachment);

                SmtpClient smtp = new SmtpClient(smtpServer);
                smtp.Port = smtpPort;
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.EnableSsl = enableSsl;

                smtp.Send(mail);

                attachment.Dispose();
                mail.Dispose();
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"Failed to send check-in receipt via SMTP: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to send check-in receipt: {ex.Message}", ex);
            }
        }
    }
}

using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Services
{
    public class InvoicePrintService : IDisposable
    {
        private BillingModel billing;
        private CustomerModel customer;
        private int currentPage = 0;
        private readonly Font titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
        private readonly Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold);
        private readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font smallFont = new Font("Segoe UI", 8, FontStyle.Regular);
        private bool disposed = false;

        public InvoicePrintService(BillingModel billing, CustomerModel customer)
        {
            this.billing = billing;
            this.customer = customer;
        }

        public void Print()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 800,
                Height = 600,
                StartPosition = FormStartPosition.CenterScreen
            };

            previewDialog.ShowDialog();
        }

        public string SaveAsPdf(string filePath)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(850, 1100))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        DrawInvoice(g, new Rectangle(0, 0, 850, 1100));
                    }
                    
                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }
                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save invoice as image: {ex.Message}");
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawInvoice(e.Graphics, e.MarginBounds);
            e.HasMorePages = false;
        }

        private void DrawInvoice(Graphics g, Rectangle bounds)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;

            float yPos = bounds.Top + 20;
            float leftMargin = bounds.Left + 40;
            float rightMargin = bounds.Right - 40;
            float centerX = (leftMargin + rightMargin) / 2;

            using (Pen borderPen = new Pen(Color.FromArgb(80, 90, 240), 3))
            {
                g.DrawRectangle(borderPen, bounds.Left + 10, bounds.Top + 10, bounds.Width - 20, bounds.Height - 20);
            }

            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

            using (Brush titleBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("LODGIX", titleFont, titleBrush, centerX, yPos, centerFormat);
            }
            yPos += 25;
            g.DrawString("Hotel Reservation System", normalFont, Brushes.Gray, centerX, yPos, centerFormat);
            yPos += 40;

            using (Pen linePen = new Pen(Color.FromArgb(80, 90, 240), 2))
            {
                g.DrawLine(linePen, leftMargin, yPos, rightMargin, yPos);
            }
            yPos += 20;

            using (Brush headerBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("INVOICE", headerFont, headerBrush, centerX, yPos, centerFormat);
            }
            yPos += 40;

            g.DrawString($"Invoice #: {billing.BillId}", boldFont, Brushes.Black, leftMargin, yPos);
            g.DrawString($"Date: {billing.DateBilled:MM/dd/yyyy}", normalFont, Brushes.Black, rightMargin, yPos, rightFormat);
            yPos += 25;

            g.DrawString($"Reservation #: {billing.ReservationId}", normalFont, Brushes.Black, leftMargin, yPos);
            yPos += 40;

            using (Brush grayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(grayBrush, leftMargin, yPos, rightMargin - leftMargin, 30);
            }
            yPos += 8;
            using (Brush sectionBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("GUEST INFORMATION", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 30;

            g.DrawString($"Name: {billing.CustomerName}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            
            string email = !string.IsNullOrEmpty(billing.CustomerEmail) ? billing.CustomerEmail : 
                          (customer != null ? customer.Email : "N/A");
            string contact = !string.IsNullOrEmpty(billing.CustomerContact) ? billing.CustomerContact : 
                            (customer != null ? customer.Contact : "N/A");
            string address = !string.IsNullOrEmpty(billing.CustomerAddress) ? billing.CustomerAddress : 
                            (customer != null ? customer.Address : "N/A");
            
            g.DrawString($"Email: {email}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString($"Contact: {contact}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString($"Address: {address}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 30;

            using (Brush grayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(grayBrush, leftMargin, yPos, rightMargin - leftMargin, 30);
            }
            yPos += 8;
            using (Brush sectionBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("ROOM DETAILS", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 30;

            g.DrawString($"Room Number: {billing.RoomNumber}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Room Type: {billing.RoomType}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 20;
            g.DrawString($"Check-In: {billing.CheckInDate:MM/dd/yyyy}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Check-Out: {billing.CheckOutDate:MM/dd/yyyy}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 20;
            g.DrawString($"Nights: {billing.NumberOfNights}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 40;

            using (Brush tableBrush = new SolidBrush(Color.FromArgb(101, 118, 255)))
            {
                g.FillRectangle(tableBrush, leftMargin, yPos, rightMargin - leftMargin, 30);
            }
            yPos += 8;
            g.DrawString("Description", boldFont, Brushes.White, leftMargin + 10, yPos);
            g.DrawString("Amount (PHP)", boldFont, Brushes.White, rightMargin - 100, yPos, rightFormat);
            yPos += 30;

            using (Pen tablePen = new Pen(Color.LightGray, 1))
            {
                float itemYPos = yPos;
                g.DrawString("Room Charge", normalFont, Brushes.Black, leftMargin + 10, itemYPos);
                g.DrawString($"PHP {billing.RoomCharge:N2}", normalFont, Brushes.Black, rightMargin - 100, itemYPos, rightFormat);
                itemYPos += 25;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                g.DrawString("Late Checkout Fee", normalFont, Brushes.Black, leftMargin + 10, itemYPos + 5);
                g.DrawString($"PHP {billing.LateCheckoutFee:N2}", normalFont, Brushes.Black, rightMargin - 100, itemYPos + 5, rightFormat);
                itemYPos += 30;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                g.DrawString("Damage Fee", normalFont, Brushes.Black, leftMargin + 10, itemYPos + 5);
                g.DrawString($"PHP {billing.DamageFee:N2}", normalFont, Brushes.Black, rightMargin - 100, itemYPos + 5, rightFormat);
                itemYPos += 30;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                yPos = itemYPos + 10;
            }

            using (Brush subtotalBrush = new SolidBrush(Color.FromArgb(250, 250, 250)))
            {
                g.FillRectangle(subtotalBrush, leftMargin, yPos, rightMargin - leftMargin, 25);
            }
            g.DrawString("Subtotal:", boldFont, Brushes.Black, leftMargin + 10, yPos + 5);
            g.DrawString($"PHP {billing.Subtotal:N2}", boldFont, Brushes.Black, rightMargin - 100, yPos + 5, rightFormat);
            yPos += 35;

            g.DrawString("Amount Paid Before:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"PHP {billing.AmountPaidBefore:N2}", normalFont, Brushes.Black, rightMargin - 100, yPos, rightFormat);
            yPos += 20;

            g.DrawString("Amount Paid at Checkout:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"PHP {billing.AmountPaidAtCheckout:N2}", normalFont, Brushes.Black, rightMargin - 100, yPos, rightFormat);
            yPos += 30;

            using (Brush totalBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.FillRectangle(totalBrush, leftMargin, yPos, rightMargin - leftMargin, 35);
            }
            g.DrawString("BALANCE DUE:", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos + 8);
            g.DrawString($"PHP {billing.BalanceDue:N2}", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, rightMargin - 100, yPos + 8, rightFormat);
            yPos += 50;

            using (Brush infoBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
            {
                g.FillRectangle(infoBrush, leftMargin, yPos, rightMargin - leftMargin, 60);
            }
            yPos += 10;
            g.DrawString($"Payment Method: {billing.PaymentMethod}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString($"Payment Status: {billing.PaymentStatus}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString($"Reference: {billing.PaymentReference}", smallFont, Brushes.Gray, leftMargin + 10, yPos);
            yPos += 40;

            using (Brush footerBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("Thank you for choosing Lodgix Hotel!", normalFont, footerBrush, centerX, yPos, centerFormat);
            }
            yPos += 20;
            g.DrawString($"Prepared by: {billing.BilledBy}", smallFont, Brushes.Gray, centerX, yPos, centerFormat);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    titleFont?.Dispose();
                    headerFont?.Dispose();
                    normalFont?.Dispose();
                    boldFont?.Dispose();
                    smallFont?.Dispose();
                }
                disposed = true;
            }
        }
    }
}

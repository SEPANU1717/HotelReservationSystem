using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model.CheckInOut;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Services
{
    public class CheckInReceiptService : IDisposable
    {
        private CheckInOutModel checkIn;
        private CustomerModel customer;
        private readonly Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        private readonly Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
        private readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font smallFont = new Font("Segoe UI", 8, FontStyle.Regular);
        private bool disposed = false;

        public CheckInReceiptService(CheckInOutModel checkIn, CustomerModel customer)
        {
            this.checkIn = checkIn;
            this.customer = customer;
        }

        public void ShowReceipt()
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintPage;

            PrintPreviewDialog previewDialog = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 700,
                Height = 900,
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Check-In Receipt"
            };

            previewDialog.ShowDialog();
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawReceipt(e.Graphics, e.MarginBounds);
            e.HasMorePages = false;
        }

        private void DrawReceipt(Graphics g, Rectangle bounds)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;

            float yPos = bounds.Top + 20;
            float leftMargin = bounds.Left + 40;
            float rightMargin = bounds.Right - 40;
            float centerX = (leftMargin + rightMargin) / 2;

            // System color border - RGB(80, 90, 240)
            using (Pen borderPen = new Pen(Color.FromArgb(80, 90, 240), 3))
            {
                g.DrawRectangle(borderPen, bounds.Left + 10, bounds.Top + 10, bounds.Width - 20, bounds.Height - 20);
            }

            StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
            StringFormat rightFormat = new StringFormat { Alignment = StringAlignment.Far };

            // System color for title
            using (Brush titleBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("LODGIX", titleFont, titleBrush, centerX, yPos, centerFormat);
            }
            yPos += 23;
            g.DrawString("Hotel Reservation System", normalFont, Brushes.Gray, centerX, yPos, centerFormat);
            yPos += 30;

            // System color line
            using (Pen linePen = new Pen(Color.FromArgb(80, 90, 240), 2))
            {
                g.DrawLine(linePen, leftMargin, yPos, rightMargin, yPos);
            }
            yPos += 15;

            // System color for header
            using (Brush headerBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("CHECK-IN RECEIPT", headerFont, headerBrush, centerX, yPos, centerFormat);
            }
            yPos += 30;

            g.DrawString($"Reservation #: {checkIn.ReservationId}", boldFont, Brushes.Black, leftMargin, yPos);
            g.DrawString($"Date: {DateTime.Now:MM/dd/yyyy HH:mm}", normalFont, Brushes.Black, rightMargin, yPos, rightFormat);
            yPos += 20;
            g.DrawString($"Status: {checkIn.ReservationStatus}", normalFont, Brushes.Black, leftMargin, yPos);
            yPos += 30;

            using (Brush grayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(grayBrush, leftMargin, yPos, rightMargin - leftMargin, 25);
            }
            yPos += 5;
            using (Brush sectionBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("GUEST INFORMATION", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 25;

            g.DrawString($"Name: {checkIn.CustomerName}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 18;
            if (customer != null)
            {
                g.DrawString($"Contact: {customer.Contact}", normalFont, Brushes.Black, leftMargin + 10, yPos);
                yPos += 18;
                g.DrawString($"Email: {customer.Email}", normalFont, Brushes.Black, leftMargin + 10, yPos);
                yPos += 25;
            }
            else
            {
                yPos += 25;
            }

            using (Brush grayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(grayBrush, leftMargin, yPos, rightMargin - leftMargin, 25);
            }
            yPos += 5;
            using (Brush sectionBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("CHECK-IN DETAILS", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 25;

            g.DrawString($"Room Number: {checkIn.RoomNumber}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Room Type: {checkIn.RoomType}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 18;
            g.DrawString($"Check-In: {checkIn.CheckInDate:MM/dd/yyyy}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Check-Out: {checkIn.CheckOutDate:MM/dd/yyyy}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 18;
            g.DrawString($"Time of Arrival: {checkIn.TimeArrival:HH:mm}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            
            // Show customer email if available
            string displayEmail = !string.IsNullOrEmpty(checkIn.CustomerEmail) ? checkIn.CustomerEmail :
                                 (customer != null ? customer.Email : "N/A");
            g.DrawString($"Email: {displayEmail}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 30;

            using (Brush grayBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
            {
                g.FillRectangle(grayBrush, leftMargin, yPos, rightMargin - leftMargin, 25);
            }
            yPos += 5;
            using (Brush sectionBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("PAYMENT SUMMARY (PHP)", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 25;

            g.DrawString("Total Price:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"PHP {checkIn.TotalPrice:N2}", boldFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            g.DrawString("Down Payment:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"PHP {checkIn.DownPayment:N2}", normalFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            g.DrawString("Amount Paid:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"PHP {checkIn.AmountPaid:N2}", normalFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            using (Pen linePen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(linePen, leftMargin + 10, yPos, rightMargin - 10, yPos);
            }
            yPos += 8;

            // System color for balance highlight - RGB(101, 118, 255)
            using (Brush highlightBrush = new SolidBrush(Color.FromArgb(101, 118, 255)))
            {
                g.FillRectangle(highlightBrush, leftMargin, yPos, rightMargin - leftMargin, 28);
            }
            yPos += 5;
            g.DrawString("Balance Due:", new Font("Segoe UI", 11, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos);
            g.DrawString($"PHP {checkIn.BalanceDue:N2}", new Font("Segoe UI", 11, FontStyle.Bold), Brushes.White, rightMargin - 10, yPos, rightFormat);
            yPos += 35;

            g.DrawString($"Payment Status: {checkIn.PaymentStatus}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 18;
            g.DrawString($"Payment Method: {checkIn.PaymentMethod ?? "N/A"}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 18;
            g.DrawString($"Reference: {checkIn.PaymentReference ?? "N/A"}", smallFont, Brushes.Gray, leftMargin + 10, yPos);
            yPos += 35;

            using (Brush footerBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("Welcome to Lodgix Hotel!", normalFont, footerBrush, centerX, yPos, centerFormat);
            }
            yPos += 18;
            g.DrawString("Enjoy your stay!", smallFont, Brushes.Gray, centerX, yPos, centerFormat);
            yPos += 20;
            g.DrawString($"Checked in by: {checkIn.CheckedInBy ?? "System"}", smallFont, Brushes.Gray, centerX, yPos, centerFormat);
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

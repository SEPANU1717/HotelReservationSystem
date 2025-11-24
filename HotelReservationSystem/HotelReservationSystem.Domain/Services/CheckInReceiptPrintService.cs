using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model;
using HotelReservationSystem.Domain.Model.CheckInOut;

namespace HotelReservationSystem.Domain.Services
{
    public class CheckInReceiptPrintService : IDisposable
    {
        private CheckInOutModel checkIn;
        private CustomerModel customer;
        private readonly Font titleFont = new Font("Segoe UI", 18, FontStyle.Bold);
        private readonly Font headerFont = new Font("Segoe UI", 12, FontStyle.Bold);
        private readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font smallFont = new Font("Segoe UI", 8, FontStyle.Regular);
        private bool disposed = false;

        public CheckInReceiptPrintService(CheckInOutModel checkIn, CustomerModel customer)
        {
            this.checkIn = checkIn;
            this.customer = customer;
        }

        public void ShowWithOptions()
        {
            using (var optionsForm = new CheckInReceiptOptionsForm(checkIn, customer, DrawReceipt))
            {
                optionsForm.ShowDialog();
            }
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
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

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
            yPos += 23;
            g.DrawString("Hotel Reservation System", normalFont, Brushes.Gray, centerX, yPos, centerFormat);
            yPos += 30;

            using (Pen linePen = new Pen(Color.FromArgb(80, 90, 240), 2))
            {
                g.DrawLine(linePen, leftMargin, yPos, rightMargin, yPos);
            }
            yPos += 15;

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
            yPos += 20;
            g.DrawString($"Check-In: {checkIn.CheckInDate:MM/dd/yyyy}", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Check-Out: {checkIn.CheckOutDate:MM/dd/yyyy}", normalFont, Brushes.Black, centerX, yPos);
            yPos += 20;
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
                g.DrawString("PAYMENT SUMMARY", boldFont, sectionBrush, leftMargin + 5, yPos);
            }
            yPos += 25;

            g.DrawString("Total Price:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Php {checkIn.TotalPrice:N2}", boldFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            g.DrawString("Down Payment:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Php {checkIn.DownPayment:N2}", normalFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            g.DrawString("Amount Paid:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString($"Php {checkIn.AmountPaid:N2}", normalFont, Brushes.Black, rightMargin - 10, yPos, rightFormat);
            yPos += 18;

            using (Pen linePen = new Pen(Color.Gray, 1))
            {
                g.DrawLine(linePen, leftMargin + 10, yPos, rightMargin - 10, yPos);
            }
            yPos += 8;

            using (Brush highlightBrush = new SolidBrush(Color.FromArgb(101, 118, 255)))
            {
                g.FillRectangle(highlightBrush, leftMargin, yPos, rightMargin - leftMargin, 28);
            }
            yPos += 5;
            g.DrawString("Balance Due:", new Font("Segoe UI", 11, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos);
            // Per requirement: always display 0.00 for balance due on check-in receipts
            g.DrawString($"Php {0.00:N2}", new Font("Segoe UI", 11, FontStyle.Bold), Brushes.White, rightMargin - 10, yPos, rightFormat);
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

        // Export check-in receipt to PNG image for emailing
        public string SaveAsImage(string filePath)
        {
            try
            {
                using (Bitmap bmp = new Bitmap(850, 1100))
                {
                    using (Graphics g = Graphics.FromImage(bmp))
                    {
                        g.Clear(Color.White);
                        DrawReceipt(g, new Rectangle(0, 0, 850, 1100));
                    }

                    var dir = Path.GetDirectoryName(filePath);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                        Directory.CreateDirectory(dir);

                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                return filePath;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to save check-in receipt image: {ex.Message}", ex);
            }
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

    // Custom form with Print, Save PDF, and Close options
    public class CheckInReceiptOptionsForm : Form
    {
        private PictureBox pictureBox;
        private Button btnPrint;
        private Button btnSavePDF;
        private Button btnClose;
        private CheckInOutModel checkIn;
        private CustomerModel customer;
        private Action<Graphics, Rectangle> drawMethod;

        public CheckInReceiptOptionsForm(CheckInOutModel checkIn, CustomerModel customer, Action<Graphics, Rectangle> drawMethod)
        {
            this.checkIn = checkIn;
            this.customer = customer;
            this.drawMethod = drawMethod;
            InitializeComponents();
            GeneratePreview();
        }

        private void InitializeComponents()
        {
            this.Text = "Check-In Receipt";
            this.Size = new Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.BackColor = Color.White;

            pictureBox = new PictureBox
            {
                Location = new Point(20, 20),
                Size = new Size(850, 550),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.White
            };

            btnPrint = new Button
            {
                Text = "Print",
                Location = new Point(620, 590),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(80, 90, 240),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnPrint.FlatAppearance.BorderSize = 0;
            btnPrint.Click += BtnPrint_Click;

            btnSavePDF = new Button
            {
                Text = "Save PDF",
                Location = new Point(710, 590),
                Size = new Size(80, 35),
                BackColor = Color.FromArgb(101, 118, 255),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSavePDF.FlatAppearance.BorderSize = 0;
            btnSavePDF.Click += BtnSavePDF_Click;

            btnClose = new Button
            {
                Text = "Close",
                Location = new Point(800, 590),
                Size = new Size(70, 35),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            this.Controls.Add(pictureBox);
            this.Controls.Add(btnPrint);
            this.Controls.Add(btnSavePDF);
            this.Controls.Add(btnClose);
        }

        private void GeneratePreview()
        {
            try
            {
                Bitmap previewBitmap = new Bitmap(850, 1100);
                using (Graphics g = Graphics.FromImage(previewBitmap))
                {
                    g.Clear(Color.White);
                    drawMethod(g, new Rectangle(0, 0, 850, 1100));
                }
                pictureBox.Image = previewBitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error generating preview: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDocument printDoc = new PrintDocument();
                printDoc.PrintPage += (s, ev) =>
                {
                    drawMethod(ev.Graphics, ev.MarginBounds);
                    ev.HasMorePages = false;
                };

                PrintDialog printDialog = new PrintDialog
                {
                    Document = printDoc
                };

                if (printDialog.ShowDialog() == DialogResult.OK)
                {
                    printDoc.Print();
                    MessageBox.Show("Receipt sent to printer successfully!", "Print Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error printing: {0}", ex.Message), "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSavePDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PNG Image|*.png|JPEG Image|*.jpg",
                    FileName = string.Format("CheckIn_{0}_{1:yyyyMMdd}.png", checkIn.ReservationId, DateTime.Now),
                    DefaultExt = "png"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    using (Bitmap saveBitmap = new Bitmap(850, 1100))
                    {
                        using (Graphics g = Graphics.FromImage(saveBitmap))
                        {
                            g.Clear(Color.White);
                            drawMethod(g, new Rectangle(0, 0, 850, 1100));
                        }

                        string ext = Path.GetExtension(saveDialog.FileName).ToLower();
                        if (ext == ".jpg" || ext == ".jpeg")
                        {
                            saveBitmap.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
                        }
                        else
                        {
                            saveBitmap.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        }
                    }

                    MessageBox.Show(string.Format("Receipt saved successfully to:\n{0}", saveDialog.FileName), "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving: {0}", ex.Message), "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                pictureBox?.Image?.Dispose();
                pictureBox?.Dispose();
                btnPrint?.Dispose();
                btnSavePDF?.Dispose();
                btnClose?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

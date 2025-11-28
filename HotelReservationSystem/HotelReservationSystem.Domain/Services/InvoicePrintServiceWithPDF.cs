using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using HotelReservationSystem.Domain.Model;

namespace HotelReservationSystem.Domain.Services
{
    public class InvoicePrintServiceWithPDF : IDisposable
    {
        private BillingModel billing;
        private CustomerModel customer;
        private readonly Font titleFont = new Font("Segoe UI", 20, FontStyle.Bold);
        private readonly Font headerFont = new Font("Segoe UI", 14, FontStyle.Bold);
        private readonly Font normalFont = new Font("Segoe UI", 10, FontStyle.Regular);
        private readonly Font boldFont = new Font("Segoe UI", 10, FontStyle.Bold);
        private readonly Font smallFont = new Font("Segoe UI", 8, FontStyle.Regular);
        private bool disposed = false;

        public InvoicePrintServiceWithPDF(BillingModel billing, CustomerModel customer)
        {
            this.billing = billing;
            this.customer = customer;
        }

        public void ShowWithOptions()
        {
            using (var optionsForm = new PrintOptionsForm(billing, customer, DrawInvoice))
            {
                optionsForm.ShowDialog();
            }
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
                StartPosition = FormStartPosition.CenterScreen,
                Text = "Invoice Preview"
            };

            previewDialog.ShowDialog();
        }

        public string SaveAsPDF(string filePath)
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
                throw new Exception(string.Format("Failed to save invoice as PDF: {0}", ex.Message));
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

            // Calculate if there's change due
            decimal totalAmount = billing.Subtotal;
            decimal totalPaid = billing.AmountPaidBefore + billing.AmountPaidAtCheckout;
            decimal change = totalPaid > totalAmount ? totalPaid - totalAmount : 0m;
            decimal displayBalance = billing.BalanceDue < 0 ? 0m : billing.BalanceDue;

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

            g.DrawString(string.Format("Invoice #: {0}", billing.BillId), boldFont, Brushes.Black, leftMargin, yPos);
            g.DrawString(string.Format("Date: {0:MM/dd/yyyy}", billing.DateBilled), normalFont, Brushes.Black, rightMargin, yPos, rightFormat);
            yPos += 25;

            g.DrawString(string.Format("Reservation #: {0}", billing.ReservationId), normalFont, Brushes.Black, leftMargin, yPos);
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

            g.DrawString(string.Format("Name: {0}", billing.CustomerName), normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            
            string email = !string.IsNullOrEmpty(billing.CustomerEmail) ? billing.CustomerEmail : 
                          (customer != null ? customer.Email : "N/A");
            string contact = !string.IsNullOrEmpty(billing.CustomerContact) ? billing.CustomerContact : 
                            (customer != null ? customer.Contact : "N/A");
            string address = !string.IsNullOrEmpty(billing.CustomerAddress) ? billing.CustomerAddress : 
                            (customer != null ? customer.Address : "N/A");
            
            g.DrawString(string.Format("Email: {0}", email), normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString(string.Format("Contact: {0}", contact), normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString(string.Format("Address: {0}", address), normalFont, Brushes.Black, leftMargin + 10, yPos);
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

            g.DrawString(string.Format("Room Number: {0}", billing.RoomNumber), normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString(string.Format("Room Type: {0}", billing.RoomType), normalFont, Brushes.Black, centerX, yPos);
            yPos += 20;
            g.DrawString(string.Format("Check-In: {0:MM/dd/yyyy}", billing.CheckInDate), normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString(string.Format("Check-Out: {0:MM/dd/yyyy}", billing.CheckOutDate), normalFont, Brushes.Black, centerX, yPos);
            yPos += 20;
            g.DrawString(string.Format("Nights: {0}", billing.NumberOfNights), normalFont, Brushes.Black, leftMargin + 10, yPos);
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
                g.DrawString(string.Format("PHP {0:N2}", billing.RoomCharge), normalFont, Brushes.Black, rightMargin - 100, itemYPos, rightFormat);
                itemYPos += 25;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                g.DrawString("Late Checkout Fee", normalFont, Brushes.Black, leftMargin + 10, itemYPos + 5);
                g.DrawString(string.Format("PHP {0:N2}", billing.LateCheckoutFee), normalFont, Brushes.Black, rightMargin - 100, itemYPos + 5, rightFormat);
                itemYPos += 30;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                g.DrawString("Damage Fee", normalFont, Brushes.Black, leftMargin + 10, itemYPos + 5);
                g.DrawString(string.Format("PHP {0:N2}", billing.DamageFee), normalFont, Brushes.Black, rightMargin - 100, itemYPos + 5, rightFormat);
                itemYPos += 30;
                g.DrawLine(tablePen, leftMargin, itemYPos, rightMargin, itemYPos);

                yPos = itemYPos + 10;
            }

            using (Brush subtotalBrush = new SolidBrush(Color.FromArgb(250, 250, 250)))
            {
                g.FillRectangle(subtotalBrush, leftMargin, yPos, rightMargin - leftMargin, 25);
            }
            g.DrawString("Subtotal:", boldFont, Brushes.Black, leftMargin + 10, yPos + 5);
            g.DrawString(string.Format("PHP {0:N2}", billing.Subtotal), boldFont, Brushes.Black, rightMargin - 100, yPos + 5, rightFormat);
            yPos += 35;

            g.DrawString("Amount Paid Before:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString(string.Format("PHP {0:N2}", billing.AmountPaidBefore), normalFont, Brushes.Black, rightMargin - 100, yPos, rightFormat);
            yPos += 20;

            g.DrawString("Amount Paid at Checkout:", normalFont, Brushes.Black, leftMargin + 10, yPos);
            g.DrawString(string.Format("PHP {0:N2}", billing.AmountPaidAtCheckout), normalFont, Brushes.Black, rightMargin - 100, yPos, rightFormat);
            yPos += 30;

            if (displayBalance > 0)
            {
                using (Brush totalBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
                {
                    g.FillRectangle(totalBrush, leftMargin, yPos, rightMargin - leftMargin, 35);
                }
                g.DrawString("BALANCE DUE:", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos + 8);
                g.DrawString(string.Format("PHP {0:N2}", displayBalance), new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, rightMargin - 100, yPos + 8, rightFormat);
                yPos += 50;
            }
            else if (change > 0)
            {
                using (Brush changeBrush = new SolidBrush(Color.FromArgb(46, 204, 113)))
                {
                    g.FillRectangle(changeBrush, leftMargin, yPos, rightMargin - leftMargin, 35);
                }
                g.DrawString("CHANGE DUE:", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos + 8);
                g.DrawString(string.Format("PHP {0:N2}", change), new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, rightMargin - 100, yPos + 8, rightFormat);
                yPos += 50;
            }
            else
            {
                using (Brush totalBrush = new SolidBrush(Color.FromArgb(46, 204, 113)))
                {
                    g.FillRectangle(totalBrush, leftMargin, yPos, rightMargin - leftMargin, 35);
                }
                g.DrawString("BALANCE DUE:", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, leftMargin + 10, yPos + 8);
                g.DrawString("PHP 0.00", new Font("Segoe UI", 12, FontStyle.Bold), Brushes.White, rightMargin - 100, yPos + 8, rightFormat);
                yPos += 50;
            }

            using (Brush infoBrush = new SolidBrush(Color.FromArgb(245, 245, 245)))
            {
                g.FillRectangle(infoBrush, leftMargin, yPos, rightMargin - leftMargin, 60);
            }
            yPos += 10;
            g.DrawString(string.Format("Payment Method: {0}", billing.PaymentMethod), normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString(string.Format("Payment Status: {0}", billing.PaymentStatus), normalFont, Brushes.Black, leftMargin + 10, yPos);
            yPos += 20;
            g.DrawString(string.Format("Reference: {0}", billing.PaymentReference), smallFont, Brushes.Gray, leftMargin + 10, yPos);
            yPos += 40;

            using (Brush footerBrush = new SolidBrush(Color.FromArgb(80, 90, 240)))
            {
                g.DrawString("Thank you for choosing Lodgix Hotel!", normalFont, footerBrush, centerX, yPos, centerFormat);
            }
            yPos += 20;
            g.DrawString(string.Format("Prepared by: {0}", billing.BilledBy), smallFont, Brushes.Gray, centerX, yPos, centerFormat);
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

    public class PrintOptionsForm : Form
    {
        private PictureBox pictureBox;
        private Button btnPrint;
        private Button btnSavePDF;
        private Button btnClose;
        private BillingModel billing;
        private CustomerModel customer;
        private Action<Graphics, Rectangle> drawMethod;

        public PrintOptionsForm(BillingModel billing, CustomerModel customer, Action<Graphics, Rectangle> drawMethod)
        {
            this.billing = billing;
            this.customer = customer;
            this.drawMethod = drawMethod;
            InitializeComponents();
            GeneratePreview();
        }

        private void InitializeComponents()
        {
            this.Text = "Invoice Preview";
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
                    MessageBox.Show("Invoice sent to printer successfully!", "Print Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    FileName = string.Format("Invoice_{0}_{1:yyyyMMdd}.png", billing.BillId, DateTime.Now),
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

                    MessageBox.Show(string.Format("Invoice saved successfully to:\n{0}", saveDialog.FileName), "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Error saving PDF: {0}", ex.Message), "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

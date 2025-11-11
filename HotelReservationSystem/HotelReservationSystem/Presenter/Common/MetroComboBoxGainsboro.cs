using MetroFramework.Controls;
using System.Drawing;
using System.Windows.Forms;

public class MetroComboBoxGainsboro : MetroComboBox
{
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        using (Pen p = new Pen(Color.Gainsboro, 1))
        {
            e.Graphics.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
        }
    }
}

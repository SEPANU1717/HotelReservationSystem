using System;
using System.Windows.Forms;

public static class FieldsCleaner
{
    public static void ClearInputs(Control parent)
    {
        foreach (Control c in parent.Controls)
        {
            if (c is TextBox tb) tb.Clear();
            else if (c is RadioButton rb) rb.Checked = false;
            else if (c is CheckBox cb) cb.Checked = false;
            else if (c is ComboBox cbx) cbx.SelectedIndex = -1;
            else if (c is DateTimePicker dt) dt.Value = DateTime.Now;

            if (c.HasChildren)
                ClearInputs(c); // recursive
        }
    }
}
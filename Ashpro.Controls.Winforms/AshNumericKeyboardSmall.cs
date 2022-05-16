using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro.Controls.Winforms
{
    public partial class AshNumericKeyboardSmall : UserControl
    {
        public event NumberClickEventHandler atNumberClick;
        public AshNumericKeyboardSmall()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            this.DoubleBuffered = true;
        }
        private void btnone_Click(object sender, EventArgs e)
        {
            SendKeys.Send((sender as Button).Tag.ToString());
            if (atNumberClick != null)
            {
                atNumberClick(this);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Form Frm = this.FindForm();

            if (Frm != null)
            {
                if (Frm.ActiveControl != null)
                {
                    if (Frm.ActiveControl.GetType() == typeof(TextBox) || Frm.ActiveControl.GetType().BaseType == typeof(TextBox))
                    {
                        (Frm.ActiveControl as TextBox).Text = "";
                    }
                }
            }
        }

        private void btnBackSpace_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
        }

    }
}

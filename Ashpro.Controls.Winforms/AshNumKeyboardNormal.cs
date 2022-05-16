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
    public partial class AshNumKeyboardNormal : UserControl
    {
        #region Constructor
        public AshNumKeyboardNormal()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            this.DoubleBuffered = true;
        }
        #endregion

        #region Control Events
        private void button1_Click(object sender, EventArgs e)
        {
            SendKeys.Send((sender as Button).Tag.ToString());
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{BACKSPACE}");
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
        #endregion
    }
}

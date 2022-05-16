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
    public partial class AshNumericKeyboardPOS : UserControl
    {
        #region Events
        public event QtyClickEventHandler atQtyClick;
        public event RateClickEventHandler atRateClick;
        public event NumberClickEventHandler atNumberClick;
        public event NumberClickEventHandler atEnterClick;
        public event NumberClickEventHandler atBackClick;
        public event NumberClickEventHandler atClearClick;
        #endregion

        #region Public Properties
        public ExButton QtyButton
        {
            get { return btnQty; }
            set { btnQty = value; }
        }
        public ExButton RateButton
        {
            get { return btnRate; }
            set
            {
                btnRate = value;
            }
        }
        #endregion

        #region Constructor
        public AshNumericKeyboardPOS()
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

            if (atNumberClick != null)
            {
                atNumberClick(this);
            }
        }
        private void btnBack_Click(object sender, EventArgs e)
        {

            SendKeys.Send("{BACKSPACE}");
            if (atBackClick != null)
            {
                atBackClick(this);
            }

        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (atClearClick != null)
            {
                atClearClick(this);
            }

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
        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (atEnterClick != null)
            {
                atEnterClick(this);
            }
            SendKeys.SendWait("{ENTER}");
        }
        private void btnPoint_Click(object sender, EventArgs e)
        {
            if (atNumberClick != null)
            {
                atNumberClick(this);
            }
            SendKeys.Send(".");
        }
        private void btnQty_Click(object sender, EventArgs e)
        {
            btnQty.BackColor = Color.FromArgb(245, 119, 35);
            btnQty.ForeColor = Color.White;
            btnRate.BackColor = Color.Gainsboro;
            btnRate.ForeColor = Color.FromArgb(61, 77, 125);
            if (atQtyClick != null)
            {
                atQtyClick(btnQty);
            }
        }
        private void btnRate_Click(object sender, EventArgs e)
        {
            if (atRateClick != null)
            {
                atRateClick(btnRate);
            }
            btnRate.BackColor = Color.FromArgb(245, 119, 35);
            btnRate.ForeColor = Color.White;
            btnQty.BackColor = Color.Gainsboro;
            btnQty.ForeColor = Color.FromArgb(61, 77, 125);

        }
        #endregion
    }
}

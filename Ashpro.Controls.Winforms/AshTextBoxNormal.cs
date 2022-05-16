using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro.Controls.Winforms
{
    public partial class AshTextBoxNormal : TextBox
    {

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, string lp);
        private string mCue;
        public bool isNumeric { get; set; }
        public bool isAllowNegative { get; set; }
        public bool isAllowSpecialChar { get; set; }
        public bool isNumberOnly { get; set; }
        public Color EnterColor { get; set; } = Color.LightPink;
        public Color LeaveColor { get; set; } = Color.White;
        public string Format { get; set; }
        [Localizable(true)]
        public string WaterMark
        {
            get { return mCue; }
            set { mCue = value; updateCue(); }
        }
        private void updateCue()
        {
            if (this.IsHandleCreated && mCue != null)
            {
                SendMessage(this.Handle, 0x1501, (IntPtr)1, mCue);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            updateCue();
        }
        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            this.BackColor = EnterColor;
        }
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.BackColor = LeaveColor;
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.KeyCode == Keys.Enter && e.Handled == false && !Multiline)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down && e.Handled == false && !Multiline)
            {
                SendKeys.Send("{TAB}");
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up && e.Handled == false && !Multiline)
            {
                SendKeys.Send("+{TAB}");
                e.Handled = true;
            }
        }
        public AshTextBoxNormal()
        {
            InitializeComponent();
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            bool _isNumber = false;
            if (isNumeric == true)
            {
                if (isAllowNegative)
                {
                    if (e.KeyChar == '-')
                    {
                        if (this.Text.Contains('-')) { e.Handled = true; }
                        return;
                    }
                }
                if (isAllowSpecialChar)
                {
                    if (e.KeyChar == '+') { return; }
                }
                if (e.KeyChar == 8 || e.KeyChar == 46) { return; }
                int n;
                _isNumber = int.TryParse(Char.ConvertFromUtf32(e.KeyChar), out n);
                if (_isNumber == false)
                {
                    e.Handled = true;
                }
            }
            if (isNumberOnly)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

        }
        public decimal Value
        {
            get
            {
                decimal ret = 0;
                decimal.TryParse(this.Text, out ret);
                return ret;
            }
            set
            {
                if (isNumeric)
                    this.Text = GlobalFunctions.getFormattedValue(Format, value);

            }
        }
        public int Data
        {
            get
            {
                int ret = 0;
                if (this.Text.Length > 0 && (!this.Text.Substring(0, 1).isNumeric()))
                {
                    int.TryParse(this.Text.Substring(1), out ret);
                }
                else
                {
                    int.TryParse(this.Text, out ret);
                }

                return ret;

            }
            set
            {
                if (isNumberOnly)
                    this.Text = GlobalFunctions.getFormattedValue(Format, value);
            }
        }
    }
}

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
    public partial class AshNumericKeyboardPOS_2 : UserControl
    {
        #region Events
        public event NumberClickEventHandler atClearClick;
        public event NumberClickEventHandler atEqualClick;
        public event StarClickEventHandler atStarClick;
        public event AddClickEventHandler atAddClick;
        public event SubClickEventHandler atSubClick;
        #endregion

        #region Public Properties
        public ExButton StarButton
        {
            get { return btnMultiple; }
            set { btnMultiple = value; }
        }
        #endregion

        #region Constructor
        public AshNumericKeyboardPOS_2()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Events
        private void btnce_Click(object sender, EventArgs e)
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
        private void btnone_Click(object sender, EventArgs e)
        {
            SendKeys.Send((sender as Button).Tag.ToString());
        }
        private void btnequel_Click(object sender, EventArgs e)
        {
            if (atEqualClick != null)
            {
                atEqualClick(this);
            }
        }
        private void btnpls_Click(object sender, EventArgs e)
        {
            if (atAddClick != null)
            {
                atAddClick(this);
            }
        }
        private void btnsub_Click(object sender, EventArgs e)
        {
            if (atSubClick != null)
            {
                atSubClick(this);
            }
        }
        private void btnmu_Click(object sender, EventArgs e)
        {
            if (atStarClick != null)
            {
                atStarClick(this);
            }
        }
        #endregion
    }
}

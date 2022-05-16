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
    public partial class AshProgressBar : UserControl
    {
        public AshProgressBar(Form frm)
        {
            InitializeComponent();
            this.Location = new System.Drawing.Point(frm.Width / 2 - 103, frm.Height / 2 - 26);
            frm.Controls.Add(this);
            this.BringToFront();
        }
    }
}

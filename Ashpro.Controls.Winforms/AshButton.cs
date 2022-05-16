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
    public partial class AshButton : Button
    {
        #region Constructor
        public AshButton()
        {
            InitializeComponent();
        }
        #endregion

        protected override void InitLayout()
        {
            base.InitLayout();
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Font = new Font("MS Reference Sans Serif", 10, FontStyle.Bold);
        }

    }
}

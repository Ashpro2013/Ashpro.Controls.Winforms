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
    public partial class AshComboBox : ComboBox
    {
        #region Private Variable
        Label lblLine;
        #endregion

        #region Constructor
        public AshComboBox()
        {
            this.FlatStyle = FlatStyle.Flat;
            lblLine = new Label()
            {
                Height = 1,
                Dock = DockStyle.None,
                Left = this.Left,
                Top = this.Height - 2,
                Width = this.Width - 20,
                BackColor = Color.DarkGray
            };
            Controls.Add(lblLine);
            this.Invalidate(true);
            InitializeComponent();
        }
        #endregion

        #region Public Properties
        public Color EnterColor { get; set; } = Color.LightPink;
        public Color LeaveColor { get; set; } = Color.White;
        #endregion

        #region Overied Events
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            lblLine.Width = this.Width - 20;
            lblLine.Top = this.Height - 2;
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
            if (e.Handled == false)
            {
                if (e.KeyCode == Keys.Return)
                {
                    SendKeys.Send("{TAB}");
                }
            }
        }
        #endregion
    }
}

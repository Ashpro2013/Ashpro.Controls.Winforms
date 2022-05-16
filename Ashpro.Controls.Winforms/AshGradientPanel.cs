using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ashpro.Controls.Winforms
{
    public partial class AshGradientPanel : Panel
    {
        #region Priavate Variable
        string _Text = "";
        #endregion

        #region Constructor
        public AshGradientPanel()
        {
            InitializeComponent();
            this.TextAdjestmentHeight = 0;
            this.TextAlign = ContentAlignment.MiddleCenter;
            _Text = "";
        }
        #endregion

        #region Public Property
        public Color TopColor { get; set; }
        public Color BottomColor { get; set; }
        public float Angle { get; set; }
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public bool Selected
        {
            get;
            set;
        }
        public bool AllowMultiSelect
        {
            get;
            set;
        }
        public override string Text
        {
            get
            {
                return _Text;
            }
            set
            {
                _Text = value;
            }
        }
        public ContentAlignment TextAlign
        {
            get;
            set;
        }
        public int TextAdjestmentHeight
        {
            get;
            set;
        }
        #endregion

        #region Public Events
        private void DrawText(Graphics g)
        {
            Graphics graphics = g;
            Brush _TextBrush = new SolidBrush(this.ForeColor);

            StringFormat frmat = new StringFormat();
            Int32 lNum = (Int32)Math.Log((Double)this.TextAlign, 2);
            frmat.LineAlignment = (StringAlignment)(lNum / 4);
            frmat.Alignment = (StringAlignment)(lNum % 4);
            graphics.DrawString(_Text, this.Font, _TextBrush, new Rectangle(TextAdjestmentHeight / 2, TextAdjestmentHeight / 2, this.ClientRectangle.Width - TextAdjestmentHeight, this.ClientRectangle.Height - TextAdjestmentHeight), frmat);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            LinearGradientBrush brush = new LinearGradientBrush(this.ClientRectangle, this.TopColor, this.BottomColor, this.Angle);
            Graphics g = e.Graphics;
            g.FillRectangle(brush, this.ClientRectangle);
            if (this.Text != string.Empty)
                DrawText(e.Graphics);
            base.OnPaint(e);
        }
        #endregion
    }
}

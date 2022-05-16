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
    public partial class AshNumericLabel : System.Windows.Forms.Label
    {
        private decimal _value;
        public string Format { get; set; }

        #region Override Methods
        protected override void InitLayout()
        {
            base.InitLayout();
            this.TextAlign = System.Drawing.ContentAlignment.MiddleRight;


        }
        #endregion
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
                _value = value;

                this.Text = getFormattedValue(Format, _value);
            }

        }
        //[Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        //public override string Text
        //{

        //    get
        //    {

        //        return base.Text;

        //    }

        //}
        public static string getFormattedValue(string Format, decimal _value)
        {
            string _FormattedValue;
            int NumberOfDecimals;
            decimal RoundedValue;
            if (Format != null && Format != "" && Format.Length > 1)
            {
                NumberOfDecimals = Convert.ToInt32(Format.Substring(1));
                if (NumberOfDecimals > 0)
                {
                    RoundedValue = Math.Round(_value, NumberOfDecimals);
                    _FormattedValue = RoundedValue.ToString(Format);
                }
                else
                {
                    _FormattedValue = _value.ToString(Format);
                }
            }
            else
            {
                _FormattedValue = _value.ToString();
            }
            return _FormattedValue;
        }
    }

}

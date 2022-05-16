using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ashpro.Controls.Winforms
{
   public  class GlobalFunctions
    {
        public static string getFormattedValue(string Format, decimal _value)
        {
            string _FormattedValue;
            if (Format != null && Format != "" && Format.Length > 1)
            {
                _FormattedValue = _value.ToString(Format);
            }
            else
            {
                _FormattedValue = _value.ToString();
            }
            return _FormattedValue;
        }

    }
}

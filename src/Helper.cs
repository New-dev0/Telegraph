using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace TelegraphApp
{
    internal class Helper
    {
        private static readonly BrushConverter _Conv = new BrushConverter();
        public static Brush Convert(string color_code)
        {
            return _Conv.ConvertFromString(color_code) as Brush;
        }
    }
}

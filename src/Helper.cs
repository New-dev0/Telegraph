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

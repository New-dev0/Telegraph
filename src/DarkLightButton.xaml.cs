using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for DarkLightButton.xaml
    /// </summary>
    public partial class DarkLightButton : ToggleButton
    {
        public static Image? Img;
        public static bool? enabled;
        public DarkLightButton()
        {

            InitializeComponent();
            Img = Imsu;
            enabled = IsChecked;
            ToggleMode();
        }

        public static void ToggleMode()
        {
            string img;
            if (App.DarkMode)
            {
                img = "moon";
            }
            else
            {
                img = "sun";
            }
            Img.Source = new BitmapImage(
                                new Uri($"/src/assets/outlined_{img}.png", UriKind.Relative)
            );
            enabled = App.DarkMode;
        }
    }
}
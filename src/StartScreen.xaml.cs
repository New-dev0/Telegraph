using System.Windows.Controls;
using System.Windows.Media;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for StartScreen.xaml
    /// </summary>
    public partial class StartScreen : UserControl
    {
        public StartScreen()
        {
            InitializeComponent();
            ToggleMode();
        }

        private void ToggleMode()
        {
            if (App.DarkMode)
            {
                Background = Helper.Convert("#263238");
                TextBlo.Foreground = Brushes.White;
            }
            else
            {

                Background = Brushes.White;
                TextBlo.Foreground = Brushes.DarkCyan;
            }
            PRopa.Foreground = Brushes.Blue;
        }
    }
}

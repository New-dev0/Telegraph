using System.Windows;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for firstStartWindow.xaml
    /// </summary>
    public partial class firstStartWindow : Window
    {
        public firstStartWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreateLogin();
        }


    }
}

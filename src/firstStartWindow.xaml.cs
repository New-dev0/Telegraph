using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for firstStartWindow.xaml
    /// </summary>
    public partial class firstStartWindow : Window
    {
        private Brush? Bt_Color;

        public firstStartWindow()
        {
            InitializeComponent();
            Toggle_Mode();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new CreateLogin();
            Height = 530;
            Width = 900;
            ResizeMode = ResizeMode.CanResizeWithGrip;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (WinMain.Width < 500)
            {
                DarkLight.Visibility = Visibility.Collapsed;
            }
            else if (!DarkLight.IsVisible)
            {
                DarkLight.Visibility = Visibility.Visible;
            }
        }

        private void DarkLight_Click(object sender, RoutedEventArgs e)
        {
            App.DarkMode = !App.DarkMode;
            Toggle_Mode();
        }

        private void StartBt_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Bt_Color = StartBt.Foreground;
            var color = App.DarkMode ? "#263238" : "#1B5E20";
            StartBt.Foreground = Helper.Convert(color);
        }

        private void StartBt_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StartBt.Foreground = Brushes.White;
        }
        private void Toggle_Mode()
        {
            string button_icon;

            // Dark Mode
            if (App.DarkMode)
            {
                var BG = Helper.Convert("#263238");
                WinMain.Background = BG;
                lABEL.Foreground = Brushes.White;
                Borderr.Background = Helper.Convert("#EEEEEE");
                StartBt.Background = BG;
                DarkLight.Background = Helper.Convert("#455A64");
                DarkLight.BorderBrush = Brushes.White;
                button_icon = "moon";
            }
            // Light MoDe
            else
            {
                WinMain.Background = Helper.Convert("#FFE1F5FE");
                lABEL.Foreground = Helper.Convert("#FF6A3A94");
                Borderr.Background = null;
                StartBt.Background = Helper.Convert("#FF2ECC71");
                DarkLight.Background = Helper.Convert("#FFF5F5FF");
                DarkLight.BorderBrush = Brushes.Black;
                button_icon = "sun";
            }
            InpImg.Source = new BitmapImage(
                new Uri($"/src/assets/{button_icon}.png", UriKind.Relative)
            );
        }


        private void WinMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.DARK_MODE = App.DarkMode;
            Properties.Settings.Default.Save();
        }
    }
}

using Kvyk.Telegraph.Exceptions;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for LogoutControl.xaml
    /// </summary>
    public partial class LogoutControl : UserControl
    {
        public LogoutControl()
        {
            InitializeComponent();
        }

        private async void LOGOUTbUTTON_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RevokeTok.IsChecked)
            {
                try {
                    await App.client.RevokeAccessToken();
                }
                catch ( TelegraphException ex)
                {
                    App.Error_box(ex.Message);
                    return;
                };
                lOGOUTbUTTON.Content = "Logging Out!";
                await Task.Delay(500);
            }
            Properties.Settings.Default.ACCESS_TOKEN = "";
            Properties.Settings.Default.Save();
            Window current = Window.GetWindow(this);
            current.Title = "Telegraph";
            current.Height = 530;
            current.Width = 900;
            current.WindowState = WindowState.Normal;
            current.Content = new CreateLogin();
        }
    }
}

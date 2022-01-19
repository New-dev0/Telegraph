using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading;
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

        private void LOGOUTbUTTON_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)RevokeTok.IsChecked)
            {
                var dict = new Dictionary<string, string>
                {
                    {"access_token", Properties.Settings.Default.ACCESS_TOKEN}
                };
                JObject response = App.Make_request("revokeAccessToken", dict);
                if (response["ok"].ToString() != "true" && response.GetValue("error") != null)
                {
                    MessageBox.Show(response["error"].ToString(), "Error while revoking token :(");
                    return;
                };
                lOGOUTbUTTON.Content = "Logging Out!";
                Thread.Sleep(500);
            }
            Properties.Settings.Default.ACCESS_TOKEN = "";
            Properties.Settings.Default.ACCESS_TOKEN = "";
            Properties.Settings.Default.Save();
            Window current = Window.GetWindow(this);
            current.Title = "Telegraph";
            current.Height = 450;
            current.Width = 800;
            current.Content = new CreateLogin();
        }
    }
}

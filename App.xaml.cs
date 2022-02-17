using Kvyk.Telegraph;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
        public const string API_URI = "https://api.telegra.ph/";

        public static Window? CRTWIN = null;
        public static Grid? RIGHT_GRID { get; set; }

        public static bool DarkMode;

        public static TelegraphClient client;
        // public static List<Window> PartWindows = new() {};

        /// <summary>
        /// Startup Application
        /// </summary>
        public async void APPLICATION_START(object sender, StartupEventArgs e)
        {

            DarkMode = TelegraphApp.Properties.Settings.Default.DARK_MODE;
            string token = TelegraphApp.Properties.Settings.Default.ACCESS_TOKEN;
            if (token == "")
            {
                client = new TelegraphClient();
                Window CRTWIN = new firstStartWindow();

                CRTWIN.Show();
            }
            else
            {
                client = new TelegraphClient()
                {
                    AccessToken = token
                };
                Window CRTWIN = new MainWindow
                {
                    Content = new StartScreen(),
                    ResizeMode = ResizeMode.CanResizeWithGrip
                };
                CRTWIN.Show();
                await Task.Delay(4000);
                CRTWIN.Content = new MainUserControl();
            }
        }

        /// <summary>
        /// Simplify file length
        /// </summary>
        public static string SimplifyFileLength(int length)
        {
            string[] exts = { "B", "KB", "MB", "GB", "TB" };
            int place = 0;
            while (length >= 1024 && place < (exts.Length - 1))
            {
                place++;
                length /= 1024;
            }
            return $"{length} {exts[place]}";
        }

        /// <summary>
        /// Open Error MessageBox
        /// </summary>
        public static void Error_box(string text)
        {
            string caption_ = "ERROR";
            MessageBox.Show(text, caption_, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Open Error showing Window
        /// </summary>
        public static void OpenErrorWin(Exception er)
        {
            Window ErrorWin = new ErrorWindow(er);
            // PartWindows.Add(ErrorWin);
            ErrorWin.Show();
        }

        /// <summary>
        /// Make http requests
        /// </summary>
        public static async Task<string> Make_request(string url)
        {
            HttpClient Http = new HttpClient();
            // var _con = new FormUrlEncodedContent(dictionary);
            var response = await Http.PostAsync(url, null);
            return await response.Content.ReadAsStringAsync();
        }
    }
}

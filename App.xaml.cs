using Kvyk.Telegraph;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
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
        public async void APPLICATION_START(object sender, StartupEventArgs e)
        {
            DarkMode = TelegraphApp.Properties.Settings.Default.DARK_MODE;
            string token = TelegraphApp.Properties.Settings.Default.ACCESS_TOKEN;
            if (token  == "")
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

        //public static JObject Make_request(string method, Dictionary<string, string> dictionary)
        //{
        //    HttpClient Http = new HttpClient();
        //    var _con = new FormUrlEncodedContent(dictionary);
        //    var response = Http.PostAsync(API_URI + method, _con).GetAwaiter().GetResult();
        //    string output = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        //    JObject json = JObject.Parse(output);
        //    return json;
        //}

        public static JObject Make_request(string method, string file)
        {

            byte[] bytes = File.ReadAllBytes(file);
            HttpContent content = new ByteArrayContent(bytes);
            var form = new MultipartFormDataContent();
            content.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");
            form.Add(content, "image", Path.GetFileName(file));
            HttpClient Http = new();
            var url = "https://telegra.ph/";
            var response = Http.PostAsync(url + method, form).GetAwaiter().GetResult();
            string output = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            JObject json;
            try
            {
                json = JObject.Parse(output);
            }
            catch (JsonReaderException)
            {

                string new_out = output[1..][..(output.Length - 2)];
                json = JObject.Parse(new_out);
            }
            return json;
        }

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
        public static void Error_box(string text)
        {
            string caption_ = "ERROR";
            MessageBox.Show(text, caption_, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

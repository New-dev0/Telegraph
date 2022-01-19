using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TelegraphApp.ViewPosts
{
    /// <summary>
    /// Interaction logic for ViewPosts.xaml
    /// </summary>
    public partial class ViewPosts : UserControl
    {
        public static Grid GRID = null;
        public ViewPosts()
        {
            InitializeComponent();
            MakeUpPage();
        }

        private void MakeUpPage()
        {
            Dictionary<string, string> dict = new()
            {
                {
                    "access_token",
                    Properties.Settings.Default.ACCESS_TOKEN
                }
            };
            JObject data = App.Make_request("getPageList", dict);
            // MessageBox.Show(data.ToString());
            if (data["ok"].ToString() == "True")
            {
                JObject inp = (JObject)data["result"];
                if (inp != null && inp["total_count"].ToString() == "0")
                {
                    Show_NoFeeds();
                    return;
                }
                DataToTemplate(inp);
                return;
            }
            if (data.GetValue("error") != null)
            {
                MessageBox.Show(data["error"].ToString());
            }

        }

        private void Show_NoFeeds()
        {
            Content = new TextBlock()
            {
                Width = 500,
                Height = 100,
                Text = "You have No Posts :(",
                FontSize = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontStyle = FontStyles.Italic,
                Opacity = 0.6,
                Foreground = Brushes.LightSeaGreen,
            };
        }

        private void DataToTemplate(JObject data)
        {
            //MessageBox.Show(data.ToString());
            RightGrid.Children.Clear();
            int curr = 1;
            int height = 15;
            int rows = 0;
            foreach (JToken tok in data.GetValue("pages").ToList())
            {
                // MessageBox.Show(tok.ToString());
                UserControl ui = new PostView(tok)
                {
                    Width = 150,
                    Height = 150,
                    VerticalAlignment = VerticalAlignment.Top


                };
                rows++;
                if (curr == 1)
                {
                    ui.HorizontalAlignment = HorizontalAlignment.Left;
                    ui.Margin = new Thickness(80, height, 0, 0);
                    curr++;
                }
                else if (curr == 2)
                {
                    {
                        ui.HorizontalAlignment = HorizontalAlignment.Center;
                        ui.Margin = new Thickness(0, height, 50, 0);
                        curr++;
                    }
                }
                else
                {
                    ui.HorizontalAlignment = HorizontalAlignment.Right;
                    ui.Margin = new Thickness(0, height, 130, 0);
                    curr = 1;
                    height += 150 + 25;

                }
                RightGrid.Children.Add(ui);
            }
            Content = RightGrid;
        }
    }
}

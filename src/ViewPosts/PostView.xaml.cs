using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace TelegraphApp.ViewPosts
{
    /// <summary>
    /// Interaction logic for PostView.xaml
    /// </summary>
    public partial class PostView : UserControl
    {
        public static string url;
        public PostView(JToken data)
        {
            InitializeComponent();
            TitleBox.Text = data["title"].ToString();
            Views.Text = data["views"].ToString();
            url = data["url"].ToString();
            // #TODO
            // URLBox.Text = url;
            Desc.Text = data["description"].ToString();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
    }
}

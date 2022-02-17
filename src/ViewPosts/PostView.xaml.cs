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
        public PostView(Kvyk.Telegraph.Models.Page data)
        {
            InitializeComponent();
            TitleBox.Text = data.Title;
            Views.Text = data.Views.ToString();
            url = data.Url.ToString();
            Desc.Text = data.Description;
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
        }
    }
}

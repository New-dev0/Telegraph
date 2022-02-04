using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TelegraphApp.MediaUpload
{
    /// <summary>
    /// Interaction logic for SingleUpload.xaml
    /// </summary>
    public partial class SingleUpload : UserControl
    {
    
        public SingleUpload(string path, string url)
        {
            InitializeComponent();
            Create_up_window(path, url);
            MainUserControl.Saved_Media_Page = this;

        }

        private void Create_up_window(string path, string url)
        {
            Uri path_ = new Uri(path);
            MediaMedia.Source = path_;
            BitmapImage img = new BitmapImage(path_);

            uRLbOX.Text = url;

            // Small Size square image.
            if (((int)(img.Height / img.Width)) == 1)
            {
                MediaMedia.Height = 150;
            }
            else
            {
                MediaMedia.Height = 420;
            }
            MediaMedia.Width = 150;
            string length = App.SimplifyFileLength((int)new FileInfo(path).Length);
            MediaName.Text = Path.GetFileName(path) + "   " + length;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(uRLbOX.Text);
            ClipCopy.Visibility = Visibility.Visible;
            await Task.Delay(2000);
            ClipCopy.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainUserControl.Saved_Media_Page = null;
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new UploadMedia());
        }
    }
}

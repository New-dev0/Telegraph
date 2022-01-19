using System;
using System.IO;
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
        private readonly DispatcherTimer timer = new DispatcherTimer();

        public SingleUpload(string path, string url)
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_TICK);
            timer.Interval = new TimeSpan(0, 0, 2);
            Create_up_window(path, url);
            MainUserControl.Saved_Media_Page = this;

        }
        private void Timer_TICK(object sender, EventArgs e)
        {
            ClipCopy.Visibility = Visibility.Collapsed;
            timer.IsEnabled = false;
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(uRLbOX.Text);
            ClipCopy.Visibility = Visibility.Visible;

            timer.Start();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MainUserControl.Saved_Media_Page = null;
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new UploadMedia());
        }
    }
}

using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using TelegraphApp.MediaUpload;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for UploadMedia.xaml
    /// </summary>
    public partial class UploadMedia : UserControl
    {
        private readonly DispatcherTimer timer = new DispatcherTimer();
        private bool INFO__ERRCOLOR_CHNGED = false;

        public UploadMedia()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(Timer_TICK);
            timer.Interval = new TimeSpan(0, 0, 10);
        }

        private void Timer_TICK(object sender, EventArgs e)
        {
            InfoBox.Visibility = Visibility.Collapsed;
            timer.IsEnabled = false;
        }

        private void Border_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files_ = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files_)
                {
                    if (Directory.Exists(file))
                    {
                        MessageBoxResult ans = Ask_confirmation();
                        if (ans.ToString() != "Yes")
                        {
                            return;
                        };
                        Multiple_upload_from_dir(file);
                        return;
                    }
                    if (!CheckISValidEXT(file))
                    {
                        TempShowInfoBox("Not A Valid file format.", true);
                        return;
                    }
                    string upl = Upload_file(file);
                    Show_out(file, upl);
                    return;
                };
            }
        }

        private void Multiple_upload_from_dir(string file)
        {
            string[] files = Directory.GetFiles(file);
            var result = new Dictionary<string, string> { };
            foreach (string _ in files)
            {
                string small = _.ToLowerInvariant();
                if (small.EndsWith(".jpg") || small.EndsWith(".png"))
                {
                    string url_or_error = Upload_file(_, true);
                    result.Add(_, url_or_error);
                }
            };
            if (result.Count == 0)
            {
                MessageBox.Show("No Image found in given directory.");
                return;
            }
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new MultipleUpload(result));
        }

        private MessageBoxResult Ask_confirmation()
        {
            return MessageBox.Show("Are you sure you want to Upload Available Images from this folder ?", "Telegraph", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private bool CheckISValidEXT(string path)
        {
            var List = new List<string> { ".JPG", ".PNG", ".GIF", ".MP4", ".JPEG" };
            return List.Contains(Path.GetExtension(path).ToUpperInvariant());
        }

        private void TempShowInfoBox(string text, bool error = false)
        {
            InfoBox.Text = text;
            if (error)
            {
                InfoBox.Foreground = Brushes.Red;
                INFO__ERRCOLOR_CHNGED = true;
            }
            else if (INFO__ERRCOLOR_CHNGED)
            {
                InfoBox.Foreground = (Brush)new BrushConverter().ConvertFromString("#FF251C1C");
                INFO__ERRCOLOR_CHNGED = false;
            }
            InfoBox.Visibility = Visibility.Visible;
            timer.Start();
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "JPEG-GIF Files(*.jpeg; *.png; *.gif; *.mp4; *.jpg)|*.gif; *.png; *.jpg; *.mp4; *.jpeg"
            };
            bool show = (bool)dialog.ShowDialog();
            if (show == true)
            {
                string file = Upload_file(dialog.FileName);
                // string file = "A";                     // Test Purpose
                Show_out(dialog.FileName, file);
            }
        }

        private void Show_out(string file_path, string url)
        {
            if (url != "")
            {
                App.RIGHT_GRID.Children.Clear();
                App.RIGHT_GRID.Children.Add(new SingleUpload(file_path, url)); ;
            }
        }


        private string Upload_file(string path, bool allow_error = false)
        {
            string filename = Path.GetFileName(path);
            TempShowInfoBox("Uploading " + filename);
            JObject tok = App.Make_request("upload", path);
            // MessageBox.Show(tok.ToString());
            if (tok.GetValue("error") != null)
            {
                string error = tok["error"].ToString();
                if (allow_error)
                {
                    return error;
                }
                TempShowInfoBox(filename + " : " + error, true);
                return "";
            }
            return "https://telegra.ph" + tok.GetValue("src").ToString();



        }
    }
}

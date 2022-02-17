using Kvyk.Telegraph.Exceptions;
using Kvyk.Telegraph.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        }


        private async void Border_Drop(object sender, DragEventArgs e)
        {
            try
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
                            await Multiple_upload_from_dir(file);
                            return;
                        }
                        if (!CheckISValidEXT(file))
                        {
                            TempShowInfoBox("Not A Valid file format.", true);
                            return;
                        }
                        string upl = await Upload_file(file);
                        Show_out(file, upl);
                        return;
                    };
                }
            }
            catch (Exception ex)
            {
                App.OpenErrorWin(ex);
            }
        }

        private async Task Multiple_upload_from_dir(string file)
        {
            try
            {
                string[] files = Directory.GetFiles(file);
                var result = new Dictionary<string, string> { };
                foreach (string _ in files)
                {
                    string small = _.ToLowerInvariant();
                    if (small.EndsWith(".jpg") || small.EndsWith(".png"))
                    {
                        string url_or_error = await Upload_file(_, true);
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
            catch (Exception ex)
            {
                App.OpenErrorWin(ex);
            }
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

        private async Task TempShowInfoBox(string text, bool error = false)
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
            await Task.Delay(2000);
            InfoBox.Visibility = Visibility.Collapsed;
        }

        private async void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog
            {
                DefaultExt = ".jpg",
                Filter = "JPEG-GIF Files(*.jpeg; *.png; *.gif; *.mp4; *.jpg)|*.gif; *.png; *.jpg; *.mp4; *.jpeg"
            };
            bool show = (bool)dialog.ShowDialog();
            if (show == true)
            {
                string file = await Upload_file(dialog.FileName);
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


        private async Task<string> Upload_file(string path, bool allow_error = false)
        {
            TelegraphFile tok;
            string filename = Path.GetFileName(path);
            try
            {
                await TempShowInfoBox("Uploading " + filename);
                string ext = Path.GetExtension(path).Substring(1);

                tok = await App.client.UploadFile(new FileToUpload { Bytes = File.ReadAllBytes(path), Type = "image/" + ext });
            }
            catch (TelegraphException er)
            {
                string error = er.Message;
                if (allow_error)
                {
                    return error;
                }
                await TempShowInfoBox(filename + " : " + error, true);
                return "";
            }
            catch (Exception er)
            {
                App.OpenErrorWin(er);
                return null;
            }
            return tok.Link;



        }
    }
}

using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace TelegraphApp.MediaUpload
{
    /// <summary>
    /// Interaction logic for MultipleUpload.xaml
    /// </summary>
    public partial class MultipleUpload : UserControl
    {
        private readonly Dictionary<string, string> STORED = new Dictionary<string, string> { };
        public MultipleUpload(Dictionary<string, string> dict)
        {
            InitializeComponent();
            Dict_to_datagrid(dict);
            MainUserControl.Saved_Media_Page = this;
        }

        private class TABLE
        {
            public string Path { get; set; }
            public string Output { get; set; }
        }

        private void Dict_to_datagrid(Dictionary<string, string> dict)
        {

            foreach (string value in dict.Keys)
            {
                string Name = Path.GetFileName(value);
                STORED.Add(Name, dict[value]);
                DataGrid.Items.Add(new TABLE { Path = Name, Output = dict[value] });
            }


        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string formated = JsonConvert.SerializeObject(STORED, Formatting.Indented);
            SaveFileDialog dialog = new SaveFileDialog
            {
                DefaultExt = ".json",
                FileName = "Telegraph"
            };
            var result = dialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(dialog.FileName, formated);
                MessageBox.Show("Saved", "Telegraph");
            }
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e) { OpenCtMenu(); }
        private void OpenCtMenu() { if (!CTMenu.IsOpen) { CTMenu.IsOpen = true; }; }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            string output = "";
            foreach (string link in STORED.Values)
            {
                output += $"{link}\n";
            }
            // remove new line
            output = output[0..^1];
            SaveFileDialog dialog = new()
            {
                DefaultExt = ".txt",
                FileName = "Telegraph"
            };
            var result = dialog.ShowDialog();
            if (result == true)
            {
                File.WriteAllText(dialog.FileName, output);
                MessageBox.Show("Saved", "Telegraph");
            }
        }

        private void TextBlock_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            CTMenu.IsOpen = false;
            MainUserControl.Saved_Media_Page = null;
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new UploadMedia());
        }

        // private void Border_MouseEnter(object sender, MouseEventArgs e) { OpenCtMenu(); }
    }
}

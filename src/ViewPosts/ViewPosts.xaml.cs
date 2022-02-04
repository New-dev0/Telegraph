using Kvyk.Telegraph.Exceptions;
using Kvyk.Telegraph.Models;
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

        private async void MakeUpPage()
        {
            PageList pages;
            try
            {
                pages = await App.client.GetPageList();
            }
            catch (TelegraphException ex)
            {
                App.Error_box(ex.Message);
                return;
            }
            if (pages.TotalCount == 0)
            {
                Show_NoFeeds();
                return;
            }
            DataToTemplate(pages.Pages);
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

    private void DataToTemplate(List<Kvyk.Telegraph.Models.Page> data)
    {
        //MessageBox.Show(data.ToString());
        RightGrid.Children.Clear();
        int curr = 1;
        int height = 15;
        int rows = 0;
        foreach (Kvyk.Telegraph.Models.Page tok in data)
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

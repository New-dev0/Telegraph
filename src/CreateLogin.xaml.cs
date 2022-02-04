using Kvyk.Telegraph.Exceptions;
using Kvyk.Telegraph.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for CreateLogin.xaml
    /// </summary>
    public partial class CreateLogin : UserControl
    {

        private List<TextBlock> Tblocks;

        public CreateLogin()
        {
            InitializeComponent();
            Tblocks = new List<TextBlock> { T1, T2, T3 };
            ToggleMode();
        }


        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            App.DarkMode = !App.DarkMode;
            ToggleMode();
        }

        private void ToggleMode()
        {
            string button_icon;

            if (App.DarkMode)
            {
                Brush BgCOLOR = Helper.Convert("#37474F");
                HeaBG.Background = BgCOLOR;
                HealLa.Foreground = Brushes.White;
                Background = BgCOLOR;
                Conbrd.Background = Helper.Convert("#263238");
                SMBUT.Background = Helper.Convert("#4A5459");
                button_icon = "moon";
                foreach (TextBlock block in Tblocks)
                {
                    block.Background = null;
                    block.Foreground = Brushes.White;
                }
            }
            else
            {
                Brush BGColor = Helper.Convert("#ADD8E6");
                HeaBG.Background = BGColor;
                HealLa.Foreground = Helper.Convert("#FF32656F");
                Background = BGColor;
                Conbrd.Background = Brushes.Azure;
                SMBUT.Background = Helper.Convert("#FF8B7ADE");
                button_icon = "sun";
                foreach (TextBlock block in Tblocks)
                {
                    block.Background = Brushes.White;
                    block.Foreground = Brushes.Black;
                }
            }
            Imsu.Source = new BitmapImage(
                new Uri($"/src/assets/outlined_{button_icon}.png", UriKind.Relative)
            );
            ExpandOrCollapse();
            DakLigBu.IsChecked = App.DarkMode;
        }

        private void Ecpa_Expanded(object sender, RoutedEventArgs e)
        {
            ExpandOrCollapse();
        }


        private void Ecpa_Collapsed(object sender, RoutedEventArgs e)
        {
            ExpandOrCollapse();
        }

        private void ExpandOrCollapse()
        {
            Brush Back;
            Brush Fore;
            if (Ecpa.IsExpanded && App.DarkMode)
            {
                Back = Helper.Convert("#131418");
                Fore = Brushes.White;
            }
            else
            {
                Back = Helper.Convert("#FFF5F4F1");
                Fore = Helper.Convert("#FF192D3A");
            }
            Ecpa.Foreground = Fore;
            bord.Background = Back;

        }

        private void BttEXT_MouseEnter(object sender, MouseEventArgs e)
        {
            IConimg.Visibility = Visibility.Collapsed;
            BttEXT.Foreground = Helper.Convert("#00695C");
        }

        private void BttEXT_MouseLeave(object sender, MouseEventArgs e)
        {
            IConimg.Visibility = Visibility.Visible;
            BttEXT.Foreground = Brushes.White;
        }

        private void ShortBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ShortBox.Text.Length > 32)
            {
                App.Error_box("Short Name should be of length: 1-32");
            }
        }

        private void AUTHOR_INPUT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AuthorBox.Text.Length > 120)
            {
                App.Error_box("Author Name should be of length 0-120");

            }
        }



        private void Profile_URL_BOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ProUrlBox.Text.Length > 512)
            {
                App.Error_box("Author URL should be of length 0-512");

            }
        }


        private async void SMBUT_Click(object sender, RoutedEventArgs e)
        {
            Account acc;
            if (AccessBox.Text != "")
            {
                App.client.AccessToken = AccessBox.Text;
                try
                {
                    acc = await App.client.GetAccountInfo();
                }
                catch (TelegraphException ex)
                {
                    App.client.AccessToken = null;
                    App.Error_box(ex.Message);
                    return;
                }
            }
            else
            {
                if (ShortBox.Text == "")
                {
                    App.Error_box("Short Name can't be Empty.");
                    return;
                }
                string short_name = ShortBox.Text;
                string author_name = null;
                string profile_url = null;

                if (AuthorBox.Text != "")
                {
                    author_name = AuthorBox.Text;
                };
                if (ProUrlBox.Text != "")
                {
                    profile_url = ProUrlBox.Text;
                }
                try
                {
                    acc = await App.client.CreateAccount(short_name, author_name, profile_url);
                }
                catch (TelegraphException ex)
                {
                    if (ex.Message == "AUTHOR_URL_INVALID")
                    {
                        App.Error_box("Invalid Url in Profle Url Box.");
                        return;
                    }
                    MessageBox.Show("what");
                    MessageBox.Show(ex.Message);
                    return;
                };
            }
            App.client.AccessToken = acc.AccessToken;
            Properties.Settings.Default.ACCESS_TOKEN = acc.AccessToken;
            Properties.Settings.Default.Save();
            Window win = Window.GetWindow(this);
            win.WindowState = WindowState.Maximized;
            win.Content = new MainUserControl(true);

            }
    }
}
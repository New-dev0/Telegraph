using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for CreateLogin.xaml
    /// </summary>
    public partial class CreateLogin : UserControl
    {
        public string name = "";
        public string author_name = "";
        public string short_url = "";
        private Brush submit_button_bg;
        private bool SAVED_BG = false;

        public CreateLogin()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TEXTBOX.Text.Length > 32)
            {
                App.Error_box("Short Name should be of length: 1-32");
            }
            else
            {
                name = TEXTBOX.Text;
            }
        }

        /*
          private void AUTHOR_PLACEHOLDER_MouseEnter(object sender, MouseEventArgs e)
          {
              AUTHOR_PLACEHOLDER.Visibility = Visibility.Hidden;
          }
        */

        private void AUTHOR_INPUT_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AUTHOR_INPUT.Text.Length > 120)
            {
                App.Error_box("Author Name should be of length 0-120");

            }
            else
            {
                author_name = AUTHOR_INPUT.Text;
            }
        }


        private void AUTHOR_PLACEHOLDER_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AUTHOR_PLACEHOLDER.Visibility = Visibility.Hidden;
        }

        private void AUTH_URL_BOX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (AUTH_URL_BOX.Text.Length > 512)
            {
                App.Error_box("Author URL should be of length 0-512");

            }
            else
            {
                author_name = AUTH_URL_BOX.Text;
            }
        }

        private void AUT_URI_PLACEHOLDER_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            AUT_URI_PLACEHOLDER.Visibility = Visibility.Hidden;
        }


        private void SUB_BUTTON_MouseEnter(object sender, MouseEventArgs e)
        {
            submit_button_bg = SUB_BUTTON.Background;
            SAVED_BG = true;
            SUB_BUTTON.Background = Brushes.White;
            SUB_BUTTON.Foreground = Brushes.Black;
        }

        private void SUB_BUTTON_MouseLeave(object sender, MouseEventArgs e)
        {
            if (SAVED_BG)
            {
                SUB_BUTTON.Background = submit_button_bg;
                SUB_BUTTON.Foreground = Brushes.White;
            }
        }

        private void SUB_BUTTON_Click(object sender, RoutedEventArgs e)
        {
            if (name == "")
            {
                App.Error_box("Shortname is Mandatory.");
                return;
            }
            Create_account();
            Window win = Window.GetWindow(this);
            win.Width = 900;
            win.Height = 500;
            win.Content = new MainUserControl(true);
        }

        private void Create_account()
        {
            var Value = new Dictionary<string, string>
            {
                {"short_name", name},
            };
            if (author_name != "")
            {
                Value.Add("author_name", author_name);
            };
            if (short_url != "")
            {
                Value.Add("author_url", short_url);
            }
            JObject result = App.Make_request("createAccount", Value);
            if (result["ok"].ToString() != "true" && result.GetValue("error") != null)
            {
                App.Error_box(result["error"].ToString());
                return;
            }
            Properties.Settings.Default.ACCESS_TOKEN = result["result"]["access_token"].ToString();
            Properties.Settings.Default.AUTH_URL = result["result"]["auth_url"].ToString();
            Properties.Settings.Default.Save();


        }
    }
}

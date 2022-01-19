using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for MainUserControl.xaml
    /// </summary>
    public partial class MainUserControl : UserControl
    {
        public static UserControl Saved_Media_Page = null;
        public MainUserControl(bool open_main_page = false)
        {
            InitializeComponent();
            App.RIGHT_GRID = RightGrid;
            if (open_main_page)
            {
                Press_button_one();
            };
        }

        private Button CLICKED_BUTTON;
        private bool is_button_clicked = false;

        private void ButtonFirst_Click(object sender, RoutedEventArgs e)
        {
            Press_button_one();

        }

        private void Press_button_one()
        {
            Button_press(ButtonFirst);
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new ViewPosts.ViewPosts());
        }

        private void Button_press(Button button)
        {
            if (is_button_clicked)
            {
                CLICKED_BUTTON.Background = null;
                CLICKED_BUTTON.Foreground = Brushes.White;
            }
            button.Background = Brushes.WhiteSmoke;
            button.Foreground = Brushes.BlueViolet;
            CLICKED_BUTTON = button;
            is_button_clicked = true;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Button_press(Button2);
            App.RIGHT_GRID.Children.Clear();
            App.RIGHT_GRID.Children.Add(new CreatePost.PageCreate());
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Button_press(Button3);
            RightGrid.Children.Clear();
            UserControl control;
            if (Saved_Media_Page != null)
            {
                control = Saved_Media_Page;
            }
            else
            {
                control = new UploadMedia();
            }

            RightGrid.Children.Add(control);
        }

        //private void ButtonL_Click(object sender, RoutedEventArgs e)
        //{
        //    Button_press(ButtonL);
        //}

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Button_press(Button4);
            RightGrid.Children.Clear();
            RightGrid.Children.Add(new LogoutControl());

        }
    }
}

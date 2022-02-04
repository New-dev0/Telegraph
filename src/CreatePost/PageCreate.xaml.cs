using Kvyk.Telegraph;
using Kvyk.Telegraph.Parsers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace TelegraphApp.CreatePost
{
    /// <summary>
    /// Interaction logic for PageCreate.xaml
    /// </summary>
    public partial class PageCreate : UserControl
    {
        private TelegraphClient client = null;

        public PageCreate()
        {
            InitializeComponent();
        }

        private void SpelChec_Checked(object sender, RoutedEventArgs e)
        {
            Texty.SpellCheck.IsEnabled = (bool)SpelChec.IsChecked;
        }

        private async void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (TitleBox.Text.Length == 0)
            {
                App.Error_box("Title can`t be Empty!");
                return;
            }
            TextRange rt = new(Texty.Document.ContentStart, Texty.Document.ContentEnd);
            if (rt.Text.Length == 0)
            {
                App.Error_box("Content can`t be Empty!");
                return;
            }
            if (client == null)
            {
                client = new TelegraphClient()
                {
                    AccessToken = Properties.Settings.Default.ACCESS_TOKEN
                };
            }
            var Mk = new TelegraphMarkdown();
            var nodes = Mk.ParseMarkdown(rt.Text);
            var result = await client.CreatePage(TitleBox.Text, nodes);
            MessageBox.Show(result.ToString());
            MessageBox.Show("Created");

        }
    }
}

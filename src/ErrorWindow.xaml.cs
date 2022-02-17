using System;
using System.Windows;

namespace TelegraphApp
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {

        private Exception exc;
        private bool AlreadyReported = false;

        public ErrorWindow(Exception er)
        {
            InitializeComponent();
            exc = er;
            ErrorBox.Text = er.Message;
            ErrorDescBox.Text = er.StackTrace;
        }

        // #TODO
        //  void ChooseMode()
        // ErrorWindow in dark Mode 

        private async void ReportBug_Click(object sender, RoutedEventArgs e)
        {
            if (AlreadyReported)
            {
                MessageBox.Show("You have already reported the Bug.\nYou can't report the same bug again...");
                return;
            }
            string content = exc.Message + "\n\n" + exc.StackTrace;
            string res = await App.Make_request($"https://newdev0.vercel.app/telegraph/report_bug?content={content}");
            if (res == "true")
            {
                AlreadyReported = true;
                MessageBox.Show("Successfully reported!", "Telegraph", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show(res, "Telegraph", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}

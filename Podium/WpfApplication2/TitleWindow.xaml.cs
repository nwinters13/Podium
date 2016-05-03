/*
/   The code behind for the Home Window
/       We haven't implemented the actual authorization and authentication yet
/       So it mostly is just to get you to the next window
*/

using System.Windows;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for TitleWindow.xaml
    /// </summary>
    public partial class TitleWindow : Window
    {
        public TitleWindow()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        // Takes you to the Home Window
        // Makes sure sizing remains the same
        private void button_login_Click(object sender, RoutedEventArgs e)
        {
            HomeWindow window = new HomeWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            window.Show();
            this.Close();
        }

    }
}

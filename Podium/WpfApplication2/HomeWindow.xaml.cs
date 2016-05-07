/*
/ The HomeWindow is the initial window after logign in that shows all of the buttons
/  It serves as the "hub of the app" before starting an interview
/
*/

using System.Windows;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public bool isPauseEnabled;
        public bool isFeedbackEnabled;
        public string user_email;

        public HomeWindow()
        {
            InitializeComponent();
            isPauseEnabled = true;
            isFeedbackEnabled = true;
            user_email = "";
        }

        // The button to start the interview, passes forward the options
        private void button_startinterview_Click(object sender, RoutedEventArgs e)
        {
            InterviewWindow window = new InterviewWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.isPauseEnabled = this.isPauseEnabled;
            window.isFeedbackEnabled = this.isFeedbackEnabled;
            window.Show();
            this.Close();
        }

        // Opens up the options window but DOES NOT close itself because the options window will pass back info
        private void button_options_Click(object sender, RoutedEventArgs e)
        {
            OptionsWindow window = new OptionsWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.home = this;
            window.updateForm();
            window.Show();
        }

        private void button_history_Click(object sender, RoutedEventArgs e)
        {
            HistoryWindow window = new HistoryWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.Show();
            window.getHistory(user_email);
        }
    }
}

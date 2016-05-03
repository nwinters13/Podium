using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public bool isPauseEnabled;
        public bool isFeedbackEnabled;

        public HomeWindow()
        {
            InitializeComponent();
            isPauseEnabled = true;
            isFeedbackEnabled = true;
        }

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
    }
}

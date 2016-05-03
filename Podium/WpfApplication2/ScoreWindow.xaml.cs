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
    /// Interaction logic for ScoreWindow.xaml
    /// </summary>
    public partial class ScoreWindow : Window
    {
        float score;
        string worst_gesture;

        public ScoreWindow()
        {
            InitializeComponent();
            score = 0;
        }

        public void setScore(float newScore)
        {
            score = newScore;
            this.label_score.Content = score;
        }

        public void setWorstGesture(string gesture_name)
        {
            worst_gesture = gesture_name;
            if (worst_gesture != "default")
                this.label_suggestion.Content = "Next time try to work on how much you " + worst_gesture;
            else
                this.label_suggestion.Content = "";
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("hello");
            if (e.Key == Key.R)
            {
                TitleWindow window = new TitleWindow();
                window.Width = this.ActualWidth;
                window.Height = this.ActualHeight;
                if (this.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Maximized;
                }
                window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                //window.setScore(this.gestureDetector.GestureResultView.Overall_Score);

                window.Show();
                this.Close();
            }
        }

        private void button_end_interview_click(object sender, RoutedEventArgs e)
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

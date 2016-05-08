/*
/   Score window displays the score you received on your interview
/       as well as the gesture that the system recognized as the most negative to your interviewing
/
/   To do: Display a more in depth score breakdown
           Allow for scores to be saved
*/


using System.Windows;
using System.Windows.Input;

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

        // Once the ScoreWindow class has been initialized we can display the score that gets passed in
        public void setScore(float newScore)
        {
            score = newScore;
            this.label_score.Content = score;
        }

        // Once the ScoreWindow is initialized we can also display the worst gesture
        public void setWorstGesture(string gesture_name)
        {
            worst_gesture = gesture_name;
            if (worst_gesture != "default")
                this.label_suggestion.Content = "Next time try to work on how much you " + worst_gesture;
            else
                this.label_suggestion.Content = "";
        }

        // We can reset back to the original screen using R for reset
        // To do: fix kinect from breaking this is currently not a button because of a memory issue with the Kinect
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
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

        // Handles ending the interview
        private void button_end_interview_click(object sender, RoutedEventArgs e)
        {
            // Commented out for now because of window re-initialization issues
            /*
            HomeWindow window = new HomeWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            
            window.Show();
            */

            this.Close();
        }
    }

}

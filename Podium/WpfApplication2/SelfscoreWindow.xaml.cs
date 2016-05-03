/*
/   The self score window will in the future push the qualitative feedback to the database
/       So that it can be stored and looked at for future breakdown
/
/   Right now the code helps transition from the interview screen to the score screen (including passing through necessary information)
        Future to do: have a scoring class that just gets passed through so multiple values don't need to be set
*/

using System.Windows;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SelfscoreWindow : Window
    {
        float score;
        string worst_gesture;

        public SelfscoreWindow()
        {
            InitializeComponent();
            score = 0;
        }

        // The transition code
        //  that makes sure sizing remains the same
        private void button_getscore_Click(object sender, RoutedEventArgs e)
        {
            ScoreWindow window = new ScoreWindow();
            window.Width = this.ActualWidth;
            window.Height = this.ActualHeight;
            if (this.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
            }
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            window.setScore(this.score);
            window.setWorstGesture(this.worst_gesture);
            window.Show();
            this.Close();
        }

        // The code to pass through the score
        public void setScore(float newScore)
        {
            score = newScore;
        }


        // The code to pass through the most seen gesture
        public void setWorstGesture(string gesture_name)
        {
            worst_gesture = gesture_name;
        }


    }
}

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

        public void setScore(float newScore)
        {
            score = newScore;
        }

        public void setWorstGesture(string gesture_name)
        {
            worst_gesture = gesture_name;
        }


    }
}

/*
/   The self score window will in the future push the qualitative feedback to the database
/       So that it can be stored and looked at for future breakdown
/
/   Right now the code helps transition from the interview screen to the score screen (including passing through necessary information)
        Future to do: have a scoring class that just gets passed through so multiple values don't need to be set
*/

using System.Windows;
using System.Windows;
using System;  // C# , ADO.NET
using C = System.Data.SqlClient; // System.Data.dll

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class SelfscoreWindow : Window
    {

        public string user_email;
        float score;
        string worst_gesture;

        public SelfscoreWindow()
        {
            InitializeComponent();
            score = 0;
            user_email = "";
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
            postScore();
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


        public void postScore()
        {
            C.SqlConnection connection;
            try
            {
                connection = new C.SqlConnection(
                    "Server=tcp:podium1.database.windows.net,1433;Database=ReKinect;User ID=podium@podium1;Password=I<3rekinect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                    );
                connection.Open();

                C.SqlCommand cmd = new C.SqlCommand();
                C.SqlDataReader reader;

                cmd.CommandText = "INSERT INTO Results VALUES ('" +
                    user_email + "', " +
                    this.score + ", " +
                    textbox_selfscore.Text + ", '" +
                    textbox_didwell.Text + "', '" +
                    textbox_improve.Text + "')";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = connection;
                reader = cmd.ExecuteReader();
                System.Diagnostics.Debug.WriteLine(cmd.CommandText);

            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("SQL Connection failed");
            }



        }

    }
}

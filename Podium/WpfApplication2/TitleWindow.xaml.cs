/*
/   The code behind for the Home Window
/       We haven't implemented the actual authorization and authentication yet
/       So it mostly is just to get you to the next window
*/

using System.Windows;
using System;  // C# , ADO.NET
using System.Security.Cryptography;
using C = System.Data.SqlClient; // System.Data.dll

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
            this.StatusBox.Text = "";

            C.SqlConnection connection;
            try
            {
                // Open connection to Podium db
                connection = new C.SqlConnection(
                   "Server=tcp:podium1.database.windows.net,1433;Database=ReKinect;User ID=podium@podium1;Password=I<3rekinect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   );
                connection.Open();

                C.SqlCommand cmd = new C.SqlCommand();
                C.SqlDataReader reader;

                var email = this.text_email.Text;
                var pword = this.passwordBox_password.Password;

                if (email != "" && pword != "")
                {
                    cmd.CommandText = "SELECT salt, passhash FROM users WHERE email='" + email + "'";
                    System.Diagnostics.Debug.WriteLine(cmd.CommandText);
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // extract from query
                            var salt = reader.GetString(0);
                            var phash = reader.GetString(1);

                            // hash password entered in form
                            pword = salt + pword;
                            SHA256 mySHA256 = SHA256Managed.Create();
                            byte[] hash = mySHA256.ComputeHash(System.Text.Encoding.Default.GetBytes(pword));

                            if (phash == System.Text.Encoding.Default.GetString(hash))
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
                            else
                            {
                                this.StatusBox.Text = "Incorrect password";
                            }

                        }

                    }
                    else
                    {
                        this.StatusBox.Text = "Email not found";
                    }

                    reader.Close();
                    connection.Close();
                    System.Diagnostics.Debug.WriteLine("closed them I think");
                }
                else
                {
                    this.StatusBox.Text = "Email and/or Password field cannot be blank";
                    System.Diagnostics.Debug.WriteLine("Email or Password field is blank");
                }

            }
            catch
            {
                this.StatusBox.Text = "There was an issue connecting to the database. Try again?";
                return;
            }
        }

        private void button_register_Click(object sender, RoutedEventArgs e)
        {
            // clear status text
            this.StatusBox.Text = "";

            C.SqlConnection connection;
            try
            {
                // Open connection to Podium db
                connection = new C.SqlConnection(
                   "Server=tcp:podium1.database.windows.net,1433;Database=ReKinect;User ID=podium@podium1;Password=I<3rekinect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   );
                connection.Open();
           
                C.SqlCommand cmd = new C.SqlCommand();
                C.SqlDataReader reader;

                var email = this.text_email.Text;
                var pword = this.passwordBox_password.Password;

                if (email != "" && pword != "")
                {
                    // Check if email already in use
                    cmd.CommandText = "SELECT email FROM users WHERE email='" + email + "'";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Connection = connection;
                    reader = cmd.ExecuteReader();

                    // if not in use
                    if (!reader.HasRows)
                    {
                        // have to close reader - readers monopolize SQL connection stream
                        reader.Close();

                        // generate password salt
                        byte[] random = new byte[32];
                        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                        rng.GetNonZeroBytes(random);
                        var salt = System.Text.Encoding.Default.GetString(random);
                        pword = salt + pword;

                        // hash new salt + password
                        SHA256 mySHA256 = SHA256Managed.Create();
                        byte[] hash = mySHA256.ComputeHash(System.Text.Encoding.Default.GetBytes(pword));
                        pword = System.Text.Encoding.Default.GetString(hash);

                        // add user to database
                        cmd.CommandText = "INSERT INTO users (email, salt, passhash, firstName) VALUES ('" +
                                              email + "', '" +
                                              salt + "', '" +
                                              pword + "', '" +
                                              "Tester" + "')";
                        System.Diagnostics.Debug.WriteLine(cmd.CommandText);

                        //should probably be in a try/catch or something
                        try
                        {
                            reader = cmd.ExecuteReader();
                            this.StatusBox.Text = "Successfully registered! Click 'Log In' to start!";
                        }
                        catch
                        {
                            this.StatusBox.Text = "Something went wrong trying to register you";
                        }

                    }
                    else
                    {
                        this.StatusBox.Text = "Email already in use. Use another email";
                    }

                    reader.Close();
                    connection.Close();

                }
                else
                {
                    this.StatusBox.Text = "Email and/or Password field cannot be blank";
                }
            }
            catch
            {
                this.StatusBox.Text = "There was an issue connecting to the database. Try again?";
                return;
            }
        
        }

    }
}

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
using System.Data;
using System.Data.SqlClient; // System.Data.dll=

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for HistoryWindow.xaml
    /// </summary>
    public partial class HistoryWindow : Window
    {
        // "SELECT * from Results WHERE email='" + email + "'"
        public HistoryWindow()
        {
            InitializeComponent();
        }

        private void button_returnhome_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void getHistory(string email)
        {
            string select = "SELECT obj_score AS \"Kinect Score\", subj_score AS \"Your Score\", did_well AS \"What you did well\", improve AS \"What needs improvement\" from Results WHERE email='" + email + "'";
            SqlConnection c = new SqlConnection(
                   "Server=tcp:podium1.database.windows.net,1433;Database=ReKinect;User ID=podium@podium1;Password=I<3rekinect;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
                   );
            SqlDataAdapter dataAdapter = new SqlDataAdapter(select, c.ConnectionString);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            DataTable table = new DataTable();
            table.Locale = System.Globalization.CultureInfo.InvariantCulture;
            dataAdapter.Fill(table);

            dataGrid_history.IsReadOnly = true;
            dataGrid_history.ItemsSource = table.DefaultView;
        }
    }


}

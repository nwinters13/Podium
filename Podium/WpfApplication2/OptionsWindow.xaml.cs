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
    /// Interaction logic for OptionsWindow.xaml
    /// </summary>
    public partial class OptionsWindow : Window
    {
        public HomeWindow home;

        public OptionsWindow()
        {
            InitializeComponent();
            home = null;
        }

        private void button_saveoptions_Click(object sender, RoutedEventArgs e)
        {
            home.isPauseEnabled = (bool)this.checkBoxPause.IsChecked;
            home.isFeedbackEnabled = (bool)this.checkBoxFeedback.IsChecked;
            this.Close();
        }

        public void updateForm()
        {
            this.checkBoxPause.IsChecked = home.isPauseEnabled;
            this.checkBoxFeedback.IsChecked = home.isFeedbackEnabled;
        }
    }
}

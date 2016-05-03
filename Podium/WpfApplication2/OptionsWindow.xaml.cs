/*
/   The options window allows you to enable and disable the pausing features and the live feedback feature
/   These two options carry through to the interview screen to prevent the features from being activated
/   if necessary
/
*/

using System.Windows;

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

        // Saves the options and closes the class out
        private void button_saveoptions_Click(object sender, RoutedEventArgs e)
        {
            home.isPauseEnabled = (bool)this.checkBoxPause.IsChecked;
            home.isFeedbackEnabled = (bool)this.checkBoxFeedback.IsChecked;
            this.Close();
        }

        // Updates the form based on what the current settings are
        //  So that the page reflects the current settings
        public void updateForm()
        {
            this.checkBoxPause.IsChecked = home.isPauseEnabled;
            this.checkBoxFeedback.IsChecked = home.isFeedbackEnabled;
        }
    }
}

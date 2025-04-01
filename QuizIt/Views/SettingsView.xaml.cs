using QuizIt.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuizIt.Views
{
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
            UsernameBox.Text = Properties.Settings.Default.Username;
            DarkModeCheckBox.IsChecked = Properties.Settings.Default.DarkMode;
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Username = UsernameBox.Text;
            Properties.Settings.Default.DarkMode = DarkModeCheckBox.IsChecked == true;
            Properties.Settings.Default.Save();

            MessageBox.Show("Ustawienia zapisane");

            var main = Application.Current.MainWindow as MainWindow;
            var vm = main.DataContext as MainViewModel;
            vm.RefreshUsername();
        }

        private void DarkModeChanged(object sender, RoutedEventArgs e)
        {
            var dark = DarkModeCheckBox.IsChecked == true;
            ((App)Application.Current).ApplyTheme(dark);

            var main = Application.Current.MainWindow as MainWindow;
            if (main != null)
            {
                var vm = main.DataContext as MainViewModel;
                var content = main.MainContentControl.Content;
                main.MainContentControl.Content = null;
                main.MainContentControl.Content = content;
            }
        }
    }
}

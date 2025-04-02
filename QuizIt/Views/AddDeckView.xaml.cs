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
using QuizIt.ViewModels;

namespace QuizIt.Views
{
    public partial class AddDeckView : UserControl
    {
        public AddDeckView(MainViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private readonly MainViewModel _viewModel;

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var name = DeckNameTextBox.Text;
            if (!string.IsNullOrWhiteSpace(name))
            {
                _viewModel.AddDeck(name);
                DeckNameTextBox.Text = string.Empty;
            }
        }
    }
}

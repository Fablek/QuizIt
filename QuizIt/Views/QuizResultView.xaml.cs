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
    public partial class QuizResultView : UserControl
    {
        public QuizResultView(int score, int total)
        {
            InitializeComponent();
            ResultText.Text = $"Twój wynik: {score} / {total}";
        }
    }
}

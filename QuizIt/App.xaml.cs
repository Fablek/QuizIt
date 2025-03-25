using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using QuizIt.Properties;

namespace QuizIt;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        ApplyTheme(Settings.Default.DarkMode);
    }

    public void ApplyTheme(bool dark)
    {
        if (dark)
        {
            Resources["BackgroundColor"] = new SolidColorBrush(Color.FromRgb(30, 30, 30));
            Resources["ForegroundColor"] = Brushes.White;
        }
        else
        {
            Resources["BackgroundColor"] = Brushes.White;
            Resources["ForegroundColor"] = Brushes.Black;
        }
    }
}
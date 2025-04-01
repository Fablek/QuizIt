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

    public void ApplyTheme(bool darkMode)
    {
        var dict = new ResourceDictionary();

        if (darkMode)
            dict.Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative);
        else
            dict.Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative);

        var oldDict = Resources.MergedDictionaries
            .FirstOrDefault(d => d.Source != null && d.Source.OriginalString.Contains("Theme"));

        if (oldDict != null)
            Resources.MergedDictionaries.Remove(oldDict);

        Resources.MergedDictionaries.Add(dict);
    }
}
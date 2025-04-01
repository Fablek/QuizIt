using QuizIt.ViewModels;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using QuizIt.Views;
using static QuizIt.Data.AppDbContext;

namespace QuizIt
{
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DbInitializer.Initialize();

            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Username))
            {
                Properties.Settings.Default.Username = Environment.UserName;
                Properties.Settings.Default.Save();
            }

            ((App)Application.Current).ApplyTheme(Properties.Settings.Default.DarkMode);

            _mainViewModel = new MainViewModel();
            DataContext = _mainViewModel;

            MainContentControl.Content = new DecksView(_mainViewModel);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableAcrylicBlur();
        }

        private void EnableAcrylicBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy
            {
                AccentState = AccentState.ACCENT_ENABLE_ACRYLICBLURBEHIND,
                AccentFlags = 2,
                GradientColor = unchecked((int)0xE61E1E2E)
            };

            int size = Marshal.SizeOf(accent);
            IntPtr accentPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData
            {
                Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY,
                SizeOfData = size,
                Data = accentPtr
            };

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);
            Marshal.FreeHGlobal(accentPtr);
        }

        private void TitleBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void MinimizeApp_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ShowDecksView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new DecksView(_mainViewModel);
        }

        private void StartQuiz_Click(object sender, RoutedEventArgs e)
        {
            if (_mainViewModel.Decks.Count > 0)
                MainContentControl.Content = new SelectDeckView(_mainViewModel);
            else
                MessageBox.Show("Brak talii do rozpoczęcia quizu.");
        }

        private void ShowResultsView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new ResultsView();
        }

        private void ShowAddDeckView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new AddDeckView(_mainViewModel);
        }

        private void ShowSettingsView_Click(object sender, RoutedEventArgs e)
        {
            MainContentControl.Content = new SettingsView();
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct AccentPolicy
        {
            public AccentState AccentState;
            public int AccentFlags;
            public int GradientColor;
            public int AnimationId;
        }

        internal enum AccentState
        {
            ACCENT_DISABLED = 0,
            ACCENT_ENABLE_GRADIENT = 1,
            ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
            ACCENT_ENABLE_BLURBEHIND = 3,
            ACCENT_ENABLE_ACRYLICBLURBEHIND = 4,
            ACCENT_ENABLE_HOSTBACKDROP = 5,
            ACCENT_INVALID_STATE = 6
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct WindowCompositionAttributeData
        {
            public WindowCompositionAttribute Attribute;
            public IntPtr Data;
            public int SizeOfData;
        }

        internal enum WindowCompositionAttribute
        {
            WCA_ACCENT_POLICY = 19
        }

        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
    }
}

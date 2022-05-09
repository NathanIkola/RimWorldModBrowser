using RimWorldModBrowser.Code;
using RimWorldModBrowser.Code.ViewModels;
using System.Windows;
using System.Windows.Input;
using SettingsView = RimWorldModBrowser.Views.Settings;

namespace RimWorldModBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;

            Settings.Load(Constants.SettingsFile); 
            SetWindowOnStartup();

            // if this is the first run, load the settings dialog
            if (Settings.Lookup(Constants.FirstRunKey) is null)
                ShowSettings();

            ViewModel.LoadMods();
        }

        private void SetWindowOnStartup()
        {
            if (int.TryParse(Settings.Lookup(Constants.LastWindowWidth), out int width))
                Width = width;
            if (int.TryParse(Settings.Lookup(Constants.LastWindowHeight), out int height))
                Height = height;
            if (bool.TryParse(Settings.Lookup(Constants.IsMaximized), out bool maximized))
                WindowState = maximized ? WindowState.Maximized : WindowState.Normal;

            if (int.TryParse(Settings.Lookup(Constants.WindowTop), out int top))
                Top = top;
            if (int.TryParse(Settings.Lookup(Constants.WindowLeft), out int left))
                Left = left;
        }

        /// <summary>
        /// The view model for this window
        /// </summary>
        private readonly MainViewModel ViewModel = new();

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ShowSettings();

            // reload mods based on the settings
            ViewModel.LoadMods();
            modList.Refresh();
        }

        private void ShowSettings()
        {
            SettingsView sv = new();
            sv.Show();

            sv.Top = Top + (ActualHeight - sv.ActualHeight) / 2;
            sv.Left = Left + (ActualWidth - sv.ActualWidth) / 2;
            sv.Hide();

            sv.ShowDialog();
        }

        private void OnFocusSearchBar(object sender, ExecutedRoutedEventArgs e)
        {
            modList.OnFocusSearchBar(sender, e);
        }

        private void OnOpenSteam(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.SteamClick(sender, e);
        }

        private void OnOpenDnSpy(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.DnSpyClick(sender, e);
        }

        private void OnOpenExplorer(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.FolderClick(sender, e);
        }

        private void OnOpenSettings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsClick(sender, e);
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            // ignore maximize events
            if (WindowState == WindowState.Maximized)
            {
                Settings.Set(Constants.IsMaximized, "true");
                return;
            }

            Settings.Set(Constants.IsMaximized, "false");
            Settings.Set(Constants.LastWindowWidth, ActualWidth.ToString());
            Settings.Set(Constants.LastWindowHeight, ActualHeight.ToString());
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Settings.Save(Constants.SettingsFile);
        }

        private void OnLocationChanged(object sender, System.EventArgs e)
        {
            Settings.Set(Constants.WindowTop, Top.ToString());
            Settings.Set(Constants.WindowLeft, Left.ToString());
        }
    }
}
using RimWorldModBrowser.Code;
using RimWorldModBrowser.Code.ViewModels;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
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
            DataContext = ViewModel.Model;

            Settings.Load(Constants.SettingsFile); 
            SetWindowOnStartup();

            // if this is the first run, load the settings dialog
            if (Settings.Lookup(Constants.FirstRunKey) is null)
                ShowSettings();

            ViewModel.LoadMods(this);
        }

        #region Private properties
        /// <summary>
        /// The view model for this window
        /// </summary>
        private MainViewModel ViewModel { get; } = new();
        #endregion

        #region Private methods
        /// <summary>
        /// Attempts to load the window location and size from the settings
        /// </summary>
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
        /// Actually bring up the Settings window in the center of the application
        /// </summary>
        private void ShowSettings()
        {
            SettingsView sv = new();
            sv.Show();

            sv.Top = Top + (ActualHeight - sv.ActualHeight) / 2;
            sv.Left = Left + (ActualWidth - sv.ActualWidth) / 2;
            sv.Hide();

            sv.ShowDialog();
        }
        #endregion

        #region Window events
        /// <summary>
        /// Callback for capturing window resize events so that the app can
        /// be restored upon next startup
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
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

        /// <summary>
        /// Callback that saves the current state of the Settings object to disk
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            Settings.Save(Constants.SettingsFile);
        }

        /// <summary>
        /// Callback for capturing window movements so that the app can start
        /// in the same place on the next startup
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnLocationChanged(object sender, EventArgs e)
        {
            Settings.Set(Constants.WindowTop, Top.ToString());
            Settings.Set(Constants.WindowLeft, Left.ToString());
        }
        #endregion

        #region Command handlers
        /// <summary>
        /// Command function for focusing the search bar via shortcut key
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnFocusSearchBar(object sender, ExecutedRoutedEventArgs e)
        {
            modList.OnFocusSearchBar(sender, e);
        }

        /// <summary>
        /// Command function for opening the Steam Workshop page via shortcut key
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnOpenSteam(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.SteamClick(sender, e);
        }

        /// <summary>
        /// Command function for opening dnSpy via shortcut key
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnOpenDnSpy(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.DnSpyClick(sender, e);
        }

        /// <summary>
        /// Command function for opening the mod folder via shortcut key
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnOpenExplorer(object sender, ExecutedRoutedEventArgs e)
        {
            selectedMod.FolderClick(sender, e);
        }

        /// <summary>
        /// Command function for opening the Settings window via shortcut key
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnOpenSettings(object sender, ExecutedRoutedEventArgs e)
        {
            SettingsClick(sender, e);
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Callback for handling when the user clicks the Settings button
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            ShowSettings();

            // reload mods based on the settings
            Task.Run(() => ViewModel.LoadMods(this));
        }
        #endregion
    }
}
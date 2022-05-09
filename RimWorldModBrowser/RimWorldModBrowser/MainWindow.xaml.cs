using RimWorldModBrowser.Code;
using RimWorldModBrowser.Code.ViewModels;
using System.Windows;
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

            Settings.Load();

            // if this is the first run, load the settings dialog
            if (Settings.Lookup(Constants.FirstRunKey) is null)
            {
                SettingsView sv = new();
                sv.ShowDialog();
            }

            ViewModel.LoadMods();
        }

        /// <summary>
        /// The view model for this window
        /// </summary>
        private readonly MainViewModel ViewModel = new();

        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            SettingsView sv = new();
            sv.ShowDialog();

            // reload mods based on the settings
            ViewModel.LoadMods();
            modList.Refresh();
        }
    }
}
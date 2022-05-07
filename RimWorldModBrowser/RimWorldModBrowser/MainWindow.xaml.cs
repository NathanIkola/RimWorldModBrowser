using RimWorldModBrowser.Code;
using RimWorldModBrowser.Code.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            ViewModel.LoadMods();

            ViewModel.Model.LoadedMods.Add(new() { Author = "test", Description = "test", Name = "test" });
        }

        /// <summary>
        /// The view model for this window
        /// </summary>
        private MainViewModel ViewModel = new();

        #region Event handlers
        /// <summary>
        /// Passthrough to the view model
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        public void HandleSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewModel.HandleSelectionChanged(sender, e);
        }
        #endregion

        /// <summary>
        /// Passthrough to the view model
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void HandleTextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel.HandleTextChanged(sender, e);
        }

        /// <summary>
        /// Passthrough to the view model
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            ViewModel.OnLoad(sender, e);
        }
    }
}
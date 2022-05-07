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

            ViewModel.Model.LoadedMods.Add(new() { Author = "test", Description = "This is a very long description that, ideally, will make the scolling go absolutely nuts here. My hopes aren't all that high, however, because the space it renders in is actually somewhat vast.\n\n\n\n\n hello", Name = "test", Path= @"C:\" });
        }

        /// <summary>
        /// The view model for this window
        /// </summary>
        private readonly MainViewModel ViewModel = new();

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
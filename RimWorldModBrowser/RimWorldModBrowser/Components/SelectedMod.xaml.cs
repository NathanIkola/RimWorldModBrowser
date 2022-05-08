using RimWorldModBrowser.Code;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace RimWorldModBrowser.Components
{
    /// <summary>
    /// Interaction logic for SelectedMod.xaml
    /// </summary>
    public partial class SelectedMod : UserControl
    {
        public SelectedMod()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Public properties
        /// <summary>
        /// The current model this control uses
        /// </summary>
        public ModConcept Model
        {
            get { return (ModConcept)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// The property that makes Model bindable
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register(nameof(Model), typeof(ModConcept), typeof(SelectedMod), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });
        #endregion

        #region Event handlers
        /// <summary>
        /// The event raised when the user attempts to open the Steam Workshop link for this mod
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void SteamClick(object sender, RoutedEventArgs e)
        {
            if (Model is null) return;
            string url = @"https://steamcommunity.com/sharedfiles/filedetails/?id=" + Model.SteamId;

            ProcessStartInfo startInfo = new()
            {
                FileName = url,
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// The event raised when the user attempts to open DnSpy for this mod's assemblies
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void DnSpyClick(object sender, RoutedEventArgs e)
        {
            if (Model is null) return;
            string dnSpyPath = Settings.Lookup(Constants.DnSpyPath);
            if (string.IsNullOrWhiteSpace(dnSpyPath)) return;

            ProcessStartInfo startInfo = new()
            {
                FileName = dnSpyPath
            };

            foreach (string dll in Model.DllPaths)
                startInfo.ArgumentList.Add(dll);

            Process.Start(startInfo);
        }

        /// <summary>
        /// The event raised when the user attempts to open the folder for this mod
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void FolderClick(object sender, RoutedEventArgs e)
        {
            if (Model is null) return;

            ProcessStartInfo startInfo = new()
            {
                FileName = Model.Path,
                UseShellExecute = true,
                Verb = "open"
            };
            Process.Start(startInfo);
        }

        /// <summary>
        /// The event raised when the user resizes the window
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            preview_img.Height = (int)LayoutRoot.RowDefinitions[2].ActualHeight;
        }
        #endregion
    }
}
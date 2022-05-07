using Ookii.Dialogs.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace RimWorldModBrowser.Components
{
    /// <summary>
    /// Interaction logic for FileBrowserBox.xaml
    /// </summary>
    public partial class FileBrowserBox : UserControl
    {
        public FileBrowserBox()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Public properties
        /// <summary>
        /// The label to put above this search box
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        /// <summary>
        /// The path that has been currently selected
        /// </summary>
        public string Path
        {
            get { return (string)GetValue(PathProperty); }
            set { SetValue(PathProperty, value); }
        }

        /// <summary>
        /// Whether or not to allow file selection
        /// </summary>
        public bool BrowseForFiles
        {
            get { return (bool)GetValue(BrowseForFilesProperty); }
            set { SetValue(BrowseForFilesProperty, value); }
        }

        /// <summary>
        /// The set of filters to use for file browsing
        /// </summary>
        public string Filters
        {
            get { return (string)GetValue(FiltersProperty); }
            set { SetValue(FiltersProperty, value); }
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// The property that makes Label bindable
        /// </summary>
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register(nameof(Label), typeof(string), typeof(FileBrowserBox), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes Path bindable
        /// </summary>
        public static readonly DependencyProperty PathProperty =
            DependencyProperty.Register(nameof(Path), typeof(string), typeof(FileBrowserBox), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes AllowFiles bindable
        /// </summary>
        public static readonly DependencyProperty BrowseForFilesProperty =
            DependencyProperty.Register(nameof(BrowseForFiles), typeof(bool), typeof(FileBrowserBox), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes Filters bindable
        /// </summary>
        public static readonly DependencyProperty FiltersProperty =
            DependencyProperty.Register(nameof(Filters), typeof(string), typeof(FileBrowserBox), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault= true,
            });

        #endregion

        /// <summary>
        /// The event fired when the Browse button is clicked
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void BrowseClick(object sender, RoutedEventArgs e)
        {
            if (BrowseForFiles)
            {
                VistaOpenFileDialog dialog = new();
                dialog.Multiselect = false;
                dialog.InitialDirectory = Path;
                dialog.CheckPathExists = true;
                dialog.Filter = Filters;

                if (dialog.ShowDialog() == true)
                    Path = dialog.FileName;
            }
            else
            {
                VistaFolderBrowserDialog dialog = new();
                dialog.Multiselect = false;
                dialog.SelectedPath = Path;

                if (dialog.ShowDialog() == true)
                    Path = dialog.SelectedPath;
            }
        }
    }
}
using RimWorldModBrowser.Code;
using RimWorldModBrowser.Code.Models;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;
using ModCollection = System.Collections.ObjectModel.ObservableCollection<RimWorldModBrowser.Code.ModConcept>;

namespace RimWorldModBrowser.Components
{
    /// <summary>
    /// Interaction logic for ModList.xaml
    /// </summary>
    public partial class ModList : UserControl, INotifyPropertyChanged
    {
        public ModList()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Public properties
        /// <summary>
        /// The list of mods to actually display in the list after filtering
        /// </summary>
        public ModCollection FilteredModList
        {
            get { return _filteredModList; }
            set 
            { 
                _filteredModList = value;
                Model.FilteredModCount = value?.Count ?? 0;
                NotifyPropertyChanged(nameof(FilteredModList)); 
            }
        }

        /// <summary>
        /// The model for this component
        /// </summary>
        public MainModel Model
        {
            get { return (MainModel)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }

        /// <summary>
        /// The ghost text to display in the text box
        /// </summary>
        public string GhostText
        {
            get { return (string)GetValue(GhostTextProperty); }
            set { SetValue(GhostTextProperty, value); }
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Focuses the text bar
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        public void OnFocusSearchBar(object sender, System.Windows.Input.ExecutedRoutedEventArgs e)
        {
            e.Handled = true;
            tb_searchBox.Focus();
        }
        #endregion

        #region Private fields
        /// <summary>
        /// The backing field for <see cref="FilteredModList"/>
        /// </summary>
        private ModCollection _filteredModList;
        #endregion

        #region Events
        /// <summary>
        /// The handler for when a property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private methods
        /// <summary>
        /// An alias for invoking the <see cref="PropertyChanged"/> handler where needed
        /// </summary>
        /// <param name="propName">The name of the property that was updated</param>
        private void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// The property that makes Model bindable
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register(nameof(Model), typeof(MainModel), typeof(ModList), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes GhostText bindable
        /// </summary>
        public static readonly DependencyProperty GhostTextProperty =
            DependencyProperty.Register(nameof(GhostText), typeof(string), typeof(ModList), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });
        #endregion

        #region Dependency property callbacks
        /// <summary>
        /// The callback fired when the Mods property gets updated
        /// </summary>
        private void OnModsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(MainModel.LoadedMods))
                SearchBox_TextChanged(null, null);
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// The event called when the user control is about to be rendered
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            Model.PropertyChanged += OnModsPropertyChanged;
            FilteredModList = new(Model.LoadedMods);
            if (string.IsNullOrEmpty(GhostText))
                GhostText = "Search...";
            if (FilteredModList.Count > 0)
                Model.SelectedMod = FilteredModList[0];
        }

        /// <summary>
        /// The function called when a user types in the search box
        /// Allows for SAYT functionality
        /// </summary>
        /// <param name="sender">The <see cref="WatermarkTextBox"/> object</param>
        /// <param name="e">The event arguments</param>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ModConcept selectedMod = Model.SelectedMod;
            FilteredModList = new(Model.LoadedMods);
            if (sender is WatermarkTextBox searchBox)
            {
                if (!string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    string searchTerm = searchBox.Text.ToLower();
                    FilteredModList = new();

                    foreach (ModConcept mod in Model.LoadedMods)
                        if (mod.Name.ToLower().Contains(searchTerm) || mod.Author.ToLower().Contains(searchTerm))
                            FilteredModList.Add(mod);
                }
            }
            Model.FilteredModCount = FilteredModList.Count;
            Model.SelectedMod = selectedMod;
        }
        #endregion
    }
}
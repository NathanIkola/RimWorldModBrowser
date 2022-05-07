using RimWorldModBrowser.Code;
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
        /// The total number of mods in the list
        /// </summary>
        public int TotalModCount => Mods.Count;

        /// <summary>
        /// The list of mods to actually display in the list after filtering
        /// </summary>
        public ModCollection FilteredModList
        {
            get { return _filteredModList; }
            set 
            { 
                _filteredModList = value;
                FilteredModCount = value?.Count ?? 0;
                NotifyPropertyChanged(nameof(FilteredModList)); 
            }
        }

        /// <summary>
        /// The number of mods filtered by the search box
        /// </summary>
        public int FilteredModCount
        {
            get { return (int)GetValue(FilteredModCountProperty); }
            set { SetValue(FilteredModCountProperty, value); }
        }

        /// <summary>
        /// The list of mods to put in the search box
        /// </summary>
        public ModCollection Mods
        {
            get { return (ModCollection)GetValue(ModsProperty); }
            set { SetValue(ModsProperty, value); }
        }

        /// <summary>
        /// The style applied to the ListBox
        /// </summary>
        public Style ListBoxStyle 
        { 
            get { return (Style)GetValue(ListBoxStyleProperty); }
            set { SetValue(ListBoxStyleProperty, value); }
        }

        /// <summary>
        /// The style applied to the search box
        /// </summary>
        public Style TextBoxStyle
        {
            get { return (Style)GetValue(TextBoxStyleProperty); }
            set { SetValue(TextBoxStyleProperty, value); }
        }

        /// <summary>
        /// The ghost text to display in the text box
        /// </summary>
        public string GhostText
        {
            get { return (string)GetValue(GhostTextProperty); }
            set { SetValue(GhostTextProperty, value); }
        }

        /// <summary>
        /// The mod that's currently selected in the list
        /// </summary>
        public ModConcept CurrentlySelectedMod
        {
            get { return (ModConcept)GetValue(CurrentlySelectedModProperty); }
            set { SetValue(CurrentlySelectedModProperty, value); }
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
        /// The property that makes Mods bindable
        /// </summary>
        public static readonly DependencyProperty ModsProperty =
            DependencyProperty.Register(nameof(Mods), typeof(ModCollection), typeof(ModList), new PropertyMetadata(OnModsPropertyChanged));

        /// <summary>
        /// The property that makes ListBoxStyle bindable
        /// </summary>
        public static readonly DependencyProperty ListBoxStyleProperty =
            DependencyProperty.Register(nameof(ListBoxStyle), typeof(Style), typeof(ModList), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes ListBoxStyle bindable
        /// </summary>
        public static readonly DependencyProperty TextBoxStyleProperty =
            DependencyProperty.Register(nameof(TextBoxStyle), typeof(Style), typeof(ModList), new FrameworkPropertyMetadata
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

        /// <summary>
        /// The property that makes FilteredModCount bindable
        /// </summary>
        public static readonly DependencyProperty FilteredModCountProperty =
            DependencyProperty.Register(nameof(FilteredModCount), typeof(int), typeof(ModList), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes CurrentlySelectedMod bindable
        /// </summary>
        public static readonly DependencyProperty CurrentlySelectedModProperty =
            DependencyProperty.Register(nameof(CurrentlySelectedMod), typeof(ModConcept), typeof(ModList), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault= true,
            });
        #endregion

        #region Dependency property callbacks
        /// <summary>
        /// The callback fired when the Mods property gets updated
        /// </summary>
        private static readonly PropertyChangedCallback OnModsPropertyChanged = delegate (DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModList modList)
                modList.FilteredModList = new((ModCollection)e.NewValue);
        };
        #endregion

        #region Event handlers
        /// <summary>
        /// The event called when the user control is about to be rendered
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnLoad(object sender, RoutedEventArgs e)
        {
            FilteredModList = new(Mods);
            if (string.IsNullOrEmpty(GhostText))
                GhostText = "Search...";
        }

        /// <summary>
        /// The function called when a user types in the search box
        /// Allows for SAYT functionality
        /// </summary>
        /// <param name="sender">The <see cref="WatermarkTextBox"/> object</param>
        /// <param name="e">The event arguments</param>
        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilteredModList = new(Mods);
            if (sender is WatermarkTextBox searchBox)
            {
                if (!string.IsNullOrWhiteSpace(searchBox.Text))
                {
                    string searchTerm = searchBox.Text.ToLower();
                    FilteredModList = new();

                    foreach (ModConcept mod in Mods)
                        if (mod.Name.ToLower().Contains(searchTerm) || mod.Author.ToLower().Contains(searchTerm))
                            FilteredModList.Add(mod);
                }
            }
            FilteredModCount = FilteredModList.Count;
            if (!FilteredModList.Contains(CurrentlySelectedMod))
            {
                CurrentlySelectedMod = FilteredModCount > 0 ? FilteredModList[0] : null;
            }
        }
        #endregion
    }
}
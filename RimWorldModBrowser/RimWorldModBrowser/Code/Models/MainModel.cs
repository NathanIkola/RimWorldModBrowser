using System.Collections.ObjectModel;
using System.ComponentModel;

namespace RimWorldModBrowser.Code.Models
{
    /// <summary>
    /// The model behind the main UI
    /// </summary>
    public sealed class MainModel : INotifyPropertyChanged
    {
        #region Public properties
        /// <summary>
        /// The list of mods loaded into the application
        /// </summary>
        public ObservableCollection<ModConcept> LoadedMods
        {
            get { return _loadedMods; }
            set { _loadedMods = value; NotifyPropertyChanged(nameof(LoadedMods)); }
        }

        /// <summary>
        /// The mod that's currently selected in the interface
        /// </summary>
        public ModConcept SelectedMod
        {
            get { return _selectedMod; }
            set { _selectedMod = value; NotifyPropertyChanged(nameof(SelectedMod)); }
        }

        /// <summary>
        /// The current string to display in the status bar
        /// </summary>
        public string StatusString
        {
            get { return _statusString; }
            set { _statusString = value; NotifyPropertyChanged(nameof(StatusString)); }
        }

        /// <summary>
        /// The number of mods that are passing the filter
        /// </summary>
        public int FilteredModCount
        {
            get { return _filteredModCount; }
            set { _filteredModCount = value; NotifyPropertyChanged(nameof(FilteredModCount)); }
        }
        #endregion

        #region Events
        /// <summary>
        /// The event raised when a property is changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private fields
        /// <summary>
        /// The list of mods loaded into the application
        /// </summary>
        private ObservableCollection<ModConcept> _loadedMods = new();

        /// <summary>
        /// The mod that's currently selected in the interface
        /// </summary>
        private ModConcept _selectedMod;

        /// <summary>
        /// The current string to display in the status bar
        /// </summary>
        private string _statusString;

        /// <summary>
        /// The number of mods that are passing the filter
        /// </summary>
        private int _filteredModCount;
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
    }
}
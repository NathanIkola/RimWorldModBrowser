using RimWorldModBrowser.Code.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace RimWorldModBrowser.Code.ViewModels
{
    /// <summary>
    /// The view model for the main window
    /// </summary>
    public class MainViewModel
    {
        #region Public properties
        /// <summary>
        /// The model being manipulated
        /// </summary>
        public MainModel Model { get; set; } = new MainModel();
        #endregion

        public MainViewModel()
        {
            Model.PropertyChanged += OnModelPropertyChanged;
        }

        #region Public methods
        /// <summary>
        /// Load all of the mods from the RimWorld install and Steam Workshop directories
        /// </summary>
        public void LoadMods()
        {
            List<ModConcept> mods = new();
            string rwInstallDir = Settings.Lookup(Constants.RWInstallDirKey);
            string workshopDir = Settings.Lookup(Constants.WorkshopDirKey);

            if (!string.IsNullOrWhiteSpace(rwInstallDir))
                mods.AddRange(ModReader.GetModsInDirectory(rwInstallDir));

            if (!string.IsNullOrWhiteSpace(workshopDir))
                mods.AddRange(ModReader.GetModsInDirectory(workshopDir));

            mods.Sort();

            Model.LoadedMods = new(mods);
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// The code run when a property in the model is updated
        /// </summary>
        /// <param name="sender">The control that spawned this event</param>
        /// <param name="e">The arguments for this event</param>
        public void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(Model.FilteredModCount))
            {
                Model.StatusString = GetStatusBarString(Model.FilteredModCount, Model.LoadedMods.Count);
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// A helper method to get the right string for the status bar
        /// </summary>
        /// <param name="filterCount">The number of mods that passed the filter</param>
        /// <param name="totalCount">The number of mods in total</param>
        /// <returns>A singular or plural string from the resources file with the numbers injected as appropriate</returns>
        private static string GetStatusBarString(int filterCount, int totalCount)
        {
            string baseStr = (totalCount != 1) 
                ? Strings.StatusBarLoadCount 
                : Strings.StatusBarLoadCountSingular;

            return baseStr
                .Replace("{0}", filterCount.ToString())
                .Replace("{1}", totalCount.ToString());
        }
        #endregion
    }
}
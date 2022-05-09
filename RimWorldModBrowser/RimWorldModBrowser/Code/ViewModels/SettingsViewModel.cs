using RimWorldModBrowser.Code.Models;
using System.Collections.Generic;
using System.IO;

namespace RimWorldModBrowser.Code.ViewModels
{
    public class SettingsViewModel
    {
        public SettingsModel Model { get; set; } = new();

        public SettingsViewModel()
        {
            LoadModel();
        }

        /// <summary>
        /// Refresh the model from the settings file
        /// </summary>
        public void LoadModel()
        {
            Model.RWInstallDir = Settings.Lookup(Constants.RWInstallDirKey);
            Model.WorkshopDir = Settings.Lookup(Constants.WorkshopDirKey);
            Model.DnSpyPath = Settings.Lookup(Constants.DnSpyPath);
            Model.FirstRun = Settings.Lookup(Constants.FirstRunKey) is null;
        }

        /// <summary>
        /// Take the model and save it to the settings file
        /// </summary>
        public void SaveModel()
        {
            Settings.Set(Constants.RWInstallDirKey, Model.RWInstallDir);
            Settings.Set(Constants.WorkshopDirKey, Model.WorkshopDir);
            Settings.Set(Constants.DnSpyPath, Model.DnSpyPath);
            Settings.Set(Constants.FirstRunKey, "false");
            Settings.Save(Constants.SettingsFile);
        }

        /// <summary>
        /// Validate the model to make sure we are allowed to close
        /// </summary>
        /// <returns>A list of any errors, an empty list if there are no errors</returns>
        public List<string> Validate()
        {
            List<string> errors = new();

            // check that all paths are, at a minimum, valid
            if (!string.IsNullOrWhiteSpace(Model.RWInstallDir) && !Directory.Exists(Model.RWInstallDir))
                errors.Add(Strings.RWModsDirBadError);

            if (!string.IsNullOrWhiteSpace(Model.WorkshopDir) && !Directory.Exists(Model.WorkshopDir))
                errors.Add(Strings.RWWorkshopBadError);

            if (!string.IsNullOrWhiteSpace(Model.DnSpyPath) && !File.Exists(Model.DnSpyPath))
                errors.Add(Strings.dnSpyPathBadError);

            // exit early if we already encountered errors
            if (errors.Count > 0) 
                return errors;

            // now check that they're actually meaningful paths
            if (!string.IsNullOrWhiteSpace(Model.RWInstallDir) && !Path.EndsInDirectorySeparator(Model.RWInstallDir))
                Model.RWInstallDir += @"\";

            if (!string.IsNullOrWhiteSpace(Model.WorkshopDir) && !Path.EndsInDirectorySeparator(Model.WorkshopDir))
                Model.WorkshopDir += @"\";

            if (!string.IsNullOrWhiteSpace(Model.RWInstallDir) && !Model.RWInstallDir.EndsWith(@"\RimWorld\Mods\"))
                errors.Add(Strings.RWModsDirIncorrectError);

            if (!string.IsNullOrWhiteSpace(Model.WorkshopDir) && !Model.WorkshopDir.EndsWith(@"workshop\content\294100\"))
                errors.Add(Strings.RWWorkshopDirIncorrectError);

            return errors;
        }
    }
}
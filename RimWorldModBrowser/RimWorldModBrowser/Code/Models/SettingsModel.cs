namespace RimWorldModBrowser.Code.Models
{
    /// <summary>
    /// The model for driving the Settings view
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// The RimWorld install directory
        /// </summary>
        public string RWInstallDir { get; set; }

        /// <summary>
        /// The RimWorld workshop directory
        /// </summary>
        public string WorkshopDir { get; set; }

        /// <summary>
        /// The path to DnSpy
        /// </summary>
        public string DnSpyPath { get; set; }

        /// <summary>
        /// Whether or not this is the first run of the program
        /// </summary>
        public bool FirstRun { get; set; }
    }
}

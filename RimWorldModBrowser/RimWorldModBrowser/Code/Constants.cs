using System;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// A class that contains all of the constants for this application
    /// </summary>
    public static class Constants
    {
        #region Settings keys
        /// <summary>
        /// The key used to store the RimWorld install directory
        /// </summary>
        public static readonly string RWInstallDirKey = "RWInstallDir";

        /// <summary>
        /// The key used to store the RimWorld Steam Workshop directory
        /// </summary>
        public static readonly string WorkshopDirKey = "WorkshopDir";

        /// <summary>
        /// The key used to store the path to DnSpy
        /// </summary>
        public static readonly string DnSpyPath = "DnSpyPath";

        /// <summary>
        /// The key used to store whether or not we're running for the first time
        /// </summary>
        public static readonly string FirstRunKey = "FirstRun";

        /// <summary>
        /// The key used to store the last width the user set the window to
        /// so it can be restored on the next launch
        /// </summary>
        public static readonly string LastWindowWidth = "LastWindowWidth";

        /// <summary>
        /// The key used to store the last height the user set the window to
        /// so it can be restored on the next launch
        /// </summary>
        public static readonly string LastWindowHeight = "LastWindowHeight";

        /// <summary>
        /// The key used to store the last maximized state so it can be
        /// restored on the next launch
        /// </summary>
        public static readonly string IsMaximized = "IsMaximized";

        /// <summary>
        /// The key used to store the last window top value so it can be
        /// restored on the next launch
        /// </summary>
        public static readonly string WindowTop = "WindowTop";

        /// <summary>
        /// The key used to store the last window left value so it can be
        /// restored on the next launch
        /// </summary>
        public static readonly string WindowLeft = "WindowLeft";

        /// <summary>
        /// The path to the settings file
        /// </summary>
        public static string SettingsFile => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\RWModBrowser\settings.xml";
        #endregion
    }
}
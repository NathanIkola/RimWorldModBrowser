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
        #endregion
    }
}
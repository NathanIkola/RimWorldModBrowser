using System.Windows.Input;

namespace RimWorldModBrowser.Code
{
    /// <summary>
    /// Collection of commands used for keyboard shortcuts
    /// </summary>
    public static class CustomCommands
    {
        /// <summary>
        /// Command for focusing the search bar
        /// </summary>
        public static readonly RoutedUICommand FocusSearchBar = new(
            "FocusSearchBar",
            "FocusSearchBar",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.F, ModifierKeys.Control) });

        /// <summary>
        /// Command for opening the Steam Workshop for the currently selected mod
        /// </summary>
        public static readonly RoutedUICommand OpenSteam = new(
            "OpenSteam",
            "OpenSteam",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.S, ModifierKeys.Control) });

        /// <summary>
        /// Command for opening dnSpy for the currently selected mod's assemblies
        /// </summary>
        public static readonly RoutedUICommand OpenDnSpy = new(
            "OpenDnSpy",
            "OpenDnSpy",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.D, ModifierKeys.Control) });
        
        /// <summary>
        /// Command for opening the currently selected mod's folder on disk
        /// </summary>
        public static readonly RoutedUICommand OpenExplorer = new(
            "OpenExplorer",
            "OpenExplorer",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.E, ModifierKeys.Control) });

        /// <summary>
        /// Command for opening the Settings window
        /// </summary>
        public static readonly RoutedUICommand OpenSettings = new(
            "OpenSettings",
            "OpenSettings",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.G, ModifierKeys.Control)});
    }
}
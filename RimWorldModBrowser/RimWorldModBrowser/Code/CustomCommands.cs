using System.Windows.Input;

namespace RimWorldModBrowser.Code
{
    public static class CustomCommands
    {
        public static readonly RoutedUICommand FocusSearchBar = new(
            "FocusSearchBar",
            "FocusSearchBar",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.F, ModifierKeys.Control) });

        public static readonly RoutedUICommand OpenSteam = new(
            "OpenSteam",
            "OpenSteam",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.S, ModifierKeys.Control) });

        public static readonly RoutedUICommand OpenDnSpy = new(
            "OpenDnSpy",
            "OpenDnSpy",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.D, ModifierKeys.Control) });

        public static readonly RoutedUICommand OpenExplorer = new(
            "OpenExplorer",
            "OpenExplorer",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.E, ModifierKeys.Control) });

        public static readonly RoutedUICommand OpenSettings = new(
            "OpenSettings",
            "OpenSettings",
            typeof(CustomCommands),
            new() { new KeyGesture(Key.G, ModifierKeys.Control)});
    }
}
using RimWorldModBrowser.Code.ViewModels;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace RimWorldModBrowser.Views
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public SettingsViewModel Model = new();
        public bool DoneButtonClicked { get; set; } = false;

        public Settings()
        {
            InitializeComponent();
            LayoutRoot.DataContext = Model;
        }

        /// <summary>
        /// The event called when the user hits the Done button
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void DoneClick(object sender, RoutedEventArgs e)
        {
            DoneButtonClicked = true;
            Close();
        }

        /// <summary>
        /// The event called when the user hits the Cancel button
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void CancelClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// The event called when the window is closing
        /// </summary>
        /// <param name="sender">The control that raised the event</param>
        /// <param name="e">The arguments for this event</param>
        private void OnClosing(object sender, CancelEventArgs e)
        {
            if (!DoneButtonClicked)
                return;

            List<string> errors = Model.Validate();

            if (errors.Count > 0)
            {
                foreach (string error in errors)
                    MessageBox.Show(error);
                e.Cancel = true;
            }
            else Model.SaveModel();

            // default state is cancel
            DoneButtonClicked = false;
        }
    }
}
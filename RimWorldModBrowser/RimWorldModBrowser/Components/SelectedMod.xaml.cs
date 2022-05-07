using RimWorldModBrowser.Code;
using System.Windows;
using System.Windows.Controls;

namespace RimWorldModBrowser.Components
{
    /// <summary>
    /// Interaction logic for SelectedMod.xaml
    /// </summary>
    public partial class SelectedMod : UserControl
    {
        public SelectedMod()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Public properties
        /// <summary>
        /// The current model this control uses
        /// </summary>
        public ModConcept Model
        {
            get { return (ModConcept)GetValue(ModelProperty); }
            set { SetValue(ModelProperty, value); }
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// The property that makes Model bindable
        /// </summary>
        public static readonly DependencyProperty ModelProperty =
            DependencyProperty.Register(nameof(Model), typeof(ModConcept), typeof(SelectedMod), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });
        #endregion
    }
}
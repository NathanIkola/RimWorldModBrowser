using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RimWorldModBrowser.Components
{
    /// <summary>
    /// Interaction logic for ZeroState.xaml
    /// </summary>
    public partial class ZeroState : UserControl
    {
        public ZeroState()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        #region Public properties
        /// <summary>
        /// The path to the image to show the user
        /// </summary>
        public string ImagePath
        {
            get { return (string)GetValue(ImagePathProperty); }
            set { SetValue(ImagePathProperty, value); }
        }

        /// <summary>
        /// The caption to display
        /// </summary>
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }
        #endregion

        #region Dependency properties
        /// <summary>
        /// The property that makes ImagePath bindable
        /// </summary>
        public static readonly DependencyProperty ImagePathProperty =
            DependencyProperty.Register(nameof(ImagePath), typeof(string), typeof(ZeroState), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });

        /// <summary>
        /// The property that makes Caption bindable
        /// </summary>
        public static readonly DependencyProperty CaptionProperty =
            DependencyProperty.Register(nameof(Caption), typeof(string), typeof(ZeroState), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
            });
        #endregion
    }
}
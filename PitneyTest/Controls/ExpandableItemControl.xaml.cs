using System;
using System.Collections;
using System.Windows.Controls;

namespace PitneyTest.Controls
{
    /// <summary>
    /// Interaction logic for ExpandableItem.xaml
    /// </summary>
    public partial class ExpandableItemControl : UserControl
    {
        public ExpandableItemControl()
        {
            InitializeComponent();
        }

        public Object Header
        {
            get { return ExpanderMain.Header; }
            set { ExpanderMain.Header = value; }
        }

        public IEnumerable DataSource
        {
            get { return LsvTodayItems.ItemsSource; }
            set { LsvTodayItems.ItemsSource = value;  }
        }
    }
}

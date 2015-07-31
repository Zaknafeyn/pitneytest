using System;
using System.Collections;
using System.Windows.Controls;
using PitneyTest.DataAccess.API;
using PitneyTest.DataAccess.DataObjects;
using PitneyTest.EventArguments;

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
            set { LsvTodayItems.ItemsSource = value; }
        }

        public Content SelectedItem
        {
            get { return LsvTodayItems.SelectedItem as Content; }
        }

        public bool IsExpanded
        {
            get { return ExpanderMain.IsExpanded; }
            set { ExpanderMain.IsExpanded = value; }
        }

        public event EventHandler<SortItemsEventArgs> SortItems;
        public event EventHandler<SelectedTransactionChangedEventArgs> SelectedTransactionChanged;

        public void LsvTodayItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectedTransactionChanged(SelectedItem);
        }

        public void SelectFirstLoadedItem()
        {
            IsExpanded = true;
            LsvTodayItems.SelectedIndex = 0;
        }

        protected virtual void OnSortItems(string sortField, SortOrder sortOrder)
        {
            if (SortItems != null)
                SortItems(this, new SortItemsEventArgs(sortField, sortOrder));
        }

        protected virtual void OnSelectedTransactionChanged(Content currentContent)
        {
            if (SelectedTransactionChanged != null)
                SelectedTransactionChanged(this, new SelectedTransactionChangedEventArgs(currentContent));
        }
    }
}
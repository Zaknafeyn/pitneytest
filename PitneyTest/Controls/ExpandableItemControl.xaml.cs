﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using PitneyTest.DataObjects;
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

        public void LsvTodayItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            OnSelectedTransactionChanged(SelectedItem);
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

        public Content SelectedItem
        {
            get { return LsvTodayItems.SelectedItem as Content; }
        }

        public bool IsExpanded
        {
            get { return ExpanderMain.IsExpanded; }
            set { ExpanderMain.IsExpanded = value; }
        }

        public void SelectFirstLoadedItem()
        {
            IsExpanded = true;
            LsvTodayItems.SelectedIndex = 0;
        }

        public event EventHandler<SelectedTransactionChangedEventArgs> SelectedTransactionChanged;

        protected virtual void OnSelectedTransactionChanged(Content currentContent)
        {
            if (SelectedTransactionChanged!= null)
                SelectedTransactionChanged(this, new SelectedTransactionChangedEventArgs(currentContent));
        }
    }
}
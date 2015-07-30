using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using PitneyTest.Controls;
using PitneyTest.DataAccess.API;
using PitneyTest.DataAccess.DataObjects;
using PitneyTest.DataAccess.Token;
using PitneyTest.EventArguments;

namespace PitneyTest.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly List<ExpandableItemControl> _expandableItemControls;

        public MainWindow(AccessToken token, DataRetrieval dataRetrieval)
        {
            DataRetrieval = dataRetrieval;
            Token = token;

            InitializeComponent();

            _expandableItemControls = new List<ExpandableItemControl>
            {
                EicToday,
                EicYesterday,
                EicLastWeek,
                EicOlder
            };

            LoadDataAsync();

            Show();
        }

        public AccessToken Token { get; private set; }
        public DataRetrieval DataRetrieval { get; private set; }
        public Transactions TodaysTransactions { get; private set; }
        public Transactions YesterdaysTransactions { get; private set; }
        public Transactions LastWeekTransactions { get; private set; }
        public Transactions OlderTransactions { get; private set; }
        public Content CurrentContent { get; set; }

        #region Event handlers

        private void ExpandableItemControl_OnSelectedTransactionChanged(object sender, SelectedTransactionChangedEventArgs e)
        {
            CurrentContent = e.CurrentContent;
            DataContext = CurrentContent;
        }

        private void ButtonRefresh_OnClick(object sender, RoutedEventArgs e)
        {
            LoadDataAsync();
        }

        #endregion

        private async void LoadDataAsync()
        {
            DataLodingStart();

            var utcNow = DateTime.UtcNow;
            var todayStartDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, 0);
            var todayEndDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 23, 59, 59, 999);

            var apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate,
                CustomSettings = new Dictionary<string, string>
                {
                    {"sort", "shipmentDate,asc"}
                },
                EndDate = todayEndDate,
                PageSize = 100
            });

            var todaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-1),
                EndDate = todayEndDate.AddDays(-1),
                CustomSettings = new Dictionary<string, string>
                {
                    {"sort", "shipmentDate,asc"}
                },
                PageSize = 100
            });

            var yesterdaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-8),
                EndDate = todayEndDate.AddDays(-2),
                CustomSettings = new Dictionary<string, string>
                {
                    {"sort", "shipmentDate,asc"}
                },
                PageSize = 100
            });

            var lastWeekTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                EndDate = todayEndDate.AddDays(-2),
                CustomSettings = new Dictionary<string, string>
                {
                    {"sort", "shipmentDate,asc"}
                },
                PageSize = 100
            });

            var olderTransactionUri = apiUriBuilder.GetTransactionsUri();

            TodaysTransactions = await DataRetrieval.GetTransactionsAsync(todaysTransactionUri, Token);
            YesterdaysTransactions = await DataRetrieval.GetTransactionsAsync(yesterdaysTransactionUri, Token);
            LastWeekTransactions = await DataRetrieval.GetTransactionsAsync(lastWeekTransactionUri, Token);
            OlderTransactions = await DataRetrieval.GetTransactionsAsync(olderTransactionUri, Token);

            BindData();

            DataLodingEnd();
        }

        private void DataLodingStart()
        {
            MainGrid.Visibility = Visibility.Hidden;

            MainDockPanel.Children.Remove(MainGrid);
            var label = new Label
            {
                Content = "Loading ...",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 30,
                FontWeight = FontWeight.FromOpenTypeWeight(30)
            };

            MainDockPanel.Children.Add(label);
        }

        private void DataLodingEnd()
        {
            foreach (var expandableItemControl in _expandableItemControls)
            {
                expandableItemControl.IsExpanded = false;
            }

            _expandableItemControls[0].SelectFirstLoadedItem();

            MainDockPanel.Children.Clear();
            MainDockPanel.Children.Add(MainGrid);
            MainGrid.Visibility = Visibility.Visible;
        }

        private void BindData()
        {
            EicToday.DataSource = TodaysTransactions.Content;
            EicYesterday.DataSource = YesterdaysTransactions.Content;
            EicLastWeek.DataSource = LastWeekTransactions.Content;
            EicOlder.DataSource = OlderTransactions.Content;
        }
    }
}
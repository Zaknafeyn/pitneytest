using System;
using System.Net.Mime;
using System.Windows;
using PitneyTest.API;
using PitneyTest.Controls;
using PitneyTest.DataObjects;
using PitneyTest.EventArguments;
using PitneyTest.Token;

namespace PitneyTest.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(AccessToken token, DataRetrieval dataRetrieval)
        {
            DataRetrieval = dataRetrieval;
            Token = token;

            InitializeComponent();

            LoadDataAsync();

//            BindData();
        }

        public AccessToken Token { get; private set; }
        public DataRetrieval DataRetrieval { get; private set; }
        public Transactions TodaysTransactions { get; private set; }
        public Transactions YesterdaysTransactions { get; private set; }
        public Transactions LastWeekTransactions { get; private set; }
        public Transactions OlderTransactions { get; private set; }

        private async void LoadDataAsync()
        {
            var utcNow = DateTime.UtcNow;
            var todayStartDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, 0);
            var todayEndDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 23, 59, 59, 999);

            var apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate,
                EndDate = todayEndDate,
                PageSize = 100
            });

            var todaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-1),
                EndDate = todayEndDate.AddDays(-1),
                PageSize = 100
            });

            var yesterdaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-8),
                EndDate = todayEndDate.AddDays(-2),
                PageSize = 100
            });

            var lastWeekTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                EndDate = todayEndDate.AddDays(-2),
                PageSize = 100
            });

            var olderTransactionUri = apiUriBuilder.GetTransactionsUri();


            TodaysTransactions = await DataRetrieval.GetTransactionsAsync(todaysTransactionUri, Token);
            YesterdaysTransactions = await DataRetrieval.GetTransactionsAsync(yesterdaysTransactionUri, Token);
            LastWeekTransactions = await DataRetrieval.GetTransactionsAsync(lastWeekTransactionUri, Token);
            OlderTransactions = await DataRetrieval.GetTransactionsAsync(olderTransactionUri, Token);

            BindData();
        }

        private void BindData()
        {
//            StackPanelTransactions.Children.Add()
            EicToday.DataSource = TodaysTransactions.Content;
            EicYesterday.DataSource = YesterdaysTransactions.Content;
        }

        private void ExpandableItemControl_OnSelectedTransactionChanged(object sender, SelectedTransactionChangedEventArgs e)
        {
            CurrentContent = e.CurrentContent;
        }

        public Content CurrentContent { get; set; }
    }
}

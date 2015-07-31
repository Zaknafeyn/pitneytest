using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using PitneyTest.DataAccess.API;
using PitneyTest.Tablet.Model;
using PitneyTest.Tablet.Services;
using PitneyTest.Tablet.View;

namespace PitneyTest.Tablet.ViewModel
{
    internal class MainViewModel : BindableBase
    {
        private readonly AuthContext _authContext;
        private readonly DataRetrieval _dataRetrieval;
        private ObservableCollection<ContentModel> _items;
        private ContentModel _selectedItem;

        public MainViewModel(DataRetrieval dataRetrieval, AuthContext authContext)
        {
            _dataRetrieval = dataRetrieval;
            _authContext = authContext;

            Items = new ObservableCollection<ContentModel>();
            LoginCommand = new DelegateCommand(() => NavigationService.Navigate(typeof (LoginView)));
            RefreshCommand = new DelegateCommand(LoadDataAsync);

            LoadDataAsync();
        }

        public ObservableCollection<ContentModel> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }

        public ContentModel SelectedItem
        {
            get { return _selectedItem; }
            set { SetProperty(ref _selectedItem, value); }
        }

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        private async void LoadDataAsync()
        {
            Items.Clear();

            var utcNow = DateTime.UtcNow;
            var todayStartDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 0, 0, 0, 0);
            var todayEndDate = new DateTime(utcNow.Year, utcNow.Month, utcNow.Day, 23, 59, 59, 999);

            var apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate,
                EndDate = todayEndDate,
                CustomSettings = new Dictionary<string, string>
                {
                    {"sort", "shipmentDate,asc"}
                },
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

            var todaysTransactions = await _dataRetrieval.GetTransactionsAsync(todaysTransactionUri, _authContext.AccessToken);
            foreach (var transaction in todaysTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, "1"));
            }

            SelectedItem = Items.FirstOrDefault();

            var yesterdaysTransactions = await _dataRetrieval.GetTransactionsAsync(yesterdaysTransactionUri, _authContext.AccessToken);
            foreach (var transaction in yesterdaysTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, "2"));
            }

            var lastWeekTransactions = await _dataRetrieval.GetTransactionsAsync(lastWeekTransactionUri, _authContext.AccessToken);
            foreach (var transaction in lastWeekTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, "3"));
            }

            var olderTransactions = await _dataRetrieval.GetTransactionsAsync(olderTransactionUri, _authContext.AccessToken);
            foreach (var transaction in olderTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, "4"));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }
    }
}
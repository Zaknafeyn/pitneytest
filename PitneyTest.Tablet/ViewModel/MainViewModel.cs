using System;
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
        private bool _isBusy;
        private ObservableCollection<ContentModel> _items;
        private ContentModel _selectedItem;

        public MainViewModel(DataRetrieval dataRetrieval, AuthContext authContext)
        {
            _dataRetrieval = dataRetrieval;
            _authContext = authContext;

            Items = new ObservableCollection<ContentModel>();
            LoginCommand = new DelegateCommand(() => NavigationService.Navigate(typeof(LoginView)));
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

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand RefreshCommand { get; set; }

        private async void LoadDataAsync()
        {
            IsBusy = true;

            Items.Clear();
            SelectedItem = null;

            var todayStartDate = DateTime.Now.Date.ToUniversalTime();
            var todayEndDate = DateTime.Now.Date.AddDays(1).AddSeconds(-1).ToUniversalTime();

            var apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate,
                EndDate = todayEndDate,
                SortOrder = SortOrder.Desc,
                SortField = SortField.ShipmentDate,
                PageSize = 100
            });

            var todaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-1),
                EndDate = todayEndDate.AddDays(-1),
                SortOrder = SortOrder.Desc,
                SortField = SortField.ShipmentDate,
                PageSize = 100
            });

            var yesterdaysTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = todayStartDate.AddDays(-7),
                EndDate = todayEndDate.AddDays(-2),
                SortOrder = SortOrder.Desc,
                SortField = SortField.ShipmentDate,
                PageSize = 100
            });

            var lastWeekTransactionUri = apiUriBuilder.GetTransactionsUri();

            apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = DateTime.MinValue,
                EndDate = todayEndDate.AddDays(-8),
                SortOrder = SortOrder.Desc,
                SortField = SortField.ShipmentDate,
                PageSize = 100
            });

            var olderTransactionUri = apiUriBuilder.GetTransactionsUri();

            var todaysTransactions = await _dataRetrieval.GetTransactionsAsync(todaysTransactionUri, _authContext.AccessToken);
            foreach (var transaction in todaysTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, GroupDescriptor.Today));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            var yesterdaysTransactions = await _dataRetrieval.GetTransactionsAsync(yesterdaysTransactionUri, _authContext.AccessToken);
            foreach (var transaction in yesterdaysTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, GroupDescriptor.Yesterday));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            var lastWeekTransactions = await _dataRetrieval.GetTransactionsAsync(lastWeekTransactionUri, _authContext.AccessToken);
            foreach (var transaction in lastWeekTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, GroupDescriptor.LastWeek));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            var olderTransactions = await _dataRetrieval.GetTransactionsAsync(olderTransactionUri, _authContext.AccessToken);
            foreach (var transaction in olderTransactions.Content)
            {
                Items.Add(new ContentModel(transaction, GroupDescriptor.Older));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }

            IsBusy = false;
        }
    }
}
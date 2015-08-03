using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

            await LoadRangeAsync(GroupDescriptor.Today,
                todayStartDate,
                todayEndDate);

            await LoadRangeAsync(GroupDescriptor.Yesterday,
                todayStartDate.AddDays(-1),
                todayEndDate.AddDays(-1));

            await LoadRangeAsync(GroupDescriptor.LastWeek,
                todayStartDate.AddDays(-7),
                todayEndDate.AddDays(-2));

            await LoadRangeAsync(GroupDescriptor.Older,
                DateTime.MinValue,
                todayEndDate.AddDays(-8));

            IsBusy = false;
        }

        private Uri GetTransactionUri(DateTime starDate, DateTime endDate)
        {
            var apiUriBuilder = new ApiDataUriBuilder(new ApiBuilderConfiguration
            {
                StartDate = starDate,
                EndDate = endDate,
                SortOrder = SortOrder.Desc,
                SortField = SortField.ShipmentDate,
                PageSize = 100
            });

            var transactionUri = apiUriBuilder.GetTransactionsUri();

            return transactionUri;
        }

        private async Task LoadRangeAsync(GroupDescriptor groupDescriptor, DateTime starDate, DateTime endDate)
        {
            var transactionUri = GetTransactionUri(starDate, endDate);

            var transactions = await _dataRetrieval.GetTransactionsAsync(transactionUri, _authContext.AccessToken);
            foreach (var transaction in transactions.Content)
            {
                Items.Add(new ContentModel(transaction, groupDescriptor));
            }

            if (SelectedItem == null)
            {
                SelectedItem = Items.FirstOrDefault();
            }
        }
    }
}
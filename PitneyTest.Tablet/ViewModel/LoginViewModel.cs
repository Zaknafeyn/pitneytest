using System;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Mvvm;
using PitneyTest.DataAccess;
using PitneyTest.DataAccess.API;
using PitneyTest.Tablet.Services;
using PitneyTest.Tablet.View;

namespace PitneyTest.Tablet.ViewModel
{
    internal class LoginViewModel : BindableBase
    {
        private readonly AuthContext _authContext;
        private readonly DataRetrieval _dataRetrieval;
        private bool _isBusy;
        private string _userId;

        public LoginViewModel(DataRetrieval dataRetrieval, AuthContext authContext)
        {
            _dataRetrieval = dataRetrieval;
            _authContext = authContext;
            LoginCommand = new DelegateCommand(LoginExecuted, LoginCanExecute);
            UserId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
        }

        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public DelegateCommand LoginCommand { get; set; }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserId);
        }

        private async void LoginExecuted()
        {
            var url = new Uri(string.Format(Configuration.LoginUrl, Configuration.LoginServer));
            var isTokenObtained = true;

            try
            {
                IsBusy = true;
                await Task.Delay(1);

                _authContext.AccessToken = await _dataRetrieval.GetTokenAsync(url, UserId);

                IsBusy = false;

                NavigationService.Navigate(typeof (MainView));
            }
            catch
            {
                IsBusy = false;
                isTokenObtained = false;
                _authContext.AccessToken = null;
            }

            if (!isTokenObtained)
            {
                await new MessageDialog("Invalid user identification. Try again.", "Login error").ShowAsync();
            }
        }
    }
}
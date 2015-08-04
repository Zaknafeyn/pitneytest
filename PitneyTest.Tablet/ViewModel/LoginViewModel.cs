using System;
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
        private string _password;
        private string _userName;

        public LoginViewModel(DataRetrieval dataRetrieval, AuthContext authContext)
        {
            _dataRetrieval = dataRetrieval;
            _authContext = authContext;
            LoginCommand = new DelegateCommand(LoginExecuted, LoginCanExecute);
            UserName = "craig.j1@horizon.com";
            Password = "Testing123";
        }

        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public DelegateCommand LoginCommand { get; set; }

        private bool LoginCanExecute()
        {
            return !string.IsNullOrWhiteSpace(UserName);
        }

        private async void LoginExecuted()
        {
            IsBusy = true;

            var url = new Uri(string.Format(Configuration.LoginUrl, Configuration.LoginServer));
            _authContext.AccessToken = await _dataRetrieval.GetTokenAsync(url, UserName, Password);

            IsBusy = false;

            if (_authContext.AccessToken != null)
            {
                NavigationService.Navigate(typeof(MainView));
            }
            else
            {
                await new MessageDialog("Invalid user identification. Try again.", "Login error").ShowAsync();
            }
        }
    }
}
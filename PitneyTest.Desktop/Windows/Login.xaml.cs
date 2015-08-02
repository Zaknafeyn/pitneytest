using System;
using System.Windows;
using System.Windows.Input;
using PitneyTest.DataAccess;
using PitneyTest.DataAccess.API;

namespace PitneyTest.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();

            UserId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
        }

        public string UserId { get; set; }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginUrl = Configuration.LoginUrl;
            var loginServer = Configuration.LoginServer;

            var url = new Uri(string.Format(loginUrl, loginServer));

            var dataRetrieval = new DataRetrieval();

            Cursor = Cursors.Wait;
            var token = await dataRetrieval.GetTokenAsync(url, UserId);
            Cursor = Cursors.Arrow;

            if (token == null)
            {
                MessageBox.Show("Invalid username. Try again.", "Login error", MessageBoxButton.OK);
            }
            else
            {
                var mainWindow = new MainWindow(token, dataRetrieval);
                //mainWindow.Show();
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
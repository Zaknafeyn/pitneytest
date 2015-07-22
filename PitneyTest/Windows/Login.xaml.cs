using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using PitneyTest.API;

namespace PitneyTest
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var userId = this.TxtUserIdTxt.Text.Trim();
            var loginUrl = ConfigurationManager.AppSettings[Constants.LoginUrl];
            var loginServer = ConfigurationManager.AppSettings[Constants.LoginServer];

            var url = new Uri(string.Format(loginUrl, loginServer));

            var dataRetrieval = new DataRetrieval();
            Cursor = Cursors.Wait;
            var token = await dataRetrieval.GetTokenAsync(url, userId);
            Cursor = Cursors.Arrow;
            TxbToken.Text = token.AuthToken;

            var mainWindow = new MainWindow(token, dataRetrieval);
            mainWindow.Show();
            
            Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}

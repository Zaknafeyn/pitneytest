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
using PitneyTest.Token;
using PitneyTest.Windows;

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

            UserId = "950f9e6a-2ce9-44f2-9f25-31e5caad60a0@pb";
        }

        public string UserId { get; set; }
    
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var loginUrl = ConfigurationManager.AppSettings[Constants.LoginUrl];
            var loginServer = ConfigurationManager.AppSettings[Constants.LoginServer];

            var url = new Uri(string.Format(loginUrl, loginServer));

            var dataRetrieval = new DataRetrieval();
            Cursor = Cursors.Wait;
            AccessToken token = null;

            try
            {
                token = await dataRetrieval.GetTokenAsync(url, UserId);
            }
            catch
            {
                MessageBox.Show("Invalid username. Try again.", "Login error", MessageBoxButton.OK);
                return;
            }
            finally
            {
                Cursor = Cursors.Arrow;
            }

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

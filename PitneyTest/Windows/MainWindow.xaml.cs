using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PitneyTest.API;
using PitneyTest.Token;

namespace PitneyTest
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
        }

        public AccessToken Token { get; private set; }
        public DataRetrieval DataRetrieval { get; private set; }
    }
}

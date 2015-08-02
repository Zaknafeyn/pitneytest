using Microsoft.Practices.Prism.Mvvm;
using PitneyTest.Tablet.ViewModel;

namespace PitneyTest.Tablet.View
{
    public sealed partial class MainView : IView
    {
        public MainView()
        {
            InitializeComponent();
            //Loaded += MainView_Loaded;
        }

        //private async void MainView_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        //{
        //    await ((MainViewModel) DataContext).RefreshCommand.Execute();
        //}
    }
}
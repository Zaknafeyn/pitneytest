using Microsoft.Practices.Prism.Mvvm;

namespace PitneyTest.Tablet.View
{
    public sealed partial class LoginView : IView
    {
        public LoginView()
        {
            InitializeComponent();
            Loaded += (sender, args) => login.SelectAll();
        }
    }
}
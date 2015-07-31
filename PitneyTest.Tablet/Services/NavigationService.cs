using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace PitneyTest.Tablet.Services
{
    internal class NavigationService
    {
        public static void Navigate(Type viewType)
        {
            var rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();
                Window.Current.Content = rootFrame;
            }

            rootFrame.Navigate(viewType);
        }
    }
}
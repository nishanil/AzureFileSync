using System;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(GoMonkeys.iOSDisplay))]
namespace GoMonkeys
{
    public class iOSDisplay : IDisplay
    {
        public Size Size
        {
            get
            {
                return new Size(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);
            }
        }
    }
}

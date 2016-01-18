using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace GoMonkeys
{
    public class App : Application
    {
        public static readonly string ApplicationURL = @"http://goselfies.azurewebsites.net/";
        public static readonly string GatewayURL = @"";
        public static readonly string ApplicationKey = @"";
        static readonly MonkeyDataManager monkeyDataManager = new MonkeyDataManager();
        public static Size ScreenSize { get; set; }

        //TODO: Do not hard code. Fetch from User Auth
        public static string UserName { get; set; } = "Nish";

        public static MonkeyDataManager MonkeyDataManager { get { return monkeyDataManager; } }
        public App()
        {
            

            // The root page of your application
            MainPage = new NavigationPage( new GoMonkeys.Views.MonkeysPage());

            var screen = DependencyService.Get<IDisplay>();
            if (screen != null)
            {
                ScreenSize = screen.Size;
            }
            else {
                ScreenSize = new Size(300, 600);
            }

        }


        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

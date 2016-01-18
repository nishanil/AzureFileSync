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
       // public readonly TodoItemManager todoItemManager = new TodoItemManager();
        static readonly MonkeyDataManager monkeyDataManager = new MonkeyDataManager();
        public static Size ScreenSize { get; set; }

        //TODO: Do not hard code. Fetch from User Auth
        public static string UserName { get; set; } = "Nish";

        public static MonkeyDataManager MonkeyDataManager { get { return monkeyDataManager; } }
        public App()
        {

            //var selfieButton = new Button {
            //    Text = "Take Photo",
            //    VerticalOptions = LayoutOptions.CenterAndExpand,
            //    HorizontalOptions = LayoutOptions.CenterAndExpand
            //};

            //selfieButton.Clicked += SelfieButton_Clicked;

            //var tabbedPage = new TabbedPage();
            //tabbedPage.Children.Add(new ContentPage
            //{
            //    Title = "Home",
            //    Content = new StackLayout
            //    {
            //        VerticalOptions = LayoutOptions.Center,
            //        Children = {
            //            new Label {
            //                XAlign = TextAlignment.Center,
            //                Text = "Welcome to Xamarin Forms!"
            //            },
            //            selfieButton
            //        }
            //    }
            //});

            //tabbedPage.Children.Add(new GoMonkeys.Pages.SelfieListPage());

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

        //private async void SelfieButton_Clicked(object sender, EventArgs e)
        //{
        //    var test = await CrossMedia.Current.PickPhotoAsync();
        //    if (test != null)
        //    {
        //        var todoItem = new TodoItem { Name = "Nish Sample" + new Random().Next() };
        //        await todoItemManager.SaveTaskAsync(todoItem);
        //        await todoItemManager.AddImage(todoItem, test.Path);
        //        await todoItemManager.SyncAsync();

        //    }
        //}

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

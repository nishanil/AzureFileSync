using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.Media;
using Plugin.Media.Abstractions;

using Xamarin.Forms;

namespace GoSelfies
{
    public class App : Application
    {
        public static readonly string ApplicationURL = @"http://goselfies.azurewebsites.net/";
        public static readonly string GatewayURL = @"";
        public static readonly string ApplicationKey = @"";
        public readonly TodoItemManager todoItemManager = new TodoItemManager();
        public App()
        {

            var selfieButton = new Button {
                Text = "Take Photo",
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            selfieButton.Clicked += SelfieButton_Clicked;

            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(new ContentPage
            {
                Title = "Home",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            XAlign = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                        selfieButton
                    }
                }
            });

            tabbedPage.Children.Add(new GoSelfies.Pages.SelfieListPage());

            // The root page of your application
            MainPage = tabbedPage;
                
        }

        private async void SelfieButton_Clicked(object sender, EventArgs e)
        {
            var test = await CrossMedia.Current.PickPhotoAsync();
            if (test != null)
            {
                var todoItem = new TodoItem { Name = "Nish Sample" + new Random().Next() };
                await todoItemManager.SaveTaskAsync(todoItem);
                await todoItemManager.AddImage(todoItem, test.Path);
                await todoItemManager.SyncAsync();

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

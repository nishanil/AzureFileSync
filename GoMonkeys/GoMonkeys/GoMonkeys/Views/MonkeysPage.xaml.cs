using GoMonkeys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GoMonkeys.Views
{
    public partial class MonkeysPage : ContentPage
    {
        MonkeysPageViewModel vm;
        public MonkeysPage()
        {
            InitializeComponent();
            BindingContext = vm = new MonkeysPageViewModel();
        }

        protected async override void OnAppearing()
        {
            await vm.LoadMonkeys();
            await App.MonkeyDataManager.SyncAsync();
            base.OnAppearing();
        }
        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddMonkeyPage());
        }
    }
}

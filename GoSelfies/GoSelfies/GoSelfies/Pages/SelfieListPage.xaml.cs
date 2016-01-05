using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GoSelfies.Pages
{
    public partial class SelfieListPage : ContentPage
    {
        public SelfieListPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext == null)
            {
                var vm = new ViewModels.SelfieListViewModel();
                await vm.PopulateTodoList();
                BindingContext = vm;
            }
        }
    }
}

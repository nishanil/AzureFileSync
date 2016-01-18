using GoMonkeys.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GoMonkeys.Views
{
    public partial class AddMonkeyPage : ContentPage
    {
        AddMonkeyPageViewModel vm;
        public AddMonkeyPage()
        {
            InitializeComponent();
            BindingContext = vm = new AddMonkeyPageViewModel(Navigation);
            PickPhoto.GestureRecognizers.Add(new TapGestureRecognizer() {
                Command = vm.PickPhotoCommand
            });
        }
    }
}

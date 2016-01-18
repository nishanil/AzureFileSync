using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GoMonkeys.ViewModels
{
    public class AddMonkeyPageViewModel : BaseViewModel
    {
        static readonly ImageSource DefaultPhoto = ImageSource.FromFile("defaultplaceholder.png");

        private ImageSource photo;
        public ImageSource Photo
        {
            get { return photo; }
            set { photo = value; RaisePropertyChanged(); }
        }

        public ICommand PickPhotoCommand { get; set; }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; RaisePropertyChanged(); }
        }


        public ICommand UploadPhotoCommand { get; set; }

        MediaFile image;
        public AddMonkeyPageViewModel(INavigation navigation)
        {
            Photo = DefaultPhoto;
            var dataManager = App.MonkeyDataManager;

            PickPhotoCommand = new Command(async () => {
                image = await Plugin.Media.CrossMedia.Current.PickPhotoAsync();
                if(image!=null)
                Photo = ImageSource.FromFile(image.Path);
            });

            UploadPhotoCommand = new Command(async ()=> {

                if (!string.IsNullOrEmpty(Status) && image != null)
                {
                    var monkey = new Models.Monkey { Status = this.Status, UserName= App.UserName };
                    await dataManager.SaveMonkeyAsync(monkey);
                    await dataManager.AddImage(monkey, image.Path);
                    if (navigation != null)
                       await navigation.PopAsync();
                }
                
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoMonkeys.ViewModels
{
    public class MonkeysPageViewModel : BaseViewModel
    {
        private readonly IFileHelper fileHelper;
        public MonkeysPageViewModel()
        {
            Title = "Monkeys";
            Monkeys = new ObservableCollection<MonkeyViewModel>();
            fileHelper = DependencyService.Get<IFileHelper>();
            
        }
        public Command RefreshCommand
        {
            get
            {
                return refreshCommand ?? (refreshCommand = new Command(async () => await LoadMonkeys()));
            }
        }

        Command refreshCommand;


        public async Task LoadMonkeys()
        {
            IsBusy = true;
            Monkeys.Clear();

            var dataManager = App.MonkeyDataManager;

            var monkeys = await dataManager.GetMonkeysAsync();
             
            foreach (var item in monkeys)
            {
         
                var imageFiles = await dataManager.GetImageFiles(item);
                // Display only the photo files
                if (imageFiles != null && imageFiles.Count() > 0)
                {
                    var monkeyVm = new MonkeyViewModel(item);
                    monkeyVm.PhotoUrl = fileHelper.GetLocalFilePath(
                             item.Id, imageFiles.First().Name);

                    Monkeys.Add(monkeyVm);
                }
                IsBusy = false;

            }

            //var collection = new ObservableCollection<MonkeyViewModel>();

            //collection.Add(new MonkeyViewModel(new Models.Monkey
            //{
            //    UserName = "Nish",
            //    Status = "Chilling out in Malaysia",
            //    PhotoUrl = "http://photos1.meetupstatic.com/photos/event/8/f/d/6/600_343476822.jpeg"
            //}));
            //collection.Add(new MonkeyViewModel(new Models.Monkey
            //{
            //    UserName = "Nish",
            //    Status = "Wooh! Got boxed",
            //    PhotoUrl = "http://photos1.meetupstatic.com/photos/event/8/f/d/6/600_343476822.jpeg"
            //}));

            // monkeys = collection;
        }

        private ObservableCollection<MonkeyViewModel> monkeys;

        public ObservableCollection<MonkeyViewModel> Monkeys
        {
            get { return monkeys; }
            set { monkeys = value; RaisePropertyChanged(); }
        }
    }
}

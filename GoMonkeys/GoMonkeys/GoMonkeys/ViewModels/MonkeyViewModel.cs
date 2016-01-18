using GoMonkeys.Models;

namespace GoMonkeys.ViewModels
{
    public class MonkeyViewModel 
        : BaseViewModel
    {
        private Monkey monkey;

        public Monkey Monkey
        {
            get { return monkey; }
            set { monkey = value; RaisePropertyChanged(); }
        }


        public MonkeyViewModel()
        {

        }
        public MonkeyViewModel(Monkey monkey)
        {
            Monkey = monkey;
        }
        public float ImageWidth
        {
            get
            {
                return (float)App.ScreenSize.Width;
            }
        }

        public float ImageHeight
        {
            get
            {
                return (float)(App.ScreenSize.Width / 1.333d);
            }
        }

        public string Id { get { return monkey.Id; } }
        public string Status { get {
                return monkey.Status;
            } }
        private string photoUrl;

        public string PhotoUrl
        {
            get { return photoUrl; }
            set { photoUrl = value; RaisePropertyChanged(); }
        }

        public string UserName { get { return monkey.UserName; } }
    }
}

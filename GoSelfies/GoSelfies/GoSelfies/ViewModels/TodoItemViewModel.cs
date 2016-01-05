using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoSelfies.ViewModels
{
    public class TodoItemViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged(); }
        }

        private string imageUrl;

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; RaisePropertyChanged(); }
        }
        
        
        public TodoItemViewModel()
        {
        }
    }
}

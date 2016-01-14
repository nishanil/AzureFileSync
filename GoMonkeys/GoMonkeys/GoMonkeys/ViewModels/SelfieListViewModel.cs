using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GoMonkeys.ViewModels
{
    public class SelfieListViewModel : BaseViewModel
    {
        private ObservableCollection<TodoItemViewModel> todoList;

        public ObservableCollection<TodoItemViewModel> TodoList
        {
            get { return todoList; }
            set { todoList = value; RaisePropertyChanged(); }
        }

        private readonly TodoItemManager manager;
        private readonly IFileHelper fileHelper;
        public SelfieListViewModel()
        {
            manager = new TodoItemManager();
            fileHelper = DependencyService.Get<IFileHelper>();
            //PopulateTodoList();
        }

        public async Task PopulateTodoList()
        {
            await manager.SyncAsync();
            var todoItems = await manager.GetTodoItemsAsync();
            var todoItemViewModel = new ObservableCollection<TodoItemViewModel>();

            foreach (var item in todoItems)
            {
                var imageFiles = await manager.GetImageFiles(item, true);
                // Display only the photo files
                if (imageFiles != null && imageFiles.Count() > 0)
                {
                    todoItemViewModel.Add(new TodoItemViewModel
                    {
                        Name = item.Name,
                        ImageUrl = fileHelper.GetLocalFilePath(
                            item.Id, imageFiles.First().Name)
                    });
                }
            }
            TodoList = todoItemViewModel;


        }
            
    }
}

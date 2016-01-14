using Microsoft.Azure.Mobile.Server;

namespace GoMonkeysService.DataObjects
{
    public class TodoItem : EntityData
    {
        public string Text { get; set; }

        public bool Complete { get; set; }
    }
}
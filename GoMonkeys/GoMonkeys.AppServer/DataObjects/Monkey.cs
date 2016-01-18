using Microsoft.Azure.Mobile.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoMonkeysService.DataObjects
{
    public class Monkey : EntityData
    {
        public string Status { get; set; }
        public string UserName { get; set; }
    }
}
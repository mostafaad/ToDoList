using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Web.Models
{
    public class UserToDoListViewModel
    {
        public int ID { get; set; }
        public string UserID { get; set; }
        public string Title { get; set; }
        public string date { get; set; }

        public DateTime DueDate { get; set; }

    }
}
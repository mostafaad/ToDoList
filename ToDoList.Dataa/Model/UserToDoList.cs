using ToDoList.Dataa.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.Dataa.Model
{
   public class UserToDoList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Users")]
        public string UserID { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }

        public virtual ApplicationUser Users { get; set; } 
        public bool IsDeleted { get; set; }
        public UserToDoList()
        {
            IsDeleted = false;
        }

    }
}

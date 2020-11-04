using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToDoList.Dataa;
using ToDoList.Dataa.Model;
using ToDoList.Dataa.Models;

namespace ToDoList.Service.Services
{
   public class UserToDoListService
    {
        IRepository repository = new Repository(new ApplicationDbContext());
        public List<UserToDoList> GetUserToDoListList()
        {
            var Result = repository.GetAll<UserToDoList>().ToList();
            return Result;
        }

        public UserToDoList GetUserToDoList(int Id)
        {

            var Result = repository.FindOne<UserToDoList>(c => c.ID == Id);

            return Result;
        }

        public int AddUserToDoList(UserToDoList _UserToDoList)
        {
            repository.Add<UserToDoList>(_UserToDoList);
            repository.UnitOfWork.SaveChanges();

            return _UserToDoList.ID;

        }

        public void DeleteUserToDoList(int Id)
        {
            var data = repository.FindOne<UserToDoList>(c => c.ID == Id);
            data.IsDeleted = true;
            repository.Update<UserToDoList>(data);
            repository.UnitOfWork.SaveChanges();

        }
        public void UpdateUserToDoList(UserToDoList _UserToDoList)
        {
            repository.Update<UserToDoList>(_UserToDoList);
            repository.UnitOfWork.SaveChanges();

        }
    }
}

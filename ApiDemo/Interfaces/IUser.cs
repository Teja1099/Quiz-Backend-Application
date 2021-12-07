using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Model;


namespace ApiDemo.Interfaces
{
    public interface IUser
    {
        List<User> GetUsers();
        User GetUserById(Guid id);
        User AddUser(User user);
        void DeleteUser(User user);
        User EditStudent(User user);
     
    }
}

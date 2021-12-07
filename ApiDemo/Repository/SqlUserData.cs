using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Interfaces;
using ApiDemo.Model;

namespace ApiDemo.Repository
{
    public class SqlUserData : IUser
    {
       
        private UserContext _userContext { set; get; }
        public SqlUserData(UserContext userContext)
        {
            _userContext = userContext;
        }
        public User AddUser(User user)
        {
            DateTime dateTime = DateTime.Now;
           
           
   
            _userContext.Users.Add(user);
            _userContext.SaveChanges(); 
            return user;
        }

        public void DeleteUser(User user)
        {
            _userContext.Users.Remove(user);
            _userContext.SaveChanges();
        }

        public User EditStudent(User user)
        {
            throw new NotImplementedException();
        }

        public User GetUserById(Guid id)
        {
            return _userContext.Users.Find(id);
        }

        public List<User> GetUsers()
        {
            return _userContext.Users.ToList();
        }
    }
}

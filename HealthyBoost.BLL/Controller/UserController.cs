using HealthyBoost.DAL.Entity;
using HealthyBoost.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace HealthyBoost.BLL.Controller
{
    public class UserController
    {
        HealthyDbContext context;
        public UserController()
        {
            context = new HealthyDbContext();
        }
        public User GetUserById(int id)
        {
            return context.Users.SingleOrDefault(c => c.UserID == id);
        }

        public List<User> GetAllUsers()
        {
            return context.Users.ToList();
        }

        public List<User> GetPasiveUsers()
        {
            return context.Users.Where(x => x.State == false).ToList();
        }

        public bool UserActivate(int id)
        {
            User user = context.Users.SingleOrDefault(x => x.UserID == id);
            user.State = true;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool UserDeActivate(int id)
        {
            User user = context.Users.SingleOrDefault(x => x.UserID == id);
            user.State = false;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public List<User> FindUser(string word)
        {
            return context.Users.Where(x => x.Name.Contains(word) || x.LastName.Contains(word) || x.UserName.Contains(word)).ToList();
        }

        public bool Insert(User entity)
        {

            context.Users.Add(entity);
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool Update(User entity)
        {
            User updatedUser = context.Users.SingleOrDefault(c => c.UserID == entity.UserID);
            updatedUser = entity;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }

        public bool ChangePassword(int userid, string password)
        {
            User updatedUser = context.Users.SingleOrDefault(c => c.UserID == userid);
            updatedUser.Password = password;
            int affRows = context.SaveChanges();
            return affRows > 0;
        }


        public User CheckLogin(string username, string password)
        {
            User user;
            user = context.Users.SingleOrDefault(x => x.UserName == username && x.Password == password);
            return user;
        }
    }
}

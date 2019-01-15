using DoneThatMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.DatabaseAccess
{
    public static class UserManager
    {
        public static User AddUser(User user)
        {
            User userDb;
            using (AppContext db = new AppContext())
            {
                db.Users.Add(user);
                userDb = db.Users.FirstOrDefault(i => i.Id == user.Id);
                db.SaveChanges();
            }
            return userDb;
        }

        public static User EditUser(User user)
        {

            using (AppContext db = new AppContext())
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            return user;
        }

        //withNotActive:true = returns all user + disactivated
        public static List<User> GetUsers(bool withNotActive)
        {
            List<User> users;
            using (AppContext db = new AppContext())
            {
                if (withNotActive) users = db.Users.ToList();
                else users = db.Users.Where(i => i.IsActive != false).ToList();
            }
            return users;
        }

        public static User GetUserById(int id)
        {
            User user;
            using (AppContext db = new AppContext())
            {
                user = db.Users.FirstOrDefault(i => i.Id == id);
            }
            return user;
        }

        public static User GetUserByUsername(string username)
        {
            User user;
            using (AppContext db = new AppContext())
            {
                user = db.Users.FirstOrDefault(i => i.Username == username);
            }
            return user;
        }
    }
}

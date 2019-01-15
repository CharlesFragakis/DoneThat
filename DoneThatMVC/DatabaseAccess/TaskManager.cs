using DoneThatMVC.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.DatabaseAccess
{
    public static class TaskManager
    {
        public static void AddTask(Models.Task task)
        {
            using (AppContext db = new AppContext())
            {
                Models.Task taskDb = new Models.Task();
                User user = db.Users.FirstOrDefault(i => i.Id == task.UserId);
                taskDb.User = user;
                taskDb.Completed = task.Completed;
                taskDb.Description = task.Description;
                taskDb.Finish = task.Finish;
                taskDb.Priority = task.Priority;
                taskDb.Start = task.Start;
                taskDb.Title = task.Title;
                db.Tasks.Add(taskDb);
                db.SaveChanges();
            }
        }

        public static void EditTask(Models.Task task)
        {
            using (AppContext db = new AppContext())
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteTask(Models.Task task)
        {
            using (AppContext db = new AppContext())
            {
                db.Entry(task).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }

        public static Models.Task GetTaskById(int id)
        {
            Models.Task task = new Models.Task();
            using (AppContext db = new AppContext())
            {
                task = db.Tasks.Find(id);
            }
            return task;
        }

        public static List<Models.Task> GetAllTasks(int userId)
        {
            List<Models.Task> tasks;
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.Where(i => i.UserId == userId).ToList();
            }
            return tasks;
        }

        public static List<Models.Task> GetTasksByDate(DateTime date,string username)
        {
            List<Models.Task> tasks;
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.Where(i => i.Date == date.Date && i.User.Username == username).ToList();
            }
            return tasks;
        }

        public static List<Models.Task> GetTasksForNext7Days(DateTime currentDate,string username)
        {
            DateTime dateLast = currentDate.AddDays(7);
            List<Models.Task> tasks;
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.Where(i => i.Date > currentDate && i.Date < dateLast && i.User.Username == username).ToList();
            }
            return tasks;
        }

        public static List<Models.Task> GetWeeklyTasks(DateTime currentDate,string username)
        {
            List<Models.Task> tasks;
            DateTime mondayDate = currentDate.AddDays(-(float)currentDate.DayOfWeek);
            DateTime sundayDate = currentDate.AddDays((float)currentDate.DayOfWeek);
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.Where(i => i.Date > mondayDate && i.Date < sundayDate && i.User.Username == username).ToList();
            }
            return tasks;
        }

        public static List<Models.Task> GetMonthlyTasks(DateTime currentDate, string username)
        {
            List<Models.Task> tasks;
            using (AppContext db = new AppContext())
            {
                tasks = db.Tasks.Where(i => i.Date.Month == currentDate.Month && i.User.Username == username).ToList();
            }
            return tasks;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DoneThatMVC.Models;
using DoneThatMVC.DatabaseAccess;

namespace DoneThatMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult DailyTasks()
        {
            List<Models.Task> tasks = TaskManager.GetTasksByDate(DateTime.Now,User.Identity.Name);

              return View(tasks);
                                    
        }

        public IActionResult WeeklyTasks()
        {
            List<Models.Task> tasks = TaskManager.GetWeeklyTasks(DateTime.Now, User.Identity.Name);

            return View(tasks);

        }

        public IActionResult MonthlyTasks()
        {
            List<Models.Task> tasks = TaskManager.GetMonthlyTasks(DateTime.Now, User.Identity.Name);

            return View(tasks);

        }

        public IActionResult CreateTask()
        {
            return View(new TaskVM());
        }
        [HttpPost]
        public IActionResult CreateTask(TaskVM task)
        {
            if(ModelState.IsValid)
            {
                Models.Task taskdb= new Models.Task();
                taskdb.Completed = false;
                taskdb.Date = task.Date;
                taskdb.Description = task.Description;
                taskdb.Finish = task.Finish;
                taskdb.Priority = task.Priority;
                taskdb.Start = task.Start;
                taskdb.Title = task.Title;
                User user = UserManager.GetUserByUsername(User.Identity.Name);
                taskdb.UserId = user.Id;
                taskdb.User = user;

                TaskManager.AddTask(taskdb);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                //TO do error message not valid task
                return null;
            }
            
        }

        //public IActionResult EditTask(int taskId)
        //{
        //    Models.Task task = TaskManager.
        //}

    }
}

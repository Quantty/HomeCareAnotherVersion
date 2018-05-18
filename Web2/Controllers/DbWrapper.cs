using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web2.Models;

namespace Web2.Controllers
{
    public class DbWrapper : Controller
    {
        public DbWrapper()
        {

        }
        public IQueryable<CustomerTask> GetTasks()
        {
            var dataContext = new TaskModelDataContext();
            var tasks = (from m in dataContext.CustomerTasks
                         select m);
            return tasks;
        }

        public IQueryable<Schedule> GetSchedules()
        {
            var dataContext = new ScheduleModelDataContext();
            var schedules = (from m in dataContext.Schedules
                         select m);
            return schedules;
        }
        public void addTask(CustomerTask task)
        {
            var dataContext = new TaskModelDataContext();
            dataContext.CustomerTasks.InsertOnSubmit(task);
            dataContext.SubmitChanges();
        }


        public CustomerTask getTaskById(int? id)
        {
           
            var dataContext = new TaskModelDataContext();
            var query = (from m in dataContext.CustomerTasks
                            where m.Id == id
                            select m);
            CustomerTask task = query.First();

            return task;
            
        }

        public void updateTask(CustomerTask task)
        {
            var dataContext = new TaskModelDataContext();
            var query = (from m in dataContext.CustomerTasks
                         where m.Id == task.Id
                         select m);
            query.First().title = task.title;
            query.First().description = task.description;
            dataContext.SubmitChanges();
        }
        
        public void deleteTaskById(int? id)
        {
            var dataContext = new TaskModelDataContext();
            var query = (from m in dataContext.CustomerTasks
                         where m.Id == id
                         select m);
            dataContext.CustomerTasks.DeleteOnSubmit(query.First());
            dataContext.SubmitChanges();
        }
    }
}
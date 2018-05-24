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
        public void addRelative(Relative relative)
        {
            var dataContext = new RelativeModelDataContext();
            dataContext.Relatives.InsertOnSubmit(relative);
            dataContext.SubmitChanges();
        }
        public void addSchedule(Schedule schedule)
        {
            var dataContext = new ScheduleModelDataContext();
            dataContext.Schedules.InsertOnSubmit(schedule);
            dataContext.SubmitChanges();
        }

        public CustomerTask getTaskById(int? id)
        {
           
            var dataContext = new TaskModelDataContext();
            var query = (from m in dataContext.CustomerTasks
                            where m.Id == id
                            select m);
            var task = query.First();
            if (task != null) {
                return task;
            }
            else
            {
                return null;
            }            
        }

        public Schedule getScheduleById(int? id)
        {

            var dataContext = new ScheduleModelDataContext();
            var query = (from m in dataContext.Schedules
                         where m.Id == id
                         select m);
            var schedule = query.First();
            if (schedule != null)
            {
                return schedule;
            }
            else
            {
                return null;
            }
        }

        public Relative getRelativeById(int? id)
        {

            var dataContext = new RelativeModelDataContext();
            var query = (from m in dataContext.Relatives
                         where m.Id == id
                         select m);
            var relative = query.First();
            if (relative != null)
            {
                return relative;
            }
            else
            {
                return null;
            }
        }

        public void updateTask(CustomerTask task)
        {
            var dataContext = new TaskModelDataContext();
            var query = (from m in dataContext.CustomerTasks
                         where m.Id == task.Id
                         select m);
            query.First().title = task.title;
            query.First().duration = task.duration;
            query.First().description = task.description;
            dataContext.SubmitChanges();
        }
        public void updateRelative(Relative relative)
        {
            var dataContext = new RelativeModelDataContext();
            var query = (from m in dataContext.Relatives
                         where m.Id == relative.Id
                         select m);
            query.First().name = relative.name;
            query.First().relation = relative.relation;
            query.First().email = relative.email;
            query.First().phone_number = relative.phone_number;
            query.First().customer_Id = relative.customer_Id;
            dataContext.SubmitChanges();
        }

        public void updateSchedule(Schedule schedule)
        {
            var dataContext = new ScheduleModelDataContext();
            var query = (from m in dataContext.Schedules
                         where m.Id == schedule.Id
                         select m);
            var sched = query.First();
            sched.customer_Id = schedule.customer_Id;
            sched.employee_Id = schedule.employee_Id;
            sched.date = schedule.date;
            sched.task_Id = schedule.task_Id;
            sched.time = schedule.time;
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

        public void deleteScheduleById(int? id)
        {
            var dataContext = new ScheduleModelDataContext();
            var query = (from m in dataContext.Schedules
                         where m.Id == id
                         select m);
            dataContext.Schedules.DeleteOnSubmit(query.First());
            dataContext.SubmitChanges();
        }

        public IQueryable<Relative> GetRelatives()
        {
            var dataContext = new RelativeModelDataContext();
            var relatives = (from m in dataContext.Relatives
                         select m);
            return relatives;
        }

        public void deleteRelativeById(int? id)
        {
            var dataContext = new RelativeModelDataContext();
            var query = (from m in dataContext.Relatives
                         where m.Id == id
                         select m);
            dataContext.Relatives.DeleteOnSubmit(query.First());
            dataContext.SubmitChanges();
        }
    }
}
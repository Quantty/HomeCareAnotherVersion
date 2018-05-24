using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web2.Models;

namespace Web2.Controllers
{
    public class CitrusController : ApiController
    {
        DbWrapper dbWrapper = new DbWrapper();
        // GET api/Citrus
        public List<Citrus> GetCitrus()
        {
            List<Citrus> citruses = new List<Citrus>();
            citruses.Add(new Citrus { id = 0, name = "lemon" });
            citruses.Add(new Citrus { id = 1, name = "orange" });
            citruses.Add(new Citrus { id = 2, name = "lime" });
            citruses.Add(new Citrus { id = 3, name = "blood orange" });
            citruses.Add(new Citrus { id = 4, name = "tangerine" });

            return citruses;
        }

        // GET api/Citrus/3
        public Citrus GetCitrusById(int id)
        {
            List<Citrus> citruses = new List<Citrus>();
            citruses.Add(new Citrus { id = 0, name = "lemon" });
            citruses.Add(new Citrus { id = 1, name = "orange" });
            citruses.Add(new Citrus { id = 2, name = "lime" });
            citruses.Add(new Citrus { id = 3, name = "blood orange" });
            citruses.Add(new Citrus { id = 4, name = "tangerine" });

            return citruses[id];
        }
        //Schedules
        [HttpGet]
        public List<Schedule> getSchedules()
        {
            var schedules = dbWrapper.GetSchedules().ToList();
            return schedules;
        }

        [HttpGet]
        public Schedule getScheduleById(int id)
        {
            var schedule = dbWrapper.getScheduleById(id);
            return schedule;
        }
        [HttpPost]
        public HttpResponseMessage addSchedule(Schedule schedule)
        {
            dbWrapper.addSchedule(schedule);
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }
        [HttpPost]
        public HttpResponseMessage updateSchedule(Schedule schedule)
        {
            dbWrapper.updateSchedule(schedule);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
        [HttpPost]
        public HttpResponseMessage deleteScheduleById(int? id)
        {
            dbWrapper.deleteScheduleById(id);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        //Tasks
        [HttpGet]
        public List<CustomerTask> getTasks()
        {
            var tasks = dbWrapper.GetTasks().ToList();
            return tasks;
        }

        [HttpGet]
        public CustomerTask getTaskById(int id)
        {
            var task = dbWrapper.getTaskById(id);
            return task;
        }

        [HttpPost]
        public HttpResponseMessage addTask(CustomerTask task)
        {
            dbWrapper.addTask(task);
            return this.Request.CreateResponse(HttpStatusCode.Created);
        }

        [HttpPost]
        public HttpResponseMessage updateTask(CustomerTask task)
        {
            dbWrapper.updateTask(task);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpPost]
        public HttpResponseMessage deleteTaskById(int id)
        {
            dbWrapper.deleteTaskById(id);
            return this.Request.CreateResponse(HttpStatusCode.OK);
        }



    }
}
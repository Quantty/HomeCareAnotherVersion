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
        // GET api/<controller>
        public List<Citrus> Get()
        {
            List<Citrus> citruses = new List<Citrus>();
            citruses.Add(new Citrus { id = 0, name = "lemon" });
            citruses.Add(new Citrus { id = 1, name = "orange" });
            citruses.Add(new Citrus { id = 2, name = "lime" });
            citruses.Add(new Citrus { id = 3, name = "blood orange" });
            citruses.Add(new Citrus { id = 4, name = "tangerine" });

            return citruses;
        }

        // GET api/<controller>/5
        public Citrus Get(int id)
        {
            List<Citrus> citruses = new List<Citrus>();
            citruses.Add(new Citrus { id = 0, name = "lemon" });
            citruses.Add(new Citrus { id = 1, name = "orange" });
            citruses.Add(new Citrus { id = 2, name = "lime" });
            citruses.Add(new Citrus { id = 3, name = "blood orange" });
            citruses.Add(new Citrus { id = 4, name = "tangerine" });

            return citruses[id];
        }

    }
}
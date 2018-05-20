using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web2.Models
{
    public class MixViewModel
    {
        public MixViewModel() { }
        public ApplicationUser employee { get; set; }
        public ApplicationUser customer { get; set; }

        public CustomerTask task { get; set; }
        public Schedule schedule { get; set; }

     
    }
}
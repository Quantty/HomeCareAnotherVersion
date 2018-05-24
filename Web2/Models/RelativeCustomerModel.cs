using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web2.Models
{
    public class RelativeCustomerModel
    {
        public RelativeCustomerModel() { }
        public Relative relative { get; set; }
        public ApplicationUser customer { get; set; }

    }
}
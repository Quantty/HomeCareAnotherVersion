using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web2.Models
{
    public class MasterMixViewModel
    {
        public MasterMixViewModel() { }
        public List<MixViewModel> mixViewModel { get; set; }
        public List<Relative> relatives { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnneling.InstanceGenerator.Domain
{
    public class Period
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int StartDay { get; set; }
        public int EndDay { get; set; }
    }
}

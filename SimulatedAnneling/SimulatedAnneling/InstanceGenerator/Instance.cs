using SimulatedAnneling.InstanceGenerator.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedAnneling.InstanceGenerator
{
    public class Instance
    {
        public Period Period { get; set; }
        public List<Physician> Physician { get; set; }
        public List<ShiftsPattern> Shifts { get; set; }

    }
}

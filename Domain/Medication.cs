using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Medication
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public byte[] Image { get; set; }
        public Drone Drone { get; set; }
    }
}

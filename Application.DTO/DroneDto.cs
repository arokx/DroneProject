using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO
{
    public class DroneDto
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        public string State { get; set; }
        public List<MedicationDto> Medications { get; set; }
    }
}

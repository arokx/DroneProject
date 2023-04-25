﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Drone
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DroneModel Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        public DroneState State { get; set; }
        public ICollection<Medication> Medications { get; set; }
    }
}

﻿using Application.DTO;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.RegisterDrone
{
    public class RegisterDroneCommand : IRequest<DroneDto>
    {
        public string SerialNumber { get; set; }
        public string Model { get; set; }
        public double WeightLimit { get; set; }
        public double BatteryCapacity { get; set; }
        public string State { get; set; }
    }
}

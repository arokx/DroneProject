using Application.DTO;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Query.LoadDrone
{
    public class LoadDroneWithMedicationQuery : IRequest<DroneDto>
    {
        public int DroneId { get; set; }
    }
}

using Application.DTO;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CreateMedication
{
    public class CreateMedicationCommand : IRequest<MedicationDto>
    {
        public string Name { get; set; }
        public double Weight { get; set; }
        public string Code { get; set; }
        public byte[]? Image { get; set; }
        public int DroneId { get; set; }
    }
}

using Application.Command.RegisterDrone;
using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Command.CreateMedication
{
    public class CreateMedicationCommandHandler : IRequestHandler<CreateMedicationCommand, MedicationDto>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly DatabaseContext Context;
        public CreateMedicationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, DatabaseContext context)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Context = context;
        }

        public async Task<MedicationDto> Handle(CreateMedicationCommand request, CancellationToken cancellationToken)
        {
            var drone = await UnitOfWork.GetRepository<Drone>().GetByIdAsync(request.DroneId);
            if (drone != null)
            {
                Medication medication = new Medication { Code = request.Code, Name = request.Name, Weight = request.Weight, Drone = drone , Image = request.Image };
                await UnitOfWork.GetRepository<Medication>().InsertAsync(medication);
                await UnitOfWork.SaveAsync();
                MedicationDto medicationDto = Mapper.Map<MedicationDto>(medication);
                return medicationDto;
            }
            else {
                throw new Exception("Invalid DroneId. Please get a valid DroneId.");
            }
        }
    }
}

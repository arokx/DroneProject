using Application.DTO;
using Application.Interface;
using AutoMapper;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Command
{
    public class RegisterDroneCommandHandler : IRequestHandler<RegisterDroneCommand, DroneDto>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        public RegisterDroneCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

        public async Task<DroneDto> Handle(RegisterDroneCommand request, CancellationToken cancellationToken)
        {
            Drone drone = new Drone { SerialNumber = request.SerialNumber, Model = request.Model, BatteryCapacity = request.BatteryCapacity, WeightLimit = request.WeightLimit,State = request.State };
            await UnitOfWork.GetRepository<Drone>().InsertAsync(drone);
            await UnitOfWork.SaveAsync();
            DroneDto droneDto = Mapper.Map<DroneDto>(drone);
            return droneDto;
        }
    }
}

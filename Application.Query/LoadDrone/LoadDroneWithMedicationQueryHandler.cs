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

namespace Application.Query.LoadDrone
{
    public class LoadDroneWithMedicationQueryHandler : IRequestHandler<LoadDroneWithMedicationQuery, DroneDto>
    {
        private readonly IUnitOfWork UnitOfWork;
        private readonly IMapper Mapper;
        private readonly DatabaseContext Context;
        public LoadDroneWithMedicationQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, DatabaseContext context)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
            Context = context;
        }

        public async Task<DroneDto> Handle(LoadDroneWithMedicationQuery request, CancellationToken cancellationToken)
        {
            double medicationWeight = 0;
            Drone drone = Context?.Drone.Include(d => d.Medications).FirstOrDefault(d => d.Id == request.DroneId);

            if (drone == null)
            {
                throw new Exception($"Drone with drone id {request.DroneId} not found");
            }

            // Check the sum of the medication weight
            if (drone != null && drone.Medications != null & drone?.Medications?.Count > 0)
            {
                for (int i = 0; i < drone?.Medications?.Count; i++)
                {
                    if (drone?.Medications[i].Weight != null)
                        medicationWeight += drone.Medications[i].Weight;
                }
            }

            // Check if the drone is already loaded to the maximum weight
            if (drone.WeightLimit < medicationWeight)
            {
                throw new Exception($"Drone is trying to load {medicationWeight} gr. But cannot be loaded because drone weight limit is {drone.WeightLimit}gr");
            }

            // Check if the drone is in the LOADING state and has enough battery level
            if (drone.State == DroneState.LOADING.ToString() && drone.BatteryCapacity < 25)
            {
                throw new Exception($"Drone with serial number {drone.SerialNumber} has not enough battery level");
            }

            // Find drones that are not in the LOADED, DELIVERING or DELIVERED state
            if (drone.State == DroneState.LOADED.ToString() || drone.State == DroneState.DELIVERING.ToString() || drone.State == DroneState.DELIVERED.ToString())
            {
                throw new Exception($"Drone drone is already {drone.State} and it not available. Please look for another drone.");
            }

            DroneDto droneDto = Mapper.Map<DroneDto>(drone);
            return droneDto;
        }
    }
}

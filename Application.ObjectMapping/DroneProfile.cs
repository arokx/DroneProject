using Application.DTO;
using AutoMapper;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ObjectMapping
{
    public class DroneProfile:Profile
    {
        public DroneProfile() 
        {
            CreateMap<Drone, DroneDto>().ReverseMap();
        }     
    }
}

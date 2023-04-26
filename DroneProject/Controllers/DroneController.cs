using Application.Command.RegisterDrone;
using Application.Query.LoadDrone;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DroneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DroneController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDrone(RegisterDroneCommand command)
        {
            var drone = await _mediator.Send(command);
            return Ok(drone);
        }
        [HttpGet]
        public async Task<IActionResult> LoadDroneAsync(int droneId)
        {
            var droneWithMed = await _mediator.Send(new LoadDroneWithMedicationQuery { DroneId = droneId});
            return Ok(droneWithMed);
        }
        }
}

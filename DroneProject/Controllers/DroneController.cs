using Application.Command.RegisterDrone;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}

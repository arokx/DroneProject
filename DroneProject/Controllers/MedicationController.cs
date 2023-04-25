using Application.Command.CreateMedication;
using Application.Command.RegisterDrone;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MedicationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateMedication(CreateMedicationCommand command)
        {
            var medication = await _mediator.Send(command);
            return Ok(medication);
        }
    }
}

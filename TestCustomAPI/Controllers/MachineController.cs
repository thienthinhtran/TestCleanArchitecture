using MediatR;
using Microsoft.AspNetCore.Mvc;
using Service.Abstract;
using Service.Command;
using Service.Queries;
using Service.Common;
using Service.Responses;
using static Service.Responses.InsertMachineDTOResponse;

namespace TestCustomAPI.Controllers
{
    [ApiController]
    [Route("api/machines")]
    public class MachineController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MachineController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetMachineAll()
        {
            var query = new GetAllMachineDTOQuery();
            var result = await _mediator.Send(query);

            var response = new GetAllMachineDTOResponse
            {
                Machines = result
            };

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMachineById(int id)
        {
            var query = new GetMachineByIdQuery { MachineId = id };
            var machine = await _mediator.Send(query);

            if (machine == null)
            {
                return NotFound(); // Return 404 if the machine with the specified ID is not found.
            }

            return Ok(machine);
        }
        [HttpPost("insert")]
        public async Task<IActionResult> InsertMachine([FromBody] InsertMachineDTO machine)
        {
            var command = new InsertMachineCommand
            {
                Machine = machine
            };

            var machineId = await _mediator.Send(command);

            var response = new InsertMachineDTOResponse
            {
                MachineId = machineId
            };

            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> UpdateMachine(int id, [FromBody] UpdateMachineDTO machine)
        {
            var command = new UpdateMachineCommand
            {
                MachineId = id,
                Machine = machine
            };

            var success = await _mediator.Send(command);

            if ((bool)success)
            {
                return Ok("Machine updated successfully.");
            }
            else
            {
                return BadRequest("Failed to update machine.");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMachine(int id)
        {
            var command = new DeleteMachineCommand { MachineId = id };
            var result = await _mediator.Send(command);

            if (result)
            {
                return Ok("Machine deleted successfully.");
            }
            else
            {
                return NotFound("Machine not found or deletion failed.");
            }
        }
    }
}

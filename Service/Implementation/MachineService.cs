using Data.Abstraction;
using Domain.Entities;
using MediatR;
using Service.Abstract;
using Service.Queries;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Implementation
{
    public class MachineService : IMachineService
    {
        private readonly IMediator _mediator;

        public MachineService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IEnumerable<GetMachineDTO>> GetAllMachineDTOAsync()
        {
            var query = new GetAllMachineDTOQuery();
            return await _mediator.Send(query);
        }
    }
}

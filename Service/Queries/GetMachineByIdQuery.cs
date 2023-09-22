using MediatR;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Queries
{
    public class GetMachineByIdQuery : IRequest<GetMachineDTO>
    {
        public int MachineId { get; set; }
    }
}

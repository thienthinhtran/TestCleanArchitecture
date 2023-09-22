using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command
{
    public class DeleteMachineCommand : IRequest<bool>
    {
        public int MachineId { get; set; }
    }
}

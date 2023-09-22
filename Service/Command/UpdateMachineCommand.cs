using MediatR;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command
{
    public class UpdateMachineCommand : IRequest<bool>
    {
        public int MachineId { get; set; }
        public UpdateMachineDTO Machine { get; set; }
    }
}

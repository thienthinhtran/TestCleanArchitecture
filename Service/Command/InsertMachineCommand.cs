using Domain.Enum;
using MediatR;
using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command
{
    public class InsertMachineCommand : IRequest<int>
    {
        public InsertMachineDTO Machine { get; set; }
       /* public string Code { get; set; }
        public int IdCPU { get; set; }
        public int IdBrand { get; set; }
        public int IdRam { get; set; }
        public double Price { get; set; }
        public string? Serial { get; set; }
        public string? Name { get; set; }
        public StateMC MachineState { get; set; }*/

    }
}

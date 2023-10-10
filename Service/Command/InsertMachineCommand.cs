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
        public InsertMachineDTO? Machine { get; set; }
    }
}

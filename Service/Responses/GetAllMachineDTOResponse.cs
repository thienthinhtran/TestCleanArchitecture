using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Responses
{
    public class GetAllMachineDTOResponse
    {
        public IEnumerable<GetMachineDTO> Machines { get; set; }
    }
}

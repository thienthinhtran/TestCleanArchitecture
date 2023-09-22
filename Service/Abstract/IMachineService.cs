using Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Abstract
{
    public interface IMachineService
    {
        Task<IEnumerable<GetMachineDTO>> GetAllMachineDTOAsync();

    }
}

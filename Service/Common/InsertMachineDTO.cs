using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Common
{
    public class InsertMachineDTO
    {
        public int idCpu { get; set; }
        public int idBrand { get; set; }
        public int idRam { get; set; }
        public StateMC MachineState { get; set; }
    }
}

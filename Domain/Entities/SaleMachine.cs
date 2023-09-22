using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleMachine : BaseAuditableEntity
    {
       /* public int MachineId { get; set; }

        public int SaleInfoId { get; set; }*/

        public int MachineId { get; set; }
        public int SaleInfoId { get; set; }
    }
}

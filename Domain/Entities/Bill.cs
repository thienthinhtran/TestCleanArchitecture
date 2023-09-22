using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bill : BaseAuditableEntity
    {
        public int IdMachine { get; set; }
        public string? NameMachine { get; set; }
        public string? SaleMachine { get; set; }
        public int State { get; set; }
    }
}

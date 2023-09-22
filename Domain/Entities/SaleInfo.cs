using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SaleInfo : BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? SaleType { get; set; }
        public double? SaleAmount { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}

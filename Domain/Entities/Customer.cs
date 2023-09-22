using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : BaseAuditableEntity
    {
        public int idBill { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Phone {  get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Rank { get; set; }
    }
}

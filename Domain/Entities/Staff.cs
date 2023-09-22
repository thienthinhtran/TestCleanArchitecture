using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Staff : BaseAuditableEntity
    {
        public int IdBillCustomer { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? BornDate { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Position { get; set; }
    }
}

using Domain.Common;
using Domain.Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BillCustomer : BaseAuditableEntity
    {
        public int idBill {  get; set; }
        public int IdStaff {  get; set; }
        public int IdCustomer {  get; set; }
        public string? Code { get; set; }
        public double? lastPrice { get; set; }
        public double? ExcessMoney { get; set; }
        public double? ReceivedMoney { get; set; }
        public double? MoneyBack { get; set; }
        public StateB BillState { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.Payment
{
    public class AddPayment
    {
        public long TrackingNumber { get; set; }
        public decimal Amount { get; set; }
        public string GatewayName { get; set; }
        public string GatewayAccountName { get; set; }
        public string Message { get; set; }
        public string AdditionalData { get; set; }
    }
}

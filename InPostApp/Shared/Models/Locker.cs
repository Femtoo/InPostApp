using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPostApp.Shared.Models
{
    public class Locker
    {
        public int LockerId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public List<Shipment> FromLockerDelivery { get; set; } = new List<Shipment>();
        public List<Shipment> ToLockerDelivery { get; set; } = new List<Shipment>();
    }
}

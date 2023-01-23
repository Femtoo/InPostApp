using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPostApp.Shared.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int? SenderId { get; set; }
        public User Sender { get; set; }
        public int? ReceiverId { get; set; }
        public User Receiver { get; set; }

        public int? FromLockerId { get; set; }
        public Locker FromLocker { get; set; }

        public int? ToLockerId { get; set; }
        public Locker ToLocker { get; set; }
    }
}

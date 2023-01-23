using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InPostApp.Shared.ModelsDto
{
    public class AddShipmentDto
    {
        public string Name { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
    }
}

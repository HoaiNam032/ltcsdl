using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBillManagementSystem.Entities
{
    public class ConsumptionDTO
    {
        public int SerialID { get; set; }
        public DateTime Month { get; set; }
        public decimal ConsumptionAmount { get; set; }
        public int SegmentNumber { get; set; }
        public decimal Price { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WaterBillManagementSystem.Entities
{
    public class CustomerDTO
    {
        public int SerialID { get; set; }
        public string UserName { get; set; }
        public long NationalID { get; set; }
        public string Address { get; set; }
        public int? CustomerType { get; set; }
    }
}

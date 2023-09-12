using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderCount { get; set; }
        public decimal OrderPrice { get; set; }
        public int OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}

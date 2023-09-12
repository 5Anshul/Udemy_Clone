using Edtech_backend_API.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Edtech_backend_API.Model
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }
        public int ShoppingCartCount { get; set; }
        [NotMapped]
        public double Price { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}

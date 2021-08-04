using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Category")]
    public class Category : BaseEntity
    {
        //Dependency Ịnjection
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id {get; set;}
        public string Name {get; set;}
        public virtual ICollection<Product> Products {get; set;}
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Category")]
    public class Category : BaseEntity
    {
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
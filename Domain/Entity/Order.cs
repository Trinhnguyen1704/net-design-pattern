using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Order")]
    public class Order : BaseEntity
    {
        //Dependency Ịnjection
        public Order()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id {get; set;}
        [Required]
        public string OrderNo {get; set;}
        public int AccountId {get; set;}
        [ForeignKey("AccountId")]
        public virtual Account Account {get; set;}
        public virtual ICollection<Product> Products {get; set;}
    }
}
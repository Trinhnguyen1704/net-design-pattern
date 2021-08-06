using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Product")]
    public class Product : BaseEntity
    {
        [Key]
        public int Id {get; set;}
        [Required][MaxLength(320)]
        public string Name {get; set;}
        public float Price {get; set;}
        public int NumInStock {get; set;}
        public string Description {get; set;}
        public int IsAvailable {get; set;}
        public int CategoryId {get; set;}
        [ForeignKey("CategoryId")]
        public virtual Category Category {get; set;}
    }
}
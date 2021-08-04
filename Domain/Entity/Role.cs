using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Role")]
    public class Role : BaseEntity
    {
        public Role()
        {
            Accounts = new HashSet<Account>();
        }
        [Key]
        public int Id {get; set;}
        [Required]
        public int Name {get; set;}
        public virtual ICollection<Account> Accounts {get; set;}
    }
}
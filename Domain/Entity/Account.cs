using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using net_design_pattern.Persistence.BaseEntity;

namespace net_design_pattern.Domain.Models
{
    [Table("Account")]
    public class Account : BaseEntity
    {
        public Account()
        {
            Profile = new Profile();
            AccountHasRoles = new HashSet<AccountHasRole>();
        }
        [Key]
        public int Id {get; set;}
        [Required][MaxLength(320)]
        public string Email {get; set;}
        [Required][MaxLength(50)]
        public string Password {get; set;}
        public virtual Profile Profile {get; set;}
        public virtual ICollection<AccountHasRole> AccountHasRoles { get; set; }
    }
}
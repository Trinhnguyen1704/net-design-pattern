using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace net_design_pattern.Domain.Models.Authorization
{
    public class RegisterModel
    {
        [Required]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
        [Required]
        public string FirstName {get; set;}
        [Required]
        public string LastName {get; set;}
    }
}
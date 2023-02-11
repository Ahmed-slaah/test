using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }


        public virtual ICollection<UserRole> UserRole { get; set; }

    }
}
using RepositoryPatternWithUOW.Core.Consts;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public class UserRole
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 0)]
        [ForeignKey("Roles")]
        public int RolesId { get; set; }

        public virtual User User { get; set; }
        public virtual Roles Roles { get; set; }

    }
}

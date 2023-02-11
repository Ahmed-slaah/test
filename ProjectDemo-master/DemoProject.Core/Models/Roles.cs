using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public class Roles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}

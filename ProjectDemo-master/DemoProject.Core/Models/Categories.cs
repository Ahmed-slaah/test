using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }

        public ICollection<Vacancies> Vacancies { get; set; }
    }
}

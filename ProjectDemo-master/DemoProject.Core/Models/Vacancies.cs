using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core.Models
{
    public class Vacancies
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public string Descriptions { get; set; }

        [Required]
        public string Resposibilities { get; set; }

        [Required]
        public string Skills { get; set; }

        [Required]
        public DateTime OpenedFrom { get; set; }

        [Required]
        public DateTime OpenedTo { get; set; }

        [Required]
        public int Max { get; set; }

        public bool IsRequestedToCancel { get; set; } = false;

        public int CategoryId { get; set; }

        public virtual Categories Categories { get; set; }
    }

   
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Models
{
    public class RequestTypes
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Requests> Requests { get; set; }
    }
}

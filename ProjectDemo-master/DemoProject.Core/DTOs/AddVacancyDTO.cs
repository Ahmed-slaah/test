using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.DTOs
{
    public class AddVacancyDTO
    {
        public int id { get; set; }
        public string Name { get; set; }

        
        public string Descriptions { get; set; }


        public string Resposibilities { get; set; }

        public string Skills { get; set; }

        public DateTime OpenedTo { get; set; }

        public int Max { get; set; }
        public int CategoryId { get; set; }

        public List<CategoryDTO> Categories { get; set; }
    }
}

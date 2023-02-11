using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.DTOs
{
    public class GetVacanciesDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
        public string Resposibilities { get; set; }
        public string Skills { get; set; }
        public DateTime OpenedFrom { get; set; }
        public DateTime OpenedTo { get; set; }
        public int Max { get; set; }
        public Categories Categories { get; set; }
    }

    public class GetVacanciesListDTO
    {
        public List<GetVacanciesDTO> VacanciesList { get; set; }
        public string userId { get; set; }
        public string roleId { get; set; }
        public int count { get; set; }
        public int pageSize { get; set; }
        public string search { get; set; }

    }
}

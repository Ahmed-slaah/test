using DemoProject.Core.Consts;
using DemoProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.DTOs
{
    public class AddUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public List<RolesDto> roles { get; set; }
    }


}

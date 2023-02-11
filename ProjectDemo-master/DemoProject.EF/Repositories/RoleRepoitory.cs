using DemoProject.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using RepositoryPatternWithUOW.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoProject.Core.Models;

namespace DemoProject.EF.Repositories
{
    public class RoleRepoitory : BaseRepository<Roles>, IRoleRepository
    {
        public RoleRepoitory(ApplicationDbContext context) : base(context)
        {
        }
    }
   
}

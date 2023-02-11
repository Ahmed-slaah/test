using DemoProject.Core.Models;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.Core.Interfaces
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
    }
}

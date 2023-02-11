using DemoProject.Core.IHelper;
using DemoProject.Core.Interfaces;
using DemoProject.Core.Models;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IUsersRepository User { get; }
        IVacanciesRepository Vacancies { get; }
        IUserRoleRepository UserRole { get; }
        IRoleRepository Roles { get; }
        IBaseRepository<Categories> Categories { get; }


        Task<int> Complete();
    }
}
using DemoProject.Core.IHelper;
using DemoProject.Core.Interfaces;
using DemoProject.Core.Models;
using DemoProject.EF.Helper;
using DemoProject.EF.Repositories;
using Microsoft.AspNetCore.Http;
using RepositoryPatternWithUOW.Core;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IUsersRepository User { get; private set; }
        public IVacanciesRepository Vacancies { get; private set; }
        public IUserRoleRepository UserRole { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IBaseRepository<Categories> Categories { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            User = new UsersRepository(_context);
            Vacancies = new VacanciesRepository(_context);
            UserRole = new UserRoleRepository(_context);
            Roles = new RoleRepoitory(_context);
            Categories = new BaseRepository<Categories>(_context);
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
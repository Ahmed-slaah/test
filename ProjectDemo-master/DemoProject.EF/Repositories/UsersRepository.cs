using DemoProject.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using RepositoryPatternWithUOW.EF;
using RepositoryPatternWithUOW.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoProject.EF.Repositories
{
    public class UsersRepository: BaseRepository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext context):base(context)
        {
        }
    }
}

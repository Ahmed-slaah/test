using RepositoryPatternWithUOW.Core.Interfaces;
using RepositoryPatternWithUOW.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryPatternWithUOW.EF.Repositories
{
    public class VacanciesRepository : BaseRepository<Vacancies>, IVacanciesRepository
    {
        public VacanciesRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<string> DeleteRequest(int id,string roleId)
        {
            var deletedOne =  _context.Vacancies.FirstOrDefault(v => v.Id == id);
            if(roleId != "1") //admin role
            {
                deletedOne.IsRequestedToCancel = true;
                return "pending";
            }
            else
            {
                _context.Vacancies.Remove(deletedOne);
                return "deleted";
            }
        }

    }
}
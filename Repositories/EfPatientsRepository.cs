using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfPatientsRepository : EfRepository<Patient>, IEfPatientsRepository<Patient>
    {
        private readonly DbContext _context;
        public EfPatientsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

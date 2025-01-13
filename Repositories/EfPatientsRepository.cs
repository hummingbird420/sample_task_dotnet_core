using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfPatientsRepository : EfRepository<Patients>, IEfPatientsRepository<Patients>
    {
        private readonly DbContext _context;
        public EfPatientsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

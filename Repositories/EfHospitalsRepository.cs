using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfHospitalsRepository : EfRepository<Hospitals>, IEfHospitalsRepository<Hospitals>
    {
        private readonly DbContext _context;
        public EfHospitalsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfDoctorsRepository : EfRepository<Doctors>, IEfDoctorsRepository<Doctors>
    {
        private readonly DbContext _context;
        public EfDoctorsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

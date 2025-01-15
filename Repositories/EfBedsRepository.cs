using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfBedsRepository : EfRepository<Bed>, IEfBedsRepository<Bed>
    {
        private readonly DbContext _context;
        public EfBedsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfBedsAlotementsRepository : EfRepository<BedsAlotements>, IEfBedsAlotementsRepository<BedsAlotements>
    {
        private readonly DbContext _context;
        public EfBedsAlotementsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

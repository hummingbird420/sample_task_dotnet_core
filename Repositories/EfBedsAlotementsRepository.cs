using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfBedsAlotementsRepository : EfRepository<BedsAlotement>, IEfBedsAlotementsRepository<BedsAlotement>
    {
        private readonly DbContext _context;
        public EfBedsAlotementsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

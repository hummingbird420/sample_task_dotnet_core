using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfNotificationsRepository : EfRepository<Notification>, IEfNotificationsRepository<Notification>
    {
        private readonly DbContext _context;
        public EfNotificationsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

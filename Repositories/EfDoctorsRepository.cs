﻿using Microsoft.EntityFrameworkCore;
using SampleTaskApp.IRepositories;
using SampleTaskApp.Models;

namespace SampleTaskApp.Repositories
{
    public class EfDoctorsRepository : EfRepository<Doctor>, IEfDoctorsRepository<Doctor>
    {
        private readonly DbContext _context;
        public EfDoctorsRepository(SampleTaskDbContext context) : base(context)
        {
            _context = context;
        }
    }
}

﻿using SampleTaskApp.IRepositories;
using System.Data;
using System.Data.Common;

namespace SampleTaskApp.Repositories
{
    public class DapperBedsAlotementsRepository : DapperRepository<BedsAlotement>, IDapperBedsAlotementsRepository<BedsAlotement>
    {
        private readonly IDbConnection _dbConnection;
        public DapperBedsAlotementsRepository(IDbConnection dbConnection) : base(dbConnection)
        {
            _dbConnection = dbConnection;
        }
    }
}

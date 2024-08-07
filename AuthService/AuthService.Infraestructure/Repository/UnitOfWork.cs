using AuthService.Domain.Repository;
using AuthService.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _dbContext;

        public UnitOfWork(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
        
    }
}

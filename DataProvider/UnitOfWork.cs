using System.Data.SqlClient;
using DataProvider.Repositories;

namespace OrganizationPanel
{
    public class UnitOfWork
    {
        private DbContext _dbContext;
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IOrganizationRepository GetOrganizationRepository()
        {
            return new OrganizationRepository(_dbContext.DateBaseConnection, _dbContext.QueryTimeOut);
        }
        
        public IEmployeeRepository GetEmployeeRepository()
        {
            return new EmployeeRepository(_dbContext.DateBaseConnection, _dbContext.QueryTimeOut);
        }
    }
}
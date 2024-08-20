using Demo3MVC.BLL.Interfaces;
using Demo3MVC.DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.BLL.Repositries
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly MVCProjectDbContext _dbcontext;

        public IEmployeeRepository EmployeeRepo { get; set; }
        public IDepartmentRepository DepartmentRepo { get; set; }

        public UnitOfWork(MVCProjectDbContext dbcontext) //Ask CLr To inject Object of MVCDbContext
        {
            EmployeeRepo = new EmployeeRepository(dbcontext); // Passing the object of mvcDbContext to make object from EmployeeRepository
            DepartmentRepo = new DepartmentRepository(dbcontext); // Passing the object of mvcDbContext to make object from DepartmentRepository
            _dbcontext = dbcontext;
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbcontext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbcontext.Dispose();
        }
    }
}

using Demo3MVC.BLL.Interfaces;
using Demo3MVC.DAL.Contexts;
using Demo3MVC.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.BLL.Repositries
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly MVCProjectDbContext _dbContext;
        public EmployeeRepository(MVCProjectDbContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }


        public IQueryable<Employee> GetempsByAddress(string address)
        => _dbContext.Employees.Where(E=>E.Address == address);
        public IQueryable<Employee> GetEmployeeByName(string searchvalue)
        => _dbContext.Employees.Where(E => E.Name.ToLower().Contains(searchvalue.ToLower())).Include(E=>E.Department);

    }
}

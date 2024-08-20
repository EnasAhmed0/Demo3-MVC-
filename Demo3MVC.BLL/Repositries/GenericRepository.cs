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
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MVCProjectDbContext _dbContext;

        public GenericRepository(MVCProjectDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task AddAsync(T item)
        {
            await _dbContext.AddAsync(item);
        }

        public void Delete(T item)
        {
            _dbContext.Remove(item);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        { // this is mosaken to show the department now but the correct solution is using "SpecificationDesignPattern"
            if(typeof(T) == typeof(Employee))
            {
                return (IEnumerable<T>) await _dbContext.Employees.Include(E=>E.Department).ToListAsync();
            }
           return await _dbContext.Set<T>().ToListAsync();

        }
        public async Task<T> GetByIdAsync(int id)
        => await _dbContext.Set<T>().FindAsync(id);

        public void Update(T item)
        {
            _dbContext.Update(item);
        }
    }
}

using Demo3MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.BLL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        // You Can Not Make The Database To Work Async must Alaways Work Sync
        IQueryable<Employee> GetempsByAddress(string address);
        IQueryable<Employee> GetEmployeeByName(string searchvalue);
    }
}

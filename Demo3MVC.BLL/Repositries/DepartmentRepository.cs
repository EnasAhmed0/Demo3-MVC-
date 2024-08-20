using Demo3MVC.BLL.Interfaces;
using Demo3MVC.DAL.Contexts;
using Demo3MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.BLL.Repositries
{
    public class DepartmentRepository :GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(MVCProjectDbContext dbContext):base(dbContext)
        {
            
        }
    }
}

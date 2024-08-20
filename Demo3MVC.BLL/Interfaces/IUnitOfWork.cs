using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        // Signature for property for each and Every Repository
        public IEmployeeRepository EmployeeRepo { get; set; }
        public IDepartmentRepository DepartmentRepo { get; set; }

        Task<int> CompleteAsync();

    }
}

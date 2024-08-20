using Demo3MVC.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.DAL.Contexts
{
    public class MVCProjectDbContext : IdentityDbContext<ApplicationUser>
    {

        public MVCProjectDbContext(DbContextOptions<MVCProjectDbContext> options):base(options)
        {
            
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //=> optionsBuilder.UseSqlServer("server = .; Database = MVCProject ; Trusted_Connection = true;");


        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}

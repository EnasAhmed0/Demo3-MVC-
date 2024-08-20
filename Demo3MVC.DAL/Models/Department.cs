using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo3MVC.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string code { get; set; }
        public DateTime DateOfCreation { get; set; }
        //Navigational property for many relationship
        [InverseProperty("Department")]

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}

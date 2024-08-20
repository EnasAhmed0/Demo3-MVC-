using Demo3MVC.DAL.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace Demo3MVC.PL.ViewModels
{
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name Is Required")]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Code Is Required")]
        public string code { get; set; }
        public DateTime DateOfCreation { get; set; }
        //Navigational property for many relationship
        [InverseProperty("Department")]

        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
    }
}

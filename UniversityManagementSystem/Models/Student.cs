using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public string RegNumber { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "The email address is not valid")]
        public string Email { get; set; }
        [Display(Name = "Contact No. ")]
        public string ContactNo { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
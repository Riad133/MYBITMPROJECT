using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UniversityManagementSystem.BAL;

namespace UniversityManagementSystem.Models
{
   public class Teacher
    {

        [Key]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "You Must Fill Name Field")]
        public string Name { get; set; }

        public string Address { get; set; }
        [Required(ErrorMessage = "You Must Fill Email Field")]
        [Index(IsUnique = true)]
        [MaxLength(32)]
        [Remote("DoesEmailExist", "Teacher", HttpMethod = "POST", ErrorMessage = "Email address exist")]

        public string Email { get; set; }
        public string ContactNo { get; set; }
         [DisplayName("Designation")]
        public int DesignationId { get; set; }

        public int DepartmentId { get; set; }
        [Required(ErrorMessage = "You Must Fill Course Credit Field")]
        [Range(0, 500, ErrorMessage = "Invalid")]
        public decimal Credit { get; set; }

        public virtual Department Department { get; set; }
        public virtual Designation Designation { get; set; }
       public virtual List<CourseAssign> CourseAssigns { get; set; }

    }
}

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
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        [MaxLength(32)]
        [Index(IsUnique = true)]
        [StringLength(30, ErrorMessage = "{0} must be at list {2}", MinimumLength = 5)]
        [Remote("DoesCodeExist", "Course", HttpMethod = "POST", ErrorMessage = "Code exist")]

        public string Code { get; set; }
        [Range(.5,5, ErrorMessage = "Credit must between  .5  to 5")]
        public double Credit { get; set; }
        [Index(IsUnique = true)]
        [MaxLength(32)]
        public string Name { get; set; }
        public string Description { get; set; }
        [DisplayName("Department")]
        public int DepartmentId { get; set; }
        [DisplayName("Semester")]
        [Range(1, 8, ErrorMessage = "Semester must be 1-8")]
        public int SemesterId { get; set; }

        public virtual Department Department { set; get; }
        public virtual Semester Semester { set; get; }
        public virtual List<CourseAssign> CourseAssigns { get; set; }
    }
}

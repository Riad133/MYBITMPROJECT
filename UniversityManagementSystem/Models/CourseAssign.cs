using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    public class CourseAssign
    {
        public int CourseAssignId { get; set; }
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public int TeacherId { get; set; }
        public virtual Teacher Teacher { get; set; }
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }


    }
}
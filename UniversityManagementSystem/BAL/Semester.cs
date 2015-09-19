using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniversityManagementSystem.BAL
{
    public class Semester
    {
        [Key]
        public int SemesterId { get; set; }
        [Range(1,8,ErrorMessage = "Must be 1-8")]
        [DisplayName ("Semester")]
        public int SemesterNO { get; set; }
    }
}

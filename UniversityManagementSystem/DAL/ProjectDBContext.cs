using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystem.BAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.DAL
{
    class ProjectDBContext:DbContext
    {

        public ProjectDBContext() : base("universityDBContext")
        {
            
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
        public DbSet<Department> Department { set; get; }
        public DbSet<Course> Course { set; get; }
        public DbSet<Teacher> Teacher { set; get; }
        public DbSet<Semester> Semester { set; get; }
        public DbSet<Designation> Designation { set; get; }
        public DbSet<CourseAssign> CourseAssigns { set; get; }
        public DbSet<Student>  Students { get; set; }

    }
}

using System.Collections.Generic;
using System.Data.SqlTypes;
using UniversityManagementSystem.BAL;

namespace UniversityManagementSystem.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<UniversityManagementSystem.DAL.ProjectDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "UniversityManagementSystem.DAL.ProjectDBContext";
        }

        protected override void Seed(UniversityManagementSystem.DAL.ProjectDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var Semester = new List<Semester>
            {
                new Semester {SemesterNO = "1"},
                new Semester {SemesterNO = "2"},
                new Semester {SemesterNO = "3"},
                new Semester {SemesterNO = "4"},
                new Semester {SemesterNO = "5"},
                new Semester {SemesterNO = "6"},
                new Semester {SemesterNO = "7"},
                new Semester {SemesterNO = "8"}

            };

            Semester.ForEach(s => context.Semester.AddOrUpdate(p => p.SemesterNO, s));
            context.SaveChanges();


            var Desination = new List<Designation>
            {
                new Designation{ DesignationName = "Lecturer"},
                
                new Designation{ DesignationName = "Assistant professor"},
                                
                new Designation{ DesignationName = "Associate professor"},
                
                new Designation{ DesignationName = "Professor"}


            };

            Desination.ForEach(s => context.Designation.AddOrUpdate(p => p.DesignationName, s));
            context.SaveChanges();

        }
    }
}

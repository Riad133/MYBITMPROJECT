using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UniversityManagementSystem.DAL;
using UniversityManagementSystem.Models;

namespace UniversityManagementSystem.Controllers
{
    public class CourseAssignsController : Controller
    {
        private ProjectDBContext db = new ProjectDBContext();

        // GET: CourseAssigns
        public ActionResult Index()
        {
            var courseAssigns = db.CourseAssigns.Include(c => c.Course).Include(c => c.Department).Include(c => c.Teacher);
            return View(courseAssigns.ToList());
        }

        // GET: CourseAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // GET: CourseAssigns/Create
        public ActionResult Create()
        {
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Code");
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentCode");
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "Name");
            return View();
        }

        // POST: CourseAssigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.CourseAssigns.Add(courseAssign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentCode", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentCode", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // POST: CourseAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseAssignId,DepartmentId,TeacherId,CourseId")] CourseAssign courseAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Code", courseAssign.CourseId);
            ViewBag.DepartmentId = new SelectList(db.Department, "DepartmentId", "DepartmentCode", courseAssign.DepartmentId);
            ViewBag.TeacherId = new SelectList(db.Teacher, "TeacherId", "Name", courseAssign.TeacherId);
            return View(courseAssign);
        }

        // GET: CourseAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            if (courseAssign == null)
            {
                return HttpNotFound();
            }
            return View(courseAssign);
        }

        // POST: CourseAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseAssign courseAssign = db.CourseAssigns.Find(id);
            db.CourseAssigns.Remove(courseAssign);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [HttpPost]
        public string DepartmentChosse(string depData)
        {
            Debug.WriteLine(depData);
            int x = Convert.ToInt32(depData);
           // ViewBag.CourseId = new SelectList(db.Course, "CourseId", "Code");
            var data = from d in db.Teacher
                where d.DepartmentId == x
                select d;
            StringBuilder sbBuilder=new StringBuilder();
            sbBuilder.Append("<option value=\"\">" + "" + "</option>");
            foreach (var d in data)
            {
                sbBuilder.Append("<option value=\""+d.TeacherId+"\">"+d.Name+"</option>");
            }
            sbBuilder.Append("\n");
            var depSub = from c in db.Course
                         where c.DepartmentId == x
                         select c;
            var CAssignSub = from c in db.CourseAssigns
                             where c.DepartmentId == x
                             select c;
            List<CourseView> list=new List<CourseView>();
            foreach (var d in depSub)
            {
                bool b = true;
                foreach (var ca in CAssignSub)
                {
                    if (d.CourseId == ca.CourseId)
                    {
                        b = false;
                        break;
                    }
                }
                if (b)
                {
                    list.Add(new CourseView(d.CourseId,d.Name));
                }
            }
            sbBuilder.Append("<option value=\"\">" + "" + "</option>");
            foreach (var d in list)
            {
                sbBuilder.Append("<option value=\"" + d.courseId + "\">" + d.courseName + "</option>");
            }
            Debug.WriteLine(sbBuilder.ToString());
            return sbBuilder.ToString();
        }
        [HttpPost]
        public string TeacherChosse(string teacherData)
        {
            Debug.WriteLine(teacherData);
            int x = Convert.ToInt32(teacherData);
            var AllCredit = from d in db.Teacher
                where d.TeacherId == x
                select d;
            var AssignCourseCredit = from d in db.CourseAssigns
                                     where d.TeacherId == x
                                     select d;
            double sum = 0;
            foreach (var ACC in AssignCourseCredit)
            {
                var CinOneSub = from d in db.Course
                    where d.CourseId == ACC.CourseId
                    select d;

                foreach (var OneSub in CinOneSub)
                {
                    sum +=  OneSub.Credit;
                }
            }
            decimal totalCredit = AllCredit.First().Credit - (decimal) sum;
            StringBuilder sbBuilder = new StringBuilder();
            sbBuilder.Append(AllCredit.First().Credit);
            sbBuilder.Append("\n");
            sbBuilder.Append(totalCredit);
            Debug.WriteLine(sbBuilder.ToString());
            return sbBuilder.ToString();
        }

        [HttpPost]
        public string CourseChosse(string teacherData)
        {
            Debug.WriteLine("hello data :" + teacherData);
            int x = Convert.ToInt32(teacherData);
            var data = from d in db.Course
                where d.CourseId == x 
                           select d;
            StringBuilder sbBuilder = new StringBuilder();
            foreach (var d in data)
            {
                sbBuilder.Append(d.Name);
                sbBuilder.Append("\n");
                sbBuilder.Append(d.Credit);

            }
            Debug.WriteLine(sbBuilder.ToString());
            return sbBuilder.ToString();
        }
        [HttpPost]
        public string SearchByDepartment(string id)
        {
            if (id == null)
            {
                id = "-1";
            }
            int DepId = Convert.ToInt32(id);
            var data = from d in db.CourseAssigns
                       where d.DepartmentId == DepId
                       select new
                       {
                           CourseID = d.Course.Code,
                           CourseName = d.Course.Name,
                           CourseSemester = d.Course.Semester.SemesterNO,
                           CourseTeacher = d.Teacher.Name
                       };
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<th>Code</th>");
            sb.Append("<th>Name/Title</th>");
            sb.Append("<th>Semester</th>");
            sb.Append("<th>Assigned To</th>");
            sb.Append("</tr>");


            foreach (var d in data)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + d.CourseID + "</td>");
                sb.Append("<td>" + d.CourseName + "</td>");
                sb.Append("<td>" + d.CourseSemester + "</td>");
                sb.Append("<td>" + d.CourseTeacher + "</td>");
                sb.Append("</tr>");
            }
            return sb.ToString();
        }
        public ActionResult SearchByDepartmentCourse()
        {
            string id=null;
            if (id == null)
            {
                id = "-1";
            }
            int DepId = Convert.ToInt32(id);
            var data = from d in db.CourseAssigns
                       where d.DepartmentId == DepId
                       select new
                       {
                           CourseID = d.Course.Code,
                           CourseName = d.Course.Name,
                           CourseSemester = d.Course.Semester.SemesterNO,
                           CourseTeacher = d.Teacher.Name
                       };
            StringBuilder sb = new StringBuilder();
            sb.Append("<tr>");
            sb.Append("<th>Code</th>");
            sb.Append("<th>Name/Title</th>");
            sb.Append("<th>Semester</th>");
            sb.Append("<th>Assigned To</th>");
            sb.Append("</tr>");


            foreach (var d in data)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + d.CourseID + "</td>");
                sb.Append("<td>" + d.CourseName + "</td>");
                sb.Append("<td>" + d.CourseSemester + "</td>");
                sb.Append("<td>" + d.CourseTeacher + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            ViewBag.table = sb.ToString();


            StringBuilder sbDep=new StringBuilder();

            var dep = from d in db.Department
                select d;
            sbDep.Append("<option value=\"\"></option>");
            foreach (var d in dep)
            {
                sbDep.Append("<option value=\""+d.DepartmentId+"\">"+d.DepartmentName+"</option>");
            }
            ViewBag.dep = sbDep.ToString();
            return View();
        }
    }

    class CourseView
    {
        public int courseId;
        public string courseName;

        public CourseView(int courseId, string courseName)
        {
            this.courseId = courseId;
            this.courseName = courseName;
        }
    }
}

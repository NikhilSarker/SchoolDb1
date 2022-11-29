using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolDb1.Models;


namespace SchoolDb1.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult List( string SearchKey = null)
        {

            //GET:  /teacher/list
            Debug.WriteLine("The user is trying to search for "+SearchKey);


            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers =  Controller.ListTeachers(SearchKey);

            Debug.WriteLine("I have accessed " + Teachers.Count());
            
            return View(Teachers);
        }


        //GET : teacher/show/{id}
        public ActionResult Show( int id)
        {
            

           TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);

        }
        //GET:  /teacher/deleteconfirm/{id}
        public ActionResult DeleteConfirm(int id)
        {


            TeacherDataController Controller = new TeacherDataController();
            Teacher NewTeacher = Controller.FindTeacher(id);

            return View(NewTeacher);

        }
        //POST:  /teacher/delete/{id}

        public ActionResult Delete(int id)
        {

            TeacherDataController Controller = new TeacherDataController();
            Controller.DeleteTeacher(id);
            return RedirectToAction("List");
        }

        //GET:  teacher/new/

        public ActionResult New()
        {

            return View();
        }

        //POST:  teacher/create/

        [HttpPost]
        public ActionResult Create(string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {



            Debug.WriteLine("I have access the create method");
            Debug.WriteLine(TeacherFname);
            Debug.WriteLine(TeacherLname);

            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFname = TeacherFname;
            NewTeacher.TeacherLname = TeacherLname;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.HireDate = HireDate;
            NewTeacher.Salary = Salary;
            TeacherDataController Controller = new TeacherDataController();
            Controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }
    }
}
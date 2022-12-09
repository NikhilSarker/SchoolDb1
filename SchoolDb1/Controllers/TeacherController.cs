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
            Teacher SelectedTeacher = Controller.FindTeacher(id);

            return View(SelectedTeacher);

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



        /// <summary>
        /// Routes to a dynamically generated "Teacher Update Page" 
        /// Gathers information frm database.
        /// </summary>
        /// <param name="id">ID of the Teacher</param>
        /// <returns>
        /// A dynamic "Update teachet" webpage which provide the current information
        /// of the teacher and ask the user for new information as part of a form.
        /// </returns>
        /// <example> GET:  /Teacher/Update/{id}  </example> 
        public ActionResult Update(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);


            return View(SelectedTeacher);
        }


        /// <summary>
        /// Receive a POST request containg information about an existing teacher
        /// in the system, with new values. Conveys this informaton to the API
        /// and Redirects to the "Teacher Show" page of our update teacher.
        /// </summary>
        /// <param name="id">ID of the Teacher to update</param>
        /// <param name="TeacherFname">The updated first name of the teacher</param>
        /// <param name="TeacherLname">The updated last name of the teacher</param>
        /// <param name="EmployeeNumber">The updated employee number of the teacher</param>
        /// <param name="HireDate">The updated hire date of the teacher</param>
        /// <param name="Salary">The updated salary of the teacher</param>
        /// <returns>
        /// A dynamic webpage which provide the current information of the teacher
        /// </returns>
        /// <example> POST:  /Teacher/Update/{id} </example>
        [HttpPost]
        public ActionResult Update(int id, string TeacherFname, string TeacherLname, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {

            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFname = TeacherFname;
            TeacherInfo.TeacherLname = TeacherLname;
            TeacherInfo.EmployeeNumber = EmployeeNumber;
            TeacherInfo.HireDate = HireDate;
            TeacherInfo.Salary = Salary;
            TeacherDataController Controller = new TeacherDataController();
            Controller.UpdateTeacher(id, TeacherInfo);

            return RedirectToAction("Show/"+id);

        }


    }

}
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



    }
}
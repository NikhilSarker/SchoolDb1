using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SchoolDb1.Models;
using System.Diagnostics;


namespace SchoolDb1.Controllers
{
    public class TeacherDataController : ApiController
    {
        // The database context class which allows us to access our MySQL Database.
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller Will access the authors table of our blog database.
        /// <summary>
        /// Returns a list of Authors in the system
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers/{SearchKey}</example>
        /// <returns>
        /// A list of authors (first names and last names)
        /// </returns>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey?}")]
        public IEnumerable<Teacher> ListTeachers(string SearchKey = null)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            Debug.WriteLine("The search key is"+SearchKey);
            string query = "Select * from Teachers where lower(teacherfname) like lower(@key) " +
                "or lower(teacherlname) like lower(@key) or " +
                "lower(concat(teacherfname, ' ', teacherlname)) like lower(@key) " ;
            //"hiredate like (@key) or salary like(@key)"
            Debug.WriteLine("The query is" +query);
            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = query;

            cmd.Parameters.AddWithValue("@key", "%" + SearchKey + "%");
            cmd.Prepare();


            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            //Create an empty list of Teachers
            List<Teacher> Teachers = new List<Teacher> { };

            //Loop Through Each Row the Result Set
            while (ResultSet.Read())
            {

                



                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
               string EmployeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime) ResultSet["hiredate"];               
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

                //Add the Author Name to the List
                Teachers.Add(NewTeacher);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();

            //Return the final list of author names
            return Teachers;
        }

        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        // GET: /api/TeacherData/FindTeacher/{id}
        public Teacher FindTeacher(int id)

        {

            Teacher NewTeacher = new Teacher();

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Select * from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            //Gather Result Set of Query into a variable
            MySqlDataReader ResultSet = cmd.ExecuteReader();



            while (ResultSet.Read())
            {
                //Access Column information by the DB column name as an index
                int TeacherId = (int)ResultSet["teacherid"];
                string TeacherFname = (string)ResultSet["teacherfname"];
                string TeacherLname = (string)ResultSet["teacherlname"];
                string EmploeeNumber = (string)ResultSet["employeenumber"];
                DateTime HireDate = (DateTime)ResultSet["hiredate"];
                decimal Salary = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = TeacherId;
                NewTeacher.TeacherFname = TeacherFname;
                NewTeacher.TeacherLname = TeacherLname;
                NewTeacher.EmployeeNumber = EmploeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = Salary;

            }



          return NewTeacher;

        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// /// POST: api/teacherdata/deleteteacher/3
        [HttpPost]

        public void DeleteTeacher(int id)
        {

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Delete from Teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "Insert into Teachers (teacherfname, teacherlname, employeenumber, hiredate, salary) value(@TeacherFname, @TeacherLname, @EmployeeNumber, @HireDate, @Salary)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", NewTeacher.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", NewTeacher.HireDate);
            cmd.Parameters.AddWithValue("@Salary", NewTeacher.Salary);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();

           
            


        }


        public void UpdateTeacher(int id, [FromBody]Teacher TeacherInfo)
        {

            //Create an instance of a connection
            MySqlConnection Conn = School.AccessDatabase();

            //Open the connection between the web server and database
            Conn.Open();

            //Establish a new command (query) for our database
            MySqlCommand cmd = Conn.CreateCommand();

            //SQL QUERY
            cmd.CommandText = "update teachers set teacherfname=@TeacherFname, teacherlname=@TeacherLname, employeenumber=@EmployeeNumber, hiredate=@HireDate, salary=@Salary where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFname);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLname);
            cmd.Parameters.AddWithValue("@EmployeeNumber", TeacherInfo.EmployeeNumber);
            cmd.Parameters.AddWithValue("@HireDate", TeacherInfo.HireDate);
            cmd.Parameters.AddWithValue("@Salary", TeacherInfo.Salary);
            cmd.Parameters.AddWithValue("@TeacherId",id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();

        }




    }
}

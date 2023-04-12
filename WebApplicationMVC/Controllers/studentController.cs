using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class studentController : Controller
    {
        public IActionResult Student()
        {
            return View();
        }

        public List<studentModel> students;
        public string info = "Hello student info";
        public IActionResult Index()
        {
            students = new List<studentModel>();
            string connectionString = "Data Source=5CG9441HWP;Initial Catalog=CollegeSportsManagementSystem; Integrated Security = True; Encrypt=False";

            SqlConnection conn = new SqlConnection(connectionString);

            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM PLAYERS";

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                studentModel s1 = new studentModel();


                s1.studentId = (int)reader["playerId"];
                s1.studentName = (string)reader["playerName"];
                s1.collegeId = (int)reader["collegeId"];

                students.Add(s1);
            }

            ViewBag.students = students;
            ViewBag.info = info;


            return View();
        }
    }
}

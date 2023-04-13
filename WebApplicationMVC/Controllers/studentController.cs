using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplicationMVC.Models;

namespace WebApplicationMVC.Controllers
{
    public class studentController : Controller
    {
        IConfiguration configuration;

        public studentController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsEnvironment("Development"))
            {
                // code to be executed in development environment 

            }
        }

        public IActionResult Student()
        {
            return View();
        }

        public List<studentModel> students;
        public string info = "Hello student info";
        public IActionResult Index()
        {
            students = new List<studentModel>();
            string connectionString = configuration.GetConnectionString("CollegeSportsManagementSystem");

            SqlConnection conn = new SqlConnection();

            conn.ConnectionString = connectionString;

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

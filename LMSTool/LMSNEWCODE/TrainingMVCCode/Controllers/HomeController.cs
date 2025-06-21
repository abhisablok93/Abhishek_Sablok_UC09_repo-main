using LMSTraining.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace LMSTraining.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult LMS(string domainId)
        {
            String connectionString = "Data Source = PRE08920436035B; Initial Catalog = LMS; Integrated Security = True";
            String sql = "SELECT * FROM trainingAssigned where Name='"+User.Identity.Name.ToString()+"'";
           

            var model = new List<TrainingDTO>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var trainingDTO = new TrainingDTO();
                    trainingDTO.Id = rdr["Id"].ToString();
                    trainingDTO.Name = rdr["Name"].ToString();
                    trainingDTO.CourseId = rdr["CourseId"].ToString();
                    trainingDTO.CourseName = rdr["CourseName"].ToString();
                    trainingDTO.Category = rdr["Category"].ToString();
                    trainingDTO.Skill = rdr["Skill"].ToString();
                    trainingDTO.Level = rdr["Level"].ToString();
                    trainingDTO.Duration = rdr["Duration"].ToString();
                    model.Add(trainingDTO);
                }

            }

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Diagnostics.Metrics;
using WebApplicationWithMVC.Models;

namespace WebApplicationWithMVC.Controllers
{
    public class MemberController : Controller
    {
        IConfiguration configuration;
        SqlConnection SqlConnection;
        public MemberController(IConfiguration configuration)
        {
            this.configuration = configuration;
            SqlConnection = new SqlConnection(configuration.GetConnectionString("LibraryDb"));
        }
        

        // GET: MemberController
        public ActionResult Index()
        {
            return View(GetMembersList());
        }

        // GET: MemberController/Details/5
        public ActionResult Details(string MemberID)
        {
            return View(GetMember(MemberID));
        }

        // GET: MemberController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MemberController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberModel member)
        {
            try
            {
                Console.WriteLine(member.MEMBER_NAME);
                InsertMembers(member);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Edit/5
        public ActionResult Edit(string memberID)
        {
            return View(GetMember(memberID));
        }

        // POST: MemberController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string memberID, MemberModel memberModel)
        {
            try
            {
                UpdateMember(memberID, memberModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MemberController/Delete/5
        public ActionResult Delete(string memberID)
        {
            return View(GetMember(memberID));
        }

        // POST: MemberController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string memberID, MemberModel memberModel)
        {
            try
            {
                DeleteMember(memberID,memberModel);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public MemberModel GetMember(string MemberId)
        {
            Console.WriteLine("Member " + MemberId);

            SqlConnection.Open();
            SqlCommand cmd = new SqlCommand("GET_MEMBER", SqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@memberId", MemberId);
            
            SqlDataReader reader = cmd.ExecuteReader();
            MemberModel member = new MemberModel();

            while (reader.Read())
            {
                member.MEMBER_ID = (string)reader["MEMBER_ID"];
                member.MEMBER_NAME = (string)reader["MEMBER_NAME"];
                member.CITY = (string)reader["CITY"];
                member.DATE_REGISTER = (DateTime)reader["DATE_REGISTER"];
                member.DATE_EXPIRE = (DateTime)reader["DATE_EXPIRE"];
                member.MEMBERSHIP_STATUS = (string)reader["MEMBERSHIP_STATUS"];

            }

            reader.Close();
            SqlConnection.Close();

            return member;
        }

        public List<MemberModel> GetMembersList()
        {
            List<MemberModel> membersList = new List<MemberModel>();
            SqlConnection.Open();
            SqlCommand cmd = new SqlCommand("FETCH_MEMBERS", SqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            SqlDataReader reader = cmd.ExecuteReader();
            

            while (reader.Read())
            {
                MemberModel member = new MemberModel();

                member.MEMBER_ID = (string)reader["MEMBER_ID"];
                member.MEMBER_NAME = (string)reader["MEMBER_NAME"];
                member.CITY = (string)reader["CITY"];
                member.DATE_REGISTER = (DateTime)reader["DATE_REGISTER"];
                member.DATE_EXPIRE = (DateTime)reader["DATE_EXPIRE"];
                member.MEMBERSHIP_STATUS = (string)reader["MEMBERSHIP_STATUS"];

                membersList.Add(member);
            }

            reader.Close();
            SqlConnection.Close();

            return membersList;

        }

        void InsertMembers(MemberModel member)
        {
            List<MemberModel> StudentList = new List<MemberModel>();
            SqlConnection.Open();
            SqlCommand cmd = new SqlCommand("ADD_MEMBER", SqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@memberId", member.MEMBER_ID);
            cmd.Parameters.AddWithValue("@memberName", member.MEMBER_NAME);
            cmd.Parameters.AddWithValue("@city", member.CITY);
            cmd.Parameters.AddWithValue("@membershipStatus", member.MEMBERSHIP_STATUS);

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();
        }

        void UpdateMember(string MemberId, MemberModel member)
        {
            
            SqlConnection.Open();
            SqlCommand cmd = new SqlCommand("EDIT_MEMBER", SqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            Console.WriteLine(member.DATE_REGISTER+" " +MemberId+ " hry");

            cmd.Parameters.AddWithValue("@memberName", member.MEMBER_NAME);
            cmd.Parameters.AddWithValue("@city", member.CITY);
            cmd.Parameters.AddWithValue("@membershipStatus", member.MEMBERSHIP_STATUS);
            cmd.Parameters.AddWithValue("@memberId", MemberId);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();
        }

        void DeleteMember(string MemberId, MemberModel member)
        {
            SqlConnection.Open();
            SqlCommand cmd = new SqlCommand("DELETE_MEMBER", SqlConnection);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            
            cmd.Parameters.AddWithValue("@memberId", MemberId);


            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection.Close();
        }
    }
}

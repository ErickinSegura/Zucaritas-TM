using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorialController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");

        [HttpGet(Name = "GetTutorialByUserID")]
        public int GetTutorialByUserID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_tutorial";
            cmd.Parameters.AddWithValue("@id_user", id);
            cmd.Connection = conn;

            MySqlDataReader rdr = cmd.ExecuteReader();

            int tutorial = 0;
            if (rdr.Read())
            {
                tutorial = rdr.GetInt32("tutorial_visto");
            }
            conn.Close();
            return tutorial;
        }

        [HttpPost(Name = "AddTutorial")]
        public IActionResult AddTutorial(int idUser)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "add_tutorial";
            cmd.Parameters.AddWithValue("@userID", idUser);
            cmd.Parameters.AddWithValue("@tutorial_visto", 1);
            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok();
        }

    }
}

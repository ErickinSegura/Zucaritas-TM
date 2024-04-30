using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;



namespace AWAQApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoreController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");


        [HttpPost(Name = "AddScore")]
        public IActionResult AddScore(int idUser, int score, int muestreo)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "add_score";
            cmd.Parameters.AddWithValue("@userID", idUser);
            cmd.Parameters.AddWithValue("@puntaje", score);
            cmd.Parameters.AddWithValue("@muestreoID", muestreo);
            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok();
        }

       
        
    }
}

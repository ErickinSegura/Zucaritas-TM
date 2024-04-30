using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");

        

        [HttpGet(Name = "GetIDByCredentials")]
        public int GetIDByCredentials(string username, string password)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_id";
            cmd.Parameters.AddWithValue("@nombreUsuario", username);
            cmd.Parameters.AddWithValue("@passcode", password);
            cmd.Connection = conn;

            MySqlDataReader rdr = cmd.ExecuteReader();

            int id = -1;
            if (rdr.Read())
            {
                id = rdr.GetInt32("ID_User");
            }
            conn.Close();
            return id;
        }




    }
}

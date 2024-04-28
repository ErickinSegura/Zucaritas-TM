using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQApi.Controllers
{

    public class espe
    {
        public int Muestreo { get; set; }
        public string Nombre { get; set; }
        public string URL { get; set; }
        public int Rareza { get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class RegistroEspecieController : ControllerBase
    {
        string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");

        [HttpGet(Name = "GetRegistroEspecieByUserID")]
        public IEnumerable<espe> GetRegistroEspecieByUserID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_registro_by_userid";
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.Connection = conn;

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<espe> registroEspecies = new List<espe>();
            while (rdr.Read())
            {
                espe registroEspecie = new espe();
                registroEspecie.Muestreo = rdr.GetInt32("Muestreo");
                registroEspecie.Nombre = rdr.GetString("Nombre");
                registroEspecie.URL = rdr.GetString("URL");
                registroEspecie.Rareza = rdr.GetInt32("Rareza");
                registroEspecies.Add(registroEspecie);
            }
            conn.Close();
            return registroEspecies;
        }

        [HttpPost(Name = "AddRegistroEspecie")]
        public IActionResult AddRegistroEspecie(int idUser, int idEspecie, int idMuestreo )
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "add_register";
            cmd.Parameters.AddWithValue("@userID", idUser);
            cmd.Parameters.AddWithValue("@especieID", idEspecie);
            cmd.Parameters.AddWithValue("@muestreoID", idMuestreo);
            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok();
        }

        



    }
        
}

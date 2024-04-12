using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistroEspecieController : ControllerBase
    {
        public string connectionString = "Server=127.0.0.1;Port=3306;Database=awaq;Uid=root;password=password;";

        [HttpGet(Name = "GetRegistroEspecieByUserID")]
        public IEnumerable<RegistroEspecie> GetRegistroEspecieByUserID(int id)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_registro_by_userid";
            cmd.Parameters.AddWithValue("@userID", id);
            cmd.Connection = conn;

            MySqlDataReader rdr = cmd.ExecuteReader();

            List<RegistroEspecie> registroEspecies = new List<RegistroEspecie>();
            while (rdr.Read())
            {
                RegistroEspecie registroEspecie = new RegistroEspecie();
                registroEspecie.ID_Registro = rdr.GetInt32("ID_Registro");
                registroEspecie.ID_Especie = rdr.GetInt32("ID_Especie");
                registroEspecie.ID_Usuario = rdr.GetInt32("ID_User");
                registroEspecie.ID_Muestreo = rdr.GetInt32("ID_Muestreo");
                registroEspecie.Fecha = rdr.GetDateTime("fecha");
                registroEspecies.Add(registroEspecie);
            }
            conn.Close();
            return registroEspecies;
        }

        [HttpPost(Name = "AddRegistroEspecie")]
        public IActionResult AddRegistroEspecie([FromBody] RegistroEspecie registroEspecie)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "add_register";
            cmd.Parameters.AddWithValue("@especieID", registroEspecie.ID_Especie);
            cmd.Parameters.AddWithValue("@userID", registroEspecie.ID_Usuario);
            cmd.Parameters.AddWithValue("@muestreoID", registroEspecie.ID_Muestreo);
            cmd.Connection = conn;

            cmd.ExecuteNonQuery();
            conn.Close();
            return Ok();
        }
    }
        
}

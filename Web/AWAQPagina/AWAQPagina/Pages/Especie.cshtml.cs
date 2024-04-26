using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace AWAQPagina.Pages
{
	public class EspecieModel : PageModel
    {
        public string? IdEspecie { set; get; }

        public Especie? especie { set; get; }

        public void OnGet()
        {
            especie = new Especie();
            string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_especieInfo";
                cmd.Connection = conexion;

                cmd.Parameters.AddWithValue("@especieID", 1);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        especie.nombre_Especie = reader["nombre_Especie"].ToString();
                        especie.img_web = reader["img_web"].ToString();
                        especie.descripcion = reader["descripcion"].ToString();
                        especie.nombre_cientifico = reader["nombre_cientifico"].ToString();
                    }
                }
            }
        }

        public IActionResult OnPost()
        {
            especie = new Especie();

            string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_especieInfo";
                cmd.Connection = conexion;

                cmd.Parameters.AddWithValue("@especieID", especie.ID_Especie);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        especie.nombre_Especie = reader["nombre_Especie"].ToString();
                        especie.img_web = reader["img_web"].ToString();
                        especie.descripcion = reader["descripcion"].ToString();
                        especie.nombre_cientifico = reader["nombre_cientifico"].ToString();
                    }
                }
                conexion.Close();
                return Page();
            }
        }
    }
}

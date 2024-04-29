using System.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace AWAQPagina.Pages
{
    public class ListaEspeciesModel : PageModel
    {
        public List<Especie>? listaEspecies { set; get; }

        public void OnGet()
        {
            string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
            MySqlConnection conexion = new MySqlConnection(connectionString);

            conexion.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "get_especie";
            cmd.Connection = conexion;

            listaEspecies = new List<Especie>();

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Especie ep = new Especie();
                    ep.nombre_Especie = Convert.ToString(reader["nombre_Especie"]);
                    ep.img_web = Convert.ToString(reader["img_web"]);
                    ep.nombre_cientifico = Convert.ToString(reader["nombre_cientifico"]);
                    listaEspecies.Add(ep);
                }
            }
            conexion.Close();
        }
    }
}

   
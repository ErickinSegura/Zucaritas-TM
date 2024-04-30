using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class studentViewModel : PageModel
    {

        public Usuario usuario { get; set; }
        public List<Muestreo> listaCantidad;
        public Medallas medalla { get; set; }
        public string? dashboardLink { get; set; }

        public studentViewModel()
        {
            usuario = new Usuario();

        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Role") != "Student")
            {
                if (HttpContext.Session.GetString("Role") != "Admin")
                {
                    return RedirectToPage("Index");
                }

                else
                {
                    return RedirectToPage("/adminView");
                }

            }

            else
            {
                string? userID = HttpContext.Request.Cookies["ID_USER"];
                string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");
                MySqlConnection conexion = new MySqlConnection(connectionString);

                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "get_user_info";
                cmd.Connection = conexion;

                cmd.Parameters.AddWithValue("@userID", userID);
                listaCantidad = new List<Muestreo>();

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario.name = reader["Nombre"].ToString();
                        usuario.firstLastname = reader["APELLIDO_PATERNO"].ToString();
                        usuario.userName = reader["Usuario"].ToString();
                        usuario.profilePicture = reader["Imagen_USUARIO"].ToString().Substring(1);


                    }
                }

                conexion.Close();

                conexion.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "medalla";
                cmd.Connection = conexion;
                cmd.Parameters.AddWithValue("@id_user", userID);

                medalla = new Medallas();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Muestreo me = new Muestreo();
                        me.cantidad = Convert.ToInt32(reader["Cantidad"]);
                        listaCantidad.Add(me);
                    }
                    foreach (var me in listaCantidad)
                    {
                        if (me.cantidad >= 5)
                        {
                            medalla.bronceCount++;
                        }
                        if (me.cantidad > 5 && me.cantidad <= 9)
                        {
                            medalla.plataCount++;
                        }
                        if (me.cantidad > 9)
                        {
                            medalla.oroCount++;
                        }
                    }
                }

                dashboardLink = String.Format("https://lookerstudio.google.com/embed/reporting/ed3ae5e0-9da6-401f-8fbb-f4fd23a6d451/page/E3ZwD?params=%7B%22ds21.iduser%22%3A{0}%2C%22ds5.iduserbar%22%3A{0}%7D", userID);

                conexion.Close();
                return Page();
            }

        }
    }
}
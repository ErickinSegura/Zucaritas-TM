using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using System.Data;

namespace AWAQPagina.Pages
{
    public class UserInfoModel : PageModel
    {

        [BindProperty]
        public Usuario usuario { get; set; }

        public UserInfoModel()
        {
            usuario = new Usuario();
        }

        public IActionResult OnGet()
        {
            if(HttpContext.Session.GetString("Role") != null)
            {
                string? userID = HttpContext.Request.Cookies["ID_USER"];
                string connectionString = System.IO.File.ReadAllText(".connectionstring.txt");

                using (MySqlConnection conexion = new MySqlConnection(connectionString))
                {
                    conexion.Open();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "get_user_bio";
                    cmd.Connection = conexion;

                    cmd.Parameters.AddWithValue("@userID", userID);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("BIO")))
                            {
                                usuario.bio = reader["BIO"].ToString();
                            }
                            else
                            {
                                usuario.bio = "¡Crea tu biografía!";
                            }

                            if (!reader.IsDBNull(reader.GetOrdinal("Imagen_USUARIO")))
                            {
                                usuario.profilePicture = reader["Imagen_USUARIO"].ToString();

                                if (usuario.profilePicture.Length > 1)
                                {
                                    usuario.profilePicture = usuario.profilePicture.Substring(1);
                                }
                                else
                                {
                                    usuario.profilePicture = "~/images/falseprofilepic.jpg";
                                    usuario.profilePicture = usuario.profilePicture.Substring(1);
                                }
                            }
                            else
                            {
                                usuario.profilePicture = "~/images/falseprofilepic.jpg";
                                usuario.profilePicture = usuario.profilePicture.Substring(1);
                            }
                        }
                    }

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "get_isAdmin";
                    cmd.Connection = conexion;
                    object isAdmin = cmd.ExecuteScalar();
                    usuario.isAdmin = Convert.ToBoolean(isAdmin);

                    var admin = usuario.isAdmin;
                    ViewData["admin"] = admin;

                    conexion.Close();
                    return Page();
                }
                
            }

            else
            {
                return RedirectToPage("/Index");
            }

        }

        public IActionResult OnPostSaveBiography()
        {
            string? userID = HttpContext.Request.Cookies["ID_USER"];
            string connectionString = System.IO.File.ReadAllText("../.connectionstring.txt");

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "update_bio";
                cmd.Connection = conexion;

                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@Biography", usuario.bio);

                cmd.ExecuteNonQuery();

                conexion.Close();
                return RedirectToPage();
            }

        }

        public async Task<IActionResult> OnPostAsync(IFormFile profilePicture)
        {
            string? userID = HttpContext.Request.Cookies["ID_USER"];
            string connectionString = System.IO.File.ReadAllText("../.connectionstring.txt");

            var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePics");
            Directory.CreateDirectory(uploadsDirectory);

            string fileName = $"{userID}_{DateTime.Now:yyyyMMddHHmmss}.jpg";
            var filePath = Path.Combine(uploadsDirectory, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(stream);
            }

            using (MySqlConnection conexion = new MySqlConnection(connectionString))
            {
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "update_img";
                cmd.Connection = conexion;

                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.Parameters.AddWithValue("@profilepic", $"~/profilePics/{fileName}");
                cmd.ExecuteNonQuery();

                conexion.Close();
                return RedirectToPage();

            }
        }

    }
}
   
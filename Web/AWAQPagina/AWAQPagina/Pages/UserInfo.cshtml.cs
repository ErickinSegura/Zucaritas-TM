using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Input;

namespace AWAQPagina.Pages
{
    public class UserInfoModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        [BindProperty]
        public Usuario usuario { get; set; }

        public UserInfoModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            usuario = new Usuario();
        }

        public IActionResult OnGet()
        {
            string userID = _httpContextAccessor.HttpContext.Request.Cookies["ID_USER"];
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";

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
                            usuario.bio = "Crea tu biografia!";
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
            }

            return Page();
        }

        public IActionResult OnPostSaveBiography()
        {
            string userID = _httpContextAccessor.HttpContext.Request.Cookies["ID_USER"];
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";

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
            }

            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostAsync(IFormFile profilePicture)
        {
            string userID = _httpContextAccessor.HttpContext.Request.Cookies["ID_USER"];
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=Vela0376;";

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

            }

            return RedirectToPage();
        }
    }
}

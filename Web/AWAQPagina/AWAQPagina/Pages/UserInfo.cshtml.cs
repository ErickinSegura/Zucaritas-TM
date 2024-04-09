using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace AWAQPagina.Pages
{
    public class UserInfoModel : PageModel
    {
        [BindProperty]
        public string Biography { get; set; }

        public string imagePath { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnPostSaveBiography()
        {
            // Retrieve the UserId from cookies
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            // Update the biography in the database using the user ID
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=password;";
            string sql = "UPDATE USUARIO SET BIO = @Biography WHERE ID_USER = @userId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@userId", userId);
                    command.Parameters.AddWithValue("@Biography", Biography);
                    command.ExecuteNonQuery();
                }
            }

            // Redirect to the same page after saving the biography
            return RedirectToPage();
        }

        public void OnGet()
        {
            // Retrieve the UserId from cookies
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            // Fetch the biography and image path from the database using the user ID
            string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=lala14;";
            string bioSql = "SELECT BIO FROM USUARIO WHERE ID_USER = @userId";
            string imagePathSql = "SELECT Imagen_USUARIO FROM USUARIO WHERE ID_USER = @userId";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Fetch biography
                using (MySqlCommand bioCommand = new MySqlCommand(bioSql, connection))
                {
                    bioCommand.Parameters.AddWithValue("@userId", userId);
                    object bioResult = bioCommand.ExecuteScalar();
                    if (bioResult != null)
                    {
                        Biography = bioResult.ToString();
                    }
                    else
                    {
                        Biography = "Create a Bio!"; // Set to empty string if biography is not found
                    }
                }

                // Fetch image path
                using (MySqlCommand imagePathCommand = new MySqlCommand(imagePathSql, connection))
                {
                    imagePathCommand.Parameters.AddWithValue("@userId", userId);
                    object imagePathResult = imagePathCommand.ExecuteScalar();
                    if (imagePathResult != null)
                    {
                        imagePath = imagePathResult.ToString().Substring(1);
                        // You can optionally use imagePath here if needed
                    }
                    else
                    {
                        // Image path not found
                    }
                }
            }
        }


        public async Task<IActionResult> OnPostAsync(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePics");
                Directory.CreateDirectory(uploadsDirectory);

                string fileName = $"{userId}.jpg";
                var filePath = Path.Combine(uploadsDirectory, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Save the file path to the database
                string connectionString = "Server=127.0.0.1;Port=3306;Database=AWAQ;Uid=root;password=lala14;";
                string sql = "UPDATE USUARIO SET Imagen_USUARIO = @ProfilePicturePath WHERE ID_USER = @userId";

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@ProfilePicturePath", $"~/profilePics/{fileName}");
                        command.ExecuteNonQuery();
                    }
                }

                return RedirectToPage("/UserInfo");
            }

            return Page();
        }
    }
}

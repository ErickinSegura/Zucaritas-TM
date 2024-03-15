using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;
using System.Threading.Tasks;

namespace AWAQPagina.Pages
{
    public class UserInfoModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserInfoModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void OnGet()
        {
            // Retrieve the UserId from cookies
            string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

            // Use the UserId as needed
        }

        public async Task<IActionResult> OnPostAsync(IFormFile profilePicture)
        {
            if (profilePicture != null && profilePicture.Length > 0)
            {
                // Retrieve the UserId from cookies
                string userId = _httpContextAccessor.HttpContext.Request.Cookies["UserId"];

                var uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePics");
                var filePath = Path.Combine(uploadsDirectory, userId);

                // Create the uploads directory if it doesn't exist
                Directory.CreateDirectory(uploadsDirectory);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Optionally, you can save the file path to a database here

                return RedirectToPage("/UserInfo"); // Redirect to the same page after successful upload
            }

            return Page(); // If no file is uploaded, return to the same page
        }
    }
}

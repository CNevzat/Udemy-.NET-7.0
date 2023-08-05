using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace UdemyWebApplication.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public int StudentNo { get; set; }

        public string? Address { get; set; }
        public string? Faculty { get; set; }
        public string? Department { get; set; }

    }
}

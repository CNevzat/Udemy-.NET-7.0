using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UdemyWebApplication.Models
{
    public class Rent
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public int StudentID { get; set; }

        [ValidateNever]
        public int BookID { get; set; }

        [ForeignKey("BookID")]
        [ValidateNever]
        public Book Book { get; set; }

    }
}

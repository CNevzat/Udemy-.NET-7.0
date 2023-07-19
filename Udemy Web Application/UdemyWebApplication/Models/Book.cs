using System.ComponentModel.DataAnnotations;

namespace UdemyWebApplication.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        public string BookName { get; set; }
        public string Description { get; set; }
        [Required]
        public string Writer { get; set; }
        [Required]
        [Range(10,5000)] // Fiyat aralığı
        public double Price { get; set; }

    }
}

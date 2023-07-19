using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UdemyWebApplication.Models
{
	public class BookType
	{
		[Key] //primary key
		public int ID { get; set; }
		[Required(ErrorMessage = "Type name cannot be empty!")] //not null
		[MaxLength(25)]
		[DisplayName("Book Type")]
		public string Name { get; set; }
	}
}

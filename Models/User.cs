

using System.ComponentModel.DataAnnotations;

namespace HashingAndSalting.Models
{
	public class User
	{
		[Key]
		public int Id { get; set; }

		[Required(AllowEmptyStrings =false)]
		[MaxLength(100)]
		public string Name { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		[MaxLength(100)]
		public string Password { get; set; } = string.Empty;

		[Required(AllowEmptyStrings = false)]
		public string Salt { get; set; } = string.Empty;


	}
}

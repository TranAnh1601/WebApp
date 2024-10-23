using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	[Table("Categories")]
	public class Category : BaseEntity
	{
		[MaxLength(1000)]
		public string Name { get; set; }

		[MaxLength(1000)]
		public string Image { get; set; }

		public ICollection<Product> Products { get; set; }
	}

}

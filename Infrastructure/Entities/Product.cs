using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	[Table("Products")]
	public class Product : BaseEntity
	{
		[MaxLength(1000)]
		public string Name { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }
		public int Quantity { get; set; } = 0;

		[Column(TypeName = "ntext")]
		public string Detail { get; set; }

		[Column(TypeName = "ntext")]
		public string Description { get; set; }

		//config khoa ngoai
		[ForeignKey("Product-Category")]	
		public Guid CategoryId { get; set; }

		[Column(TypeName = "decimal(18,2)")]
		public decimal? DiscountPrice { get; set; }
		public Category Category { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public ICollection<ProductImage> ProductImages { get; set; }
	}

}

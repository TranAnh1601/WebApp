using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
	[Table("ProductImages")]
	public class ProductImage
	{
		[Column(TypeName = "ntext")]
		public string ImageLink { get; set; }

		[Column(TypeName = "ntext")]
		public string Alt { get; set; }

		[ForeignKey("Image-Product")]
		public Guid ProductId { get; set; }
		public Product Product { get; set; }
	}

}

using Infrastructure.Model.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Model.Products
{
	public class ProductIndexModel
	{
		public List<CategoryViewModel> Categories { get; set; }                                     
        public List<ProductOrderByModel> OrderBys { get; set; }
		public List<int> SelectPageSize { get; set; }
		public string CategoryId { get; set; }
		public string KeyWord { get; set; }
	}
	public class ProductOrderByModel
	{
		public int Value { get; set; }
		public string Name { get; set; }
	}

}

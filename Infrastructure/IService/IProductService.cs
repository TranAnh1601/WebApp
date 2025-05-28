using Infrastructure.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
	public interface IProductService
	{
        ProductDetailViewModel GetProductDetail(Guid productId);
        List<ProductViewModel> SearchProduct(ProductFilterModel filter);
	}
}

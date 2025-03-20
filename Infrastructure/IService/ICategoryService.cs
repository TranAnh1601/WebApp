using Infrastructure.Model.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.IService
{
	public interface ICategoryService
	{
		List<CategoryViewModel> GetCategories();
	}
}

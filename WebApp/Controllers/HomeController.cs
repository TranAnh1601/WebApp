using Infrastructure.IService;
using Infrastructure.Model.Categories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ICategoryService _categoryService;

		public HomeController(ILogger<HomeController> logger, ICategoryService categoryService)
		{
			_categoryService = categoryService;
			_logger = logger;
		}

		public IActionResult Index()
		{

            //var categories = _categoryService.GetCategories(); // hoặc lấy từ database
            //return View(categories);
            //var model = _categoryService.GetCategories(); // Lấy danh mục từ DB
            //return View(model); // Trả về Model
            return View();
        }

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}

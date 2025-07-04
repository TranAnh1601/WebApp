using Infrastructure.Commons;
using Infrastructure.Enums;
using Infrastructure.IService;
using Infrastructure.Model.Bill;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApp.Constants;
using WebApp.Extentions;
using WebApp.Helpers;
using WebApp.Models;

namespace WebApp.Controllers
{
	public class CheckoutController : Controller
	{
        private readonly IBillService _billService;
        public CheckoutController(IBillService billService)
        {
            _billService = billService;
        }

        public IActionResult Index()
		{          
            var model = new CheckoutViewModel();
            var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
            model.Items = cart;
            model.ShippingMethod = EnumHelper.GetList(typeof(PaymentMethod));
            return View(model);
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> PlaceOrder(CustomerInfoModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var cart = HttpContext.Session.GetCart(TechShopConstant.Cart);
            if (cart == null)
            {
                return Json(new ResponseResult(400, "cart is empty"));
            }
            BillCreateViewModel billModel = new BillCreateViewModel();
            billModel.FirstName = model.FirstName;
            billModel.LastName = model.LastName;
            billModel.Email = model.Email;
            billModel.PhoneNumber = model.PhoneNumber;
            billModel.Address = model.Address;
            billModel.PaymentMethod = model.PaymentMethod;
            billModel.BillDetails = cart.Select(s => new BillDetailCreateViewModel
            {
                Price = s.Price,
                ProductName = s.ProductName,
                Quantity = s.Quantity,
            }).ToList();
            var response = await _billService.CreateBill(billModel);
            HttpContext.Session.Remove(TechShopConstant.Cart);
            TempData["checkout"] = JsonConvert.SerializeObject(response);
            return RedirectToAction("Index", "Checkout");
        }

    }
}

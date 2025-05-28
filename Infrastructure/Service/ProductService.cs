using Infrastructure.Entities;
using Infrastructure.Enums;
using Infrastructure.IService;
using Infrastructure.Model.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public class ProductService : IProductService
    {
        private readonly TechShopDbContext _context;

        public ProductService(TechShopDbContext context)
        {
            _context = context;
        }
        public List<ProductViewModel> SearchProduct(ProductFilterModel filter)
        {
            var productViewModels = new List<ProductViewModel>();
            var products = _context.Products.AsQueryable();
            var categories = _context.Categories.AsQueryable();
            var images = _context.ProductImages.AsQueryable();
            var reviews = _context.Reviews.AsQueryable();

            var result = (from p in products
                          join c in categories
                          on p.CategoryId equals c.Id
                          select new ProductViewModel
                          {
                              Id = p.Id,
                              Name = p.Name,
                              Price = p.Price,
                              DiscountPrice = p.DiscountPrice,
                              Description = p.Description,
                              Detail = p.Detail,
                              CategoryName = c.Name,
                              CategoryId = c.Id,
                          }).AsEnumerable();

            if (!string.IsNullOrEmpty(filter.CategoryId) && Guid.TryParse(filter.CategoryId, out Guid categoryId))
            {
                result = result.Where(s => s.CategoryId == categoryId);
            }

            if (filter.FromPrice.HasValue && filter.ToPrice.HasValue)
            {
                result = result.Where(s => s.Price >= filter.FromPrice.Value && s.Price <= filter.ToPrice.Value);
            }
            if (filter.ToPrice.HasValue && !filter.FromPrice.HasValue)
            {
                result = result.Where(s => s.Price <= filter.ToPrice.Value);
            }
            if (filter.FromPrice.HasValue && !filter.ToPrice.HasValue)
            {
                result = result.Where(s => s.Price >= filter.FromPrice.Value);
            }

            if (!string.IsNullOrEmpty(filter.KeyWord))
            {
                result = result.Where(s => s.Name.Equals(filter.KeyWord, StringComparison.OrdinalIgnoreCase) || s.CategoryName.Equals(filter.KeyWord, StringComparison.OrdinalIgnoreCase));
            }
            if (filter.SortBy.Equals(SortEnum.Price))
            {
                result = result.OrderBy(s => s.Price);
            }
            else
            {
                result = result.OrderBy(s => s.Name);
            }
            //trang 1 bo 0 lay 9 cai tiep theo, trang 2 bo qua 9 cai lay 9 cai tiep theo
            productViewModels = result.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize).ToList();

            foreach (var item in productViewModels)
            {
                var image = images.FirstOrDefault(s => s.ProductId == item.Id)?.ImageLink;
                item.Image = string.IsNullOrEmpty(image) ? string.Empty : image;

                var productReviews = reviews.Where(s => s.ProductId == item.Id);
                if (productReviews.Any())
                {
                    item.Rating = productReviews.Max(s => s.Rating);
                }
            }
            return productViewModels;
        }
        public ProductDetailViewModel GetProductDetail(Guid productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return null;
            }
            var result = new ProductDetailViewModel
            {
                Id = productId,
                Name = product.Name,
                Description = product.Description,
                Quantity = product.Quantity,
                Price = product.Price,
                DiscountPrice = product.DiscountPrice,
                CategoryId = product.CategoryId,
                CategoryName = GetCategory(product.CategoryId),
                Reviews = GetReviewModels(productId),
                Images = GetImageModels(productId),

            };

            //var results = new ProductDetailViewModel();
            //result.Id = product.Id;
            //result.Name = product.Name;
            //result.Description = product.Description;
            //result.Quantity = product.Quantity;
            //result.Price = product.Price;
            //result.DiscountPrice = product.DiscountPrice;
            //result.CategoryId = product.CategoryId;
            //result.CategoryName = GetCategory(product.CategoryId);
            //result.Reviews = GetReviewModels(productId);
            //result.Images = GetImageModels(productId);

            return result;
        }
        private string GetCategory(Guid categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            return category != null ? category.Name : string.Empty;
        }

        private List<ReviewModel> GetReviewModels(Guid productId)
        {
            var result = _context.Reviews
                .Where(s => s.ProductId == productId)
                .Select(x => new ReviewModel
                {
                    Id = x.Id,
                    Content = x.Content,
                    ReviewerName = x.ReviewerName,
                    Email = x.Email,
                    Rating = x.Rating

                });
            return result.ToList();
        }
        private List<ImageViewModel> GetImageModels(Guid productId)
        {

            var result = _context.ProductImages
                .Where(s => s.ProductId == productId)
                .Select(x => new ImageViewModel
                {
                    Id = x.Id,
                    ImageLink = x.ImageLink,
                    Alt = x.Alt,
                });
            return result.ToList();
        }


    }

}


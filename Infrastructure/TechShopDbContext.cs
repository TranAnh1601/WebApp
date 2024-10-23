using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
	public class TechShopDbContext : IdentityDbContext
	{
		public DbSet<Category> Categories { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<ProductImage> ProductImages { get; set; }
		public DbSet<Bill> Bills { get; set; }
		public DbSet<BillDetail> BillDetails { get; set; }

		public TechShopDbContext()
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

		}

	}
}

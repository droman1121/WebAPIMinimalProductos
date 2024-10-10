using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WebAPIMinimalProductos.Models
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
        : base(options) { }

        public DbSet<Producto> Productos => Set<Producto>();
    }
}

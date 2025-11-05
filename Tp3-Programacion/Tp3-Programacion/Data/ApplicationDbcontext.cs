using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp3_Programacion.Models;

namespace Tp3_Programacion.Data
{
    public class ApplicationDbcontext : DbContext
    {
            public DbSet<Producto> Productos { get; set; }
            public DbSet<Cliente> Clientes { get; set; }
            public DbSet<Venta> Ventas { get; set; }
            public DbSet<VentaDetalle> VentaDetalles { get; set; }

            
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=TP3-Programacion;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Producto>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);
        }


    }
}

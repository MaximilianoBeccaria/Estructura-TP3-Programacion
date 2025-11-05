using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Tp3_Programacion.Data
{
    public class AplicationDbContextcs : DbContext
    {
        // Definicio de tablas.
        //public DbSet<clase> clases { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=TP3-Programacion;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}

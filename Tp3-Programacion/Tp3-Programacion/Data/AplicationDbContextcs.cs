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
    public class AplicationDbContextcs : DbContext
    {
        public DbSet<Cancha> Canchas { get; set; }
        public DbSet<Socio> Socios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=(localdb)\\MSSQLLocalDB;Database=CentroDeportivo;Trusted_Connection=True;TrustServerCertificate=True;"
            );
        }
    }
}

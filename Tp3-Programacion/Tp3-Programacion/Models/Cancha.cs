using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_Programacion.Models
{
    public class Cancha
    {
        public int CanchaId { get; set; }
        public string Nombre { get; set; }
        public string Tipo { get; set; } // Futbol, Tenis, Padel
        public bool Activa { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_Programacion.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int SocioId { get; set; }
        public Socio Socio { get; set; }

        public int CanchaId { get; set; }
        public Cancha Cancha { get; set; }

        public DateTime FechaHora { get; set; }
    }
}

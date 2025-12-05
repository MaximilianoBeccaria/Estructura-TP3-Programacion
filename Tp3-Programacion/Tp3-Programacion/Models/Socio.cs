using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_Programacion.Models
{
    public class Socio
    {
        public int SocioId { get; set; }
        public string Nombre { get; set; }
        public string Documento { get; set; }
        public string CorreoElectronico { get; set; }

        public ICollection<Reserva> Reservas { get; set; }
    }
}

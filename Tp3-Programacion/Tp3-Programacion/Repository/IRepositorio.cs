using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp3_Programacion.Repository
{
    public interface IRepositorio<T> where T : class
    {
        void Agregar(T entidad);
        T ObtenerPorId(int id);
        IEnumerable<T> ObtenerTodos();
        void Guardar();
    }

}

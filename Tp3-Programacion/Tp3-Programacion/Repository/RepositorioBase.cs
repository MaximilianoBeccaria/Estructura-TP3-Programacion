using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Tp3_Programacion.Data;
using static Tp3_Programacion.Data.ApplicationDbcontext;

namespace Tp3_Programacion.Repository
{
    public class RepositorioBase<T> : IRepositorio<T> where T : class
    {
        protected readonly ApplicationDbcontext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositorioBase(ApplicationDbcontext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Agregar(T entidad) => _dbSet.Add(entidad);
        public T ObtenerPorId(int id) => _dbSet.Find(id);
        public IEnumerable<T> ObtenerTodos() => _dbSet.ToList();
        public void Guardar() => _context.SaveChanges();
    }

}

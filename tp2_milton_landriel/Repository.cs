using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tp2_milton_landriel
{
    public class Repository<T>
    where T : class
    {
        private NorthwindEntities _context;

        public Repository()
        {
            _context = new NorthwindEntities();
        }

        public T Persist(T entity)
        {
            return _context.Set<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public T Update(T entity)
        {
            _context.Entry<T>(entity);

            return entity;
        }

        public IQueryable<T> Set()
        {
            return _context.Set<T>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using QLBanHangDemo.DAL;
using QLBanHangDemo.Services.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QLBanHangDemo.Services.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly QLBanHangMVC2Context QLBanHangMVC2Context;

        public Repository(QLBanHangMVC2Context qLBanHangMVCContext)
        {
            QLBanHangMVC2Context = qLBanHangMVCContext;
        }

        private void Save() => QLBanHangMVC2Context.SaveChanges();
        public void Add(T entity)
        {
            QLBanHangMVC2Context.Add(entity);
            Save();

        }

        public int Count(Func<T, bool> predicate)
        {
            return QLBanHangMVC2Context.Set<T>().Where(predicate).Count();
        }

        public void Delete(T entity)
        {
            QLBanHangMVC2Context.Remove(entity);
            Save();

        }

        public IEnumerable<T> GetAll()
        {
            return QLBanHangMVC2Context.Set<T>().ToList();
        }

        public IEnumerable<T> GetByFiler(Func<T, bool> predicate)
        {
            return QLBanHangMVC2Context.Set<T>().Where(predicate).ToList();
        }

        public T GetById(int id)
        {
            return QLBanHangMVC2Context.Set<T>().Find(id);
        }

        public void Update(T entity)
        {
            QLBanHangMVC2Context.Entry(entity).State = EntityState.Modified;
            Save();
        }
    }
}

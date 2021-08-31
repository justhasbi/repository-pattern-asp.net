using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NETCore.Repository
{
    public class GeneralRepository<C, E, K> : IRepository<E, K> 
        where E : class
        where C : MyContext
    {
        private readonly MyContext myContext;
        private readonly DbSet<E> dbSet;

        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
            dbSet = myContext.Set<E>();
        }

        public int Delete(K key)
        {
            var data = dbSet.Find(key);
            if (data == null)
            {
                throw new ArgumentNullException();
            }

            dbSet.Remove(data);
            return myContext.SaveChanges();
        }

        public IEnumerable<E> GetAll()
        {
            if (dbSet.ToList().Count == 0)
            {
                return null;
            }
            return dbSet.ToList();
        }

        public E GetById(K key)
        {
            return dbSet.Find(key);
        }

        public int Insert(E entity)
        {
            dbSet.Add(entity);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(E entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}

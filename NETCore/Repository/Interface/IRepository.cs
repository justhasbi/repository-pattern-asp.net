using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository.Interface
{
    public interface IRepository<E, K> where E : class
    {
        IEnumerable<E> GetAll();
        
        E GetById(K key);
        
        int Insert(E entity);
        
        int Update(E entity);
        
        int Delete(K key);
    }
}

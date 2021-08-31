using Microsoft.EntityFrameworkCore;
using NETCore.Context;
using NETCore.Models;
using NETCore.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NETCore.Repository
{
    public class oldPersonRepository : oldIPersonRepository
    {
        private readonly MyContext myContext;
        public oldPersonRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(string NIK)
        {

            var data = myContext.Persons.Find(NIK);
            if(data == null)
            {
                throw new ArgumentNullException();
            }
            
            myContext.Persons.Remove(data);
            return myContext.SaveChanges();
        }

        public IEnumerable<Person> Get()
        {

            if(myContext.Persons.ToList().Count == 0)
            {
                return null;
            }
            return myContext.Persons.ToList();
        }

        public Person Get(string NIK)
        {
            return myContext.Persons.Find(NIK);
        }

        public int Insert(Person person)
        {
            myContext.Persons.Add(person);
            var insert = myContext.SaveChanges();
            return insert;
        }

        public int Update(Person person)
        {
            myContext.Entry(person).State = EntityState.Modified;
            return myContext.SaveChanges();
        }
    }
}

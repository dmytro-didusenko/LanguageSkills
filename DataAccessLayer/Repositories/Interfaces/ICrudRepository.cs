using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories.Interfaces
{
    interface ICrudRepository<T> where T : class
        {
            IEnumerable<T> GetAll();
            T Get(int id);
            void Create(T item);
            void Update(T item);
            void Delete(int id);
        }
    }

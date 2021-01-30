using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Repositories.Interfaces
{
    interface ICrudRepository<T> where T : class
        {
            /// <summary>
            /// Get all items from table DB
            /// </summary>
            /// <returns>All items</returns>
            IEnumerable<T> GetAll();

            /// <summary>
            /// Get item by Id from table DB
            /// </summary>
            /// <param name="id">Item Id</param>
            /// <returns>Item</returns>
            T Get(int id);

            /// <summary>
            /// Add new range of items
            /// </summary>
            /// <param name="items">List of items</param>
            void CreateRange(List<T> items);

            /// <summary>
            /// Update the item
            /// </summary>
            /// <param name="item">Item to update</param>
            void Update(T item);

            /// <summary>
            /// Delete item by Id from table DB
            /// </summary>
            /// <param name="id">Item Id to delete</param>
            void Delete(int id);
        }
    }

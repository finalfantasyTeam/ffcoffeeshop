using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public interface IBaseRepository<T>
    {
        IUnitOfWork UnitOfWork { get; }

        bool Exists(object primaryKey);

        IEnumerable<T> GetAll();

        /// <summary>
        /// Retrieve craeted by and modified by id's FullName
        /// </summary>
        /// <param name="dynamicObject">The primary key of the record</param>
        /// <returns></returns>
        Dictionary<string, string> GetAuditNames(dynamic dynamicObject);

        int Insert(T entity);

        int Delete(T entity);

        void Update(T entity);

        T Single(object primaryKey);

        T SingleOrDefault(object primaryKey);
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Transactions;

namespace ff.coffee.repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TransactionScope _transaction;

        private readonly CoffeeShopEntities _db;
        public DbContext Db
        {
            get { return _db; }
        }

        public UnitOfWork()
        {
            _db = new CoffeeShopEntities();
        }

        public void Dispose()
        {
        }

        public void StartTransaction()
        {
            _transaction = new TransactionScope();
        }

        public void Commit()
        {
            _db.SaveChanges();
            _transaction.Complete();
        }
    }
}

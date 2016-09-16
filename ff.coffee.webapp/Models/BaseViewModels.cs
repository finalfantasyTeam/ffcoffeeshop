using ff.coffee.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ff.coffee.webapp.Models
{
    public class BaseViewModels
    {
        protected IUnitOfWork uow { get; set; }

        public virtual void GetDataToList() { }

        public virtual void GetDataToModel(int Id) { }

        protected virtual void MapDtoToModel() { }
        protected virtual void MapModelToDto() { }

        public virtual int InsertModel() { return 0; }
        public virtual int DeleteModel(int Id) { return 0; }
        public virtual void UpdateModel() { }
    }
}
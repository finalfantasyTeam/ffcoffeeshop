using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class ProductCategoryRepository : BaseRepository<ProductCat>
    {
        public ProductCategoryRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

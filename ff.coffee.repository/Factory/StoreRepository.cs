using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class StoreRepository : BaseRepository<Store>
    {
        public StoreRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

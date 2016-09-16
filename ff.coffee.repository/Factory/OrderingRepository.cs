using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class OrderingRepository : BaseRepository<Ordering>
    {
        public OrderingRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

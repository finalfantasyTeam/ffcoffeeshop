using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class AreaRepository : BaseRepository<Area>
    {
        public AreaRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

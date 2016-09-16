using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class StaffRepository : BaseRepository<Staff>
    {
        public StaffRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

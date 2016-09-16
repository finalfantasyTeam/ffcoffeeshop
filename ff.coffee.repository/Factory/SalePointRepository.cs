using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class SalePointRepository : BaseRepository<SalePoint>
    {
        public SalePointRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

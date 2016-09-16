using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class ImportDetailRepository : BaseRepository<ImportDetail>
    {
        public ImportDetailRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

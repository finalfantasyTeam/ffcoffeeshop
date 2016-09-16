using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class InvoiceRepository : BaseRepository<Invoice>
    {
        public InvoiceRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ff.coffee.repository
{
    public class CustomerRepository : BaseRepository<Customer>
    {
        public CustomerRepository(IUnitOfWork unit)
            : base(unit)
        { }
    }
}

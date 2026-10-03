using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Common.Domain.Exceptions
{
    public partial class BaseDomainExceotion:Exception
    {
        public BaseDomainExceotion()
        {
        }

        public BaseDomainExceotion(string message) : base(message)
        {
        }
    }

}

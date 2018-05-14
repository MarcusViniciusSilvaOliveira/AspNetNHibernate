using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Customer
    {
        public virtual long? Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string CPF { get; set; }
    }
}

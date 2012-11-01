using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NHibernate.GuitarStore.Common
{
    public class Guitar
    {
        public virtual Guid Id { get; set; }
        public virtual string Type { get; set; }

        public virtual IList<Inventory> Inventory { get; set; }
    }
}

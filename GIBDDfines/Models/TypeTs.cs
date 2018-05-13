using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class TypeTs
    {
        public TypeTs()
        {
            Autoes = new HashSet<Autoes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Autoes> Autoes { get; set; }
    }
}

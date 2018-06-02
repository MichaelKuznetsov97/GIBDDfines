using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Tscategories
    {
        public Tscategories()
        {
            Autoes = new HashSet<Autoes>();
            LinkOwnCateg = new HashSet<LinkOwnCateg>();
        }

        public int Id { get; set; }
        public string Describe { get; set; }
        public string Name { get; set; }

        public ICollection<Autoes> Autoes { get; set; }
        public ICollection<LinkOwnCateg> LinkOwnCateg { get; set; }
    }
}

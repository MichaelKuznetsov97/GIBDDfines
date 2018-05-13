using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Models
    {
        public Models()
        {
            Autoes = new HashSet<Autoes>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdMark { get; set; }

        public Marks IdMarkNavigation { get; set; }
        public ICollection<Autoes> Autoes { get; set; }
    }
}

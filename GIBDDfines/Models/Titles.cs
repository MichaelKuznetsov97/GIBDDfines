using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Titles
    {
        public Titles()
        {
            Police = new HashSet<Police>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Police> Police { get; set; }
    }
}

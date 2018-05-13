using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Marks
    {
        public Marks()
        {
            Models = new HashSet<Models>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Models> Models { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class AutoOwners
    {
        public AutoOwners()
        {
            Autoes = new HashSet<Autoes>();
            LinkOwnCateg = new HashSet<LinkOwnCateg>();
            Punishments = new HashSet<Punishments>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateEntered { get; set; }
        public DateTime? DateConfisc { get; set; }
        public DateTime? DateReturn { get; set; }
        public string Number { get; set; }

        public ICollection<Autoes> Autoes { get; set; }
        public ICollection<LinkOwnCateg> LinkOwnCateg { get; set; }
        public ICollection<Punishments> Punishments { get; set; }
    }
}

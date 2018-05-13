using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class TypePunishments
    {
        public TypePunishments()
        {
            Punishments = new HashSet<Punishments>();
        }

        public int Id { get; set; }
        public string Describe { get; set; }

        public ICollection<Punishments> Punishments { get; set; }
    }
}

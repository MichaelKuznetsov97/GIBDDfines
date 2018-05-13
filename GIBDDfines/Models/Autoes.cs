using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Autoes
    {
        public Autoes()
        {
            Punishments = new HashSet<Punishments>();
        }

        public int Id { get; set; }
        public string Number { get; set; }
        public int? Pengine { get; set; }
        public DateTime? DateRelease { get; set; }
        public int IdMarkModel { get; set; }
        public int IdColor { get; set; }
        public int IdType { get; set; }
        public int IdTcateg { get; set; }
        public int IdOwner { get; set; }

        public Colors IdColorNavigation { get; set; }
        public Models IdMarkModelNavigation { get; set; }
        public AutoOwners IdOwnerNavigation { get; set; }
        public Tscategories IdTcategNavigation { get; set; }
        public TypeTs IdTypeNavigation { get; set; }
        public ICollection<Punishments> Punishments { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Police
    {
        public Police()
        {
            Punishments = new HashSet<Punishments>();
        }

        public string Id { get; set; }
        public int IdTitle { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public Titles IdTitleNavigation { get; set; }
        public ICollection<Punishments> Punishments { get; set; }
    }
}

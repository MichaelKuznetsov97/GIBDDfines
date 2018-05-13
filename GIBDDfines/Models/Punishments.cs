using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class Punishments
    {
        public string Id { get; set; }
        public DateTime Date { get; set; }
        public int? Penalty { get; set; }
        public DateTime? DatePay { get; set; }
        public string Describe { get; set; }
        public int IdAuto { get; set; }
        public int IdAowner { get; set; }
        public string IdPolice { get; set; }
        public int IdTpunish { get; set; }

        public AutoOwners IdAownerNavigation { get; set; }
        public Autoes IdAutoNavigation { get; set; }
        public Police IdPoliceNavigation { get; set; }
        public TypePunishments IdTpunishNavigation { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace GIBDDfines.Models
{
    public partial class LinkOwnCateg
    {
        public int Id { get; set; }
        public int IdAowner { get; set; }
        public int IdCategory { get; set; }

        public AutoOwners IdAownerNavigation { get; set; }
        public Tscategories IdCategoryNavigation { get; set; }
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GIBDDfines.Models
{
    public class User : IdentityUser
    {
        public string nameSurname { get; set; }
        public string Number { get; set; }
    }
}

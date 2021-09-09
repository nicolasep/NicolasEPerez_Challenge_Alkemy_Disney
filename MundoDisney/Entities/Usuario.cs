using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Entities
{
    public class Usuario : IdentityUser
    {
        public bool IsActive { get; set; }
        
    }
}

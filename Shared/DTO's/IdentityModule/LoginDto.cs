using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s.IdentityModule
{
    public record LoginDto
    {


        public String Email { get; set; }
        public String Password { get; set; }
    }
}

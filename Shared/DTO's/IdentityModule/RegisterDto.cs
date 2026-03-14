using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTO_s.IdentityModule
{
    public record RegisterDto
    {
        public string UserName { get; set; }
        public String DisplayName { get; set; }
        [EmailAddress]
        public String Email { get; set; }
        public String Password { get; set; }
        [Phone]
        public String PhoneNumber { get; set; }
    }
}

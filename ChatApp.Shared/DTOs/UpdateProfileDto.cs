using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ChatApp.Shared.DTOs
{
    public class UpdateProfileDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
    }
}

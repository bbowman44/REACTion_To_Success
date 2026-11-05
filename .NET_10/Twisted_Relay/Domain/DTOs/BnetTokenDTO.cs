using System;
using System.Collections.Generic;
using System.Text;

namespace Twisted.Domain.DTOs
{
    public class BnetTokenDTO
    {
        public required string access_token { get; set; }
        public required string token_type { get; set; }
        public required int expires_in { get; set; }
        public required string sub { get; set; }
    }
}

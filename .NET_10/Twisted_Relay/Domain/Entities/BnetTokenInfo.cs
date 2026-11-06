using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using System.Text;

namespace Twisted.Domain.Entities
{
    public class BnetTokenInfo
    {
        [Key]
        public int BnetTokenInfoId { get; set; }
        [Required]
        public required string AccessToken { get; set; }
        [Required]
        public required string TokenType { get; set; }
        [Required]
        public required int ExpiresIn { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public string? Sub { get; set; }
    }
}

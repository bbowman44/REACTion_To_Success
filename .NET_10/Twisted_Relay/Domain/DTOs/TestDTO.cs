using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Twisted.Domain
{
    public class TestDTO
    {
        public required string FieldString { get; set; }
        public required int FieldInt { get; set; }
        public required bool FieldBool { get; set; }
    }
}

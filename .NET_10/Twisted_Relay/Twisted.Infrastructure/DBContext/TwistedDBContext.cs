using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Twisted.Infrastructure.DBContext
{
    public class TwistedDBContext : DbContext
    {
        public TwistedDBContext(DbContextOptions<TwistedDBContext> options) : base(options)
        {
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Twisted.Domain.Entities;

namespace Twisted.Infrastructure.DBContext
{
    public class TwistedDBContext : DbContext
    {
        public TwistedDBContext(DbContextOptions<TwistedDBContext> options) : base(options)
        {
        }

        public DbSet<BnetTokenInfo> BnetTokenInfo { get; set; }
    }
}

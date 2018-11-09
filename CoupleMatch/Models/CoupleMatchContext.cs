using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KamiJal.CoupleMatch.Models
{
    public class CoupleMatchContext : DbContext
    {
        public virtual DbSet<Subscriber> Subscribers { get; set; }

        public CoupleMatchContext() : base("CoupleMatchContext") {}
    }
}

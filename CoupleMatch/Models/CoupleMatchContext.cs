using System.Data.Entity;

namespace KamiJal.CoupleMatch.Models
{
    public class CoupleMatchContext : DbContext
    {
        public CoupleMatchContext() : base("CoupleMatchContext")
        {
        }

        public virtual DbSet<Subscriber> Subscribers { get; set; }
    }
}
using System.Data.Entity;


namespace FileCFDIMonitor
{
    public class ObradorDBContext : DbContext
    {
        public virtual DbSet<BdospModel> Bdosps { get; set; }
    }
}

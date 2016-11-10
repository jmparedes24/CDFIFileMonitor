using System.Data;


namespace FileCFDIMonitor
{
    public class ObradorDBContext : DbContext
    {
        public virtual DbSet<BdospModel> Bdosps { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Configure default schema
            modelBuilder.HasDefaultSchema("Admin");

            //Map entity to table
            modelBuilder.Entity<Student>().ToTable("StudentInfo");
            modelBuilder.Entity<Standard>().ToTable("StandardInfo", "dbo");
        }
    }
}

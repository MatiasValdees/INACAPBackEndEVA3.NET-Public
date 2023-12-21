using Microsoft.EntityFrameworkCore;

namespace EVA3.Models
{
    public class AppDbContext:DbContext
    {
        public DbSet<Music> TblMusic { get; set; }
        public DbSet<PlayList> TblPlayList { get; set; }
        public DbSet<PlayListMusic> TblRelPlayListMusic { get; set; }
        public DbSet<User> TblUser { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Eva3BackEnd;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayListMusic>().HasKey(x => new { x.PlayListId, x.MusicId });
            modelBuilder.Entity<PlayListMusic>().HasOne<PlayList>(x => x.playList).WithMany(p => p.playListMusics).HasForeignKey(x => x.PlayListId);
            modelBuilder.Entity<PlayListMusic>().HasOne<Music>(x => x.music).WithMany(m => m.playListMusics).HasForeignKey(x => x.MusicId);
            base.OnModelCreating(modelBuilder);
        }
    }
}

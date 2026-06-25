using Microsoft.EntityFrameworkCore;
using SIKAMTA.Models;

namespace SIKAMTA.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Kategori> Kategori { get; set; }

    public DbSet<TransaksiKas> TransaksiKas { get; set; }

    public DbSet<Pengaturan> Pengaturan { get; set; }
}
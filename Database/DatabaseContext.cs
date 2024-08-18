using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
	public class DatabaseContext : DbContext
	{
		//DO NOT DELETE THIS CTOR
		public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<DbWindowsWorkstation> Workstations { get; set; }
		public DbSet<DbCpuInfo> Cpus { get; set; }
		public DbSet<DbRamInfo> Rams { get; set; }
		public DbSet<DbOsInfo> OperationalSystems { get; set; }
		public DbSet<DbDiskInfo> Disks { get; set; }
		public DbSet<DbProgram> Programs { get; set; }
	}
}

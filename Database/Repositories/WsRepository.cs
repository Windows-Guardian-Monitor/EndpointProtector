using Common.Contracts.DAL;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Database.Repositories
{
	public class WsRepository : IWindowsWorkstationRepository
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private readonly DatabaseContext _databaseContext;

		public WsRepository(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			var scope = _serviceScopeFactory.CreateScope();
			_databaseContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
		}

		public void Delete(int id)
		{
			var w = GetById(id);
			_databaseContext.Workstations.Remove(w);
			_databaseContext.SaveChanges();
		}

		public IEnumerable<DbWindowsWorkstation> GetAll() => _databaseContext.Workstations.ToList();

		public DbWindowsWorkstation GetById(int id) => _databaseContext.Workstations
			.Include(x => x.CpuInfo)
				.Include(x => x.OsInfo)
				.Include(x => x.RamInfo)
				.Include(x => x.DisksInfo).FirstOrDefault(w => w.Id == id);

		public DbWindowsWorkstation? GetFirst()
		{
			return _databaseContext.Workstations.Include(w => w.CpuInfo).Include(w => w.DisksInfo).Include(w => w.RamInfo).Include(w => w.OsInfo).FirstOrDefault();
		}

		public void Insert(DbWindowsWorkstation item)
		{
			_databaseContext.Workstations.Add(item);

			_databaseContext.SaveChanges();
		}

		public void Upsert(DbWindowsWorkstation item)
		{
			var dbWorkstation = GetFirst();

			//var dbCpuInfo = new DbCpuInfo(item.CpuInfo.Architecture, item.CpuInfo.Description, item.CpuInfo.Manufacturer, item.CpuInfo.Name);

			//var dbOsVersion = new DbOsInfo(item.OsInfo.Architecture, item.OsInfo.Description, item.OsInfo.Manufacturer, item.OsInfo.SerialNumber, item.OsInfo.OsVersion, item.OsInfo.WindowsDirectory);

			//var dbRamInfo = new DbRamInfo(item.RamInfo.TotalMemory, item.RamInfo.Description, item.RamInfo.Manufacturer, item.RamInfo.Speed);


			//var dbDisks = item.DisksInfo.Select(d => new DbDiskInfo(d.AvailableSize, d.DiskName, d.DiskType, d.TotalSize));

			//var windowsWorkstation = new DbWindowsWorkstation(dbCpuInfo, dbDisks, dbOsVersion, dbRamInfo, item.Uuid, item.HostName);

			if (dbWorkstation is null)
			{
				Insert(item);

				return;
			}

			item.Id = dbWorkstation.Id;
			item.CpuInfo.Id = dbWorkstation.CpuInfo.Id;
			item.RamInfo.Id = dbWorkstation.RamInfo.Id;
            item.OsInfo.Id = dbWorkstation.OsInfo.Id;

			var i = 0;
            foreach (var diksInfo in dbWorkstation.DisksInfo)
            {
				item.DisksInfo[i].Id = diksInfo.Id;
				_databaseContext.DetachLocal(item.DisksInfo[i], dbWorkstation.DisksInfo[i]);
				_databaseContext.Disks.Update(item.DisksInfo[i]);
				i++;
            }



			_databaseContext.DetachLocal(item, dbWorkstation);
			_databaseContext.Workstations.Update(item);

			_databaseContext.DetachLocal(item.OsInfo, dbWorkstation.OsInfo);
			_databaseContext.OperationalSystems.Update(item.OsInfo);
			
			_databaseContext.DetachLocal(item.RamInfo, dbWorkstation.RamInfo);
			_databaseContext.Rams.Update(item.RamInfo);
			
			_databaseContext.DetachLocal(item.CpuInfo, dbWorkstation.CpuInfo);
			_databaseContext.Cpus.Update(item.CpuInfo);

			_databaseContext.SaveChanges();
		}
	}
}

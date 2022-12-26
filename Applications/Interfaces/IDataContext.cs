using Core.PMV.LogSheets;
using Core.PMV.Stations;
using Microsoft.EntityFrameworkCore;

namespace Applications.Interfaces
{
    public interface IDataContext
    {
        public DbSet<LogSheet> LogSheets { get; set; }
        public DbSet<LogSheetDetail> LogSheetDetails { get; set; }

        public DbSet<LVStation> LVStations { get; set; }

    }
}
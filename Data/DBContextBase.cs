/*
    @Date			              : 10.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Base DBContext. All entities must inherit IDataEntityBase
*/

using LundbeckConsulting.Components.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBContextBase
    {
        DbSet<SampleTypeOne> SampleTypeOne { get; set; }
        DbSet<SampleTypeTwo> SampleTypeTwo { get; set; }
    }

    public abstract class DBContextBase : DbContext, IDBContextBase
    {
        private readonly IConfiguration _config;
        private readonly DbContextOptions<DBContextBase> _options;

        public DBContextBase()
        {

        }

        public DBContextBase(IConfiguration config, DbContextOptions<DBContextBase> options)
        {
            _config = config;
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<SampleTypeOne>().ToTable("TypeOne");
            builder.Entity<SampleTypeTwo>().ToTable("TypeTwo");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseSqlServer(_config.GetConnectionString("Default"));
        }

        public DbSet<SampleTypeOne> SampleTypeOne { get; set; }
        public DbSet<SampleTypeTwo> SampleTypeTwo { get; set; }
    }
}

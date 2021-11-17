/*
    @Date			              : 10.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Base DBContext. All entities must inherit IDataEntityBase
*/

using LundbeckConsulting.Components.Core.Components.Data;
using LundbeckConsulting.Components.Core.Components.Data.Models;
using LundbeckConsulting.Components.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections;
using System.Collections.ObjectModel;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBContextBase
    {
        void AddEntity<TEntity>(string tableName) where TEntity : class, IDb;
        void AddNamespaces(params string[] names);
        string ConnectionString { get; }
        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IDb;
    }

    public abstract class DBContextBase : DbContext, IDBContextBase
    {
        private readonly string _connString;
        private readonly IConfiguration _config;
        private readonly ArrayList _entities = new ArrayList();
        private readonly ICollection<string> _namespaces = new Collection<string>();

        public DBContextBase(string connectionString)
        {
            _connString = connectionString;
        }

        public DBContextBase(IConfiguration config, DbContextOptions<DBContextBase> options) : base(options)
        {
            _config = config;
        }

        public void AddEntity<TEntity>(string tableName) where TEntity : class, IDb => _entities.Add(new DBContextDbSet<TEntity>(tableName));
        public void AddNamespaces(params string[] names) => _namespaces.AddRange(names);
        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : class, IDb
        {
            DbSet<TEntity> result = default(DbSet<TEntity>);

            foreach(IDBContextDbSet dbSet in _entities)
            {
                if (dbSet.EntityType == typeof(TEntity))
                {
                    result = ((DBContextDbSet<TEntity>)dbSet).DbSet;
                }
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach(IDBContextDbSet entity in _entities)
            {
                entity.SetEntity(this, builder);
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseSqlServer(this.ConnectionString);
        }

        public string ConnectionString => _connString;
    }
}

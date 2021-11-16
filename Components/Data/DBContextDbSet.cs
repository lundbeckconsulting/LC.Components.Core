/*
    @Date			: 29.07.2021
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components.Data.Models;
using LundbeckConsulting.Components.Core.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace LundbeckConsulting.Components.Core.Components.Data
{
    public interface IDBContextDbSet
    {
        string TableName { get; }
        void SetEntity(DBContextBase db, ModelBuilder builder);
        Type EntityType { get; }
    }

    public class DBContextDbSet<TEntity> : IDBContextDbSet where TEntity : class, IDb
    {
        public DBContextDbSet(string tableName)
        {
            this.TableName = tableName;
        }

        public void SetEntity(DBContextBase db, ModelBuilder builder)
        {
            if (db == null)
            {
                throw new ArgumentNullException("DB Context is null");
            }

            if (builder == null)
            {
                throw new ArgumentNullException("ModelBuilder is null");
            }

            this.EntityType = typeof(TEntity);

            builder.Entity<TEntity>().ToTable(this.TableName);

            this.DbSet = db.Set<TEntity>();
        }

        public string TableName { get; private set; }
        public DbSet<TEntity> DbSet { get; private set; }
        public Type EntityType { get; private set; }
    }
}

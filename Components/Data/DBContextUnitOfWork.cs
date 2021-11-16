/*
        @Date			 : 28.11.2019
        @Author			 : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components.Data.Models;
using LundbeckConsulting.Components.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBContextUnitOfWork<TEntity> where TEntity : class, IDb
    {
        /// <summary>
        /// Returns all elements that matches the query
        /// </summary>
        /// <param name="update">If true tracks changes to elements</param>
        Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, bool update = false);

        /// <summary>
        /// Adds an entity
        /// </summary>
        /// <param name="entity">Entity to add</param>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        Task<TEntity> Add(TEntity entity, bool instantSave = false);

        /// <summary>
        /// Adds entities in supplied collection
        /// </summary>
        /// <param name="entities">Elements to add</param>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities, bool instantSave = false);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        Task<TEntity> Remove(int id, bool instantSave = false);

        /// <summary>
        /// Removes an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        Task<TEntity> Remove(TEntity entity, bool instantSave = false);

        /// <summary>
        /// Removes all entities
        /// </summary>
        /// <param name="entities">Elements to remove</param>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        Task<IEnumerable<TEntity>> Remove(IEnumerable<TEntity> entities, bool instantSave = false);

        /// <summary>
        /// Removes elements matching predicate
        /// </summary>
        /// <param name="instantSave">If true changes are applied to database right away</param>
        /// <returns>The elements matching the predicate</returns>
        Task<IEnumerable<TEntity>> Remove(Expression<Func<TEntity, bool>> predicate, bool instantSave = false);

        /// <summary>
        /// Removes the entity with equal Id
        /// </summary>
        /// <param name="id">The Id of the element to remove</param>
        /// <param name="instantSave">If true changes are applied</param>
        Task RemovePermanent(int id, bool instantSave = false);

        /// <summary>
        /// Removes an entity completely from the database
        /// </summary>
        /// <param name="entity">Entity to remove from database</param>
        /// <param name="instantSave">If true changes are applied</param>
        Task RemovePermanent(TEntity entity, bool instantSave = false);

        /// <summary>
        /// Removes all entities completely from the database
        /// </summary>
        /// <param name="entities">Elements to remove from the database</param>
        /// <param name="instantSave">If true changes are applied</param>
        Task RemovePermanent(IEnumerable<TEntity> entities, bool instantSave = false);

        /// <summary>
        /// Permantly removes all matching items
        /// </summary>
        /// <param name="instantSave">If true changes are applied</param>
        Task RemovePermanent(Expression<Func<TEntity, bool>> predicate, bool instantSave = false);

        /// <summary>
        /// Returns a single entity and throws an exception if more or less than one entity found
        /// </summary>
        /// <param name="update">If true tracks changes to elements</param>
        Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate, bool update = false);

        /// <summary>
        /// Expects the query to result in a single element
        /// </summary>
        /// <param name="predicate">Query to run</param>
        /// <param name="update">If true tracks changes to elements</param>
        /// <returns>A single element if query only results in 1 element or Default if no match. Throws an exception if more than one element is found</returns>
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool update = false);

        /// <summary>
        /// Tracks changes to defined TEntity
        /// </summary>
        /// <param name="entity">Entity to track</param>
        void Update(TEntity entity);

        /// <summary>
        /// Tracks changes on all defined entities
        /// </summary>
        /// <param name="entities">Entities to track</param>
        void Update(IEnumerable<TEntity> entities);

        /// <summary>
        /// Tracls changes on all elements matching the predicate
        /// </summary>
        Task Update(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns the first element in query
        /// </summary>
        /// <param name="predicate">Query to run</param>
        /// <param name="update">If true tracks changes to elements</param>
        /// <returns>First element in resulting query</returns>
        Task<TEntity> First(Expression<Func<TEntity, bool>> predicate, bool update = false);

        /// <summary>
        /// Returns the last element in query
        /// </summary>
        /// <param name="predicate">Query to run</param>
        /// <param name="update">If true tracks changes to elements</param>
        /// <returns>The last element in resulting query</returns>
        Task<TEntity> Last(Expression<Func<TEntity, bool>> predicate, bool update = false);

        /// <summary>
        /// Returns the number of elements defined in Count starting with the first element
        /// </summary>
        /// <param name="count">Number of elements to return</param>
        /// <param name="update">If true tracks changes to elements</param>
        /// <returns>Max number of elements as defined in Count starting with row 0</returns>
        Task<IEnumerable<TEntity>> Take(int count, bool update = false);

        /// <summary>
        /// Runs a query and returns all elements
        /// </summary>
        /// <param name="predicate">Query to run</param>
        /// <returns>All resulting elements of defined Predicate</returns>
        Task<bool> All(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Returns all elements as List
        /// </summary>
        /// <returns>List containing all elements</returns>
        Task<IEnumerable<TEntity>> List();

        /// <summary>
        /// Commits all changes to the database
        /// </summary>
        Task<int> SaveChanges();

        /// <summary>
        /// The current DbSet
        /// </summary>
        DbSet<TEntity> DbSet { get; }
    }

    public sealed class DBContextUnitOfWork<TEntity> : DisposableBase, IDBContextUnitOfWork<TEntity> where TEntity : class, IDb
    {
        public DBContextUnitOfWork(IDBContextBase context) : base(Marshal.GetIUnknownForObject(context))
        {
            this.DBContext = (DBContextBase)context;
        }

        #region METHODS
        public async Task<int> SaveChanges()
        {
            var result = await this.DBContext.SaveChangesAsync();

            return result;
        }

        public async Task<IEnumerable<TEntity>> Where(Expression<Func<TEntity, bool>> predicate, bool update = false)
        {
            var result = await this.DbSet.Where(predicate).ToListAsync();

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public async Task<TEntity> Add(TEntity entity, bool instantSave)
        {
            await this.DBContext.AddAsync(entity);

            if (instantSave)
            {
                await SaveChanges();
            }

            return entity;
        }

        public async Task<IEnumerable<TEntity>> Add(IEnumerable<TEntity> entities, bool instantSave)
        {
            await this.DBContext.AddRangeAsync(entities);

            if (instantSave)
            {
                await SaveChanges();
            }

            return entities;
        }

        public async Task<TEntity> Remove(int id, bool instantSave)
        {
            TEntity e = await this.SingleOrDefault(k => k.Id.Equals(id));
            var result = await Remove(e, instantSave);

            return result;
        }

        public async Task<TEntity> Remove(TEntity entity, bool instantSave)
        {
            this.DBContext.Update(entity);

            if (instantSave)
            {
                await SaveChanges();
            }

            return entity;
        }

        public async Task<IEnumerable<TEntity>> Remove(IEnumerable<TEntity> entities, bool instantSave)
        {
            ICollection<TEntity> result = new Collection<TEntity>();

            foreach (TEntity i in entities)
            {
                var tmp = await this.Remove(i, instantSave);

                result.Add(tmp);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> Remove(Expression<Func<TEntity, bool>> predicate, bool instantSave)
        {
            var items = await this.Where(predicate);
            var result = await Remove(items, instantSave);

            return result;
        }

        public async Task RemovePermanent(int id, bool instantSave = false)
        {
            TEntity e = await this.SingleOrDefault(jd => jd.Id.Equals(id));

            await this.RemovePermanent(e, instantSave);
        }

        public async Task RemovePermanent(TEntity entity, bool instantSave = false)
        {
            this.DBContext.Remove(entity);

            if (instantSave)
            {
                await this.DBContext.SaveChangesAsync();
            }
        }

        public async Task RemovePermanent(IEnumerable<TEntity> entities, bool instantSave = false)
        {
            foreach (TEntity item in entities)
            {
                await this.RemovePermanent(item, instantSave);
            }
        }

        public async Task RemovePermanent(Expression<Func<TEntity, bool>> predicate, bool instantSave = false)
        {
            var items = await this.Where(predicate);

            await this.RemovePermanent(items, instantSave);
        }

        public async Task<TEntity> Single(Expression<Func<TEntity, bool>> predicate, bool update = false)
        {
            var result = await this.DbSet.SingleAsync(predicate);

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate, bool update = false)
        {
            var result = await this.DbSet.SingleOrDefaultAsync(predicate);

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public void Update(TEntity entity)
        {
            this.DBContext.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            foreach (TEntity o in entities)
            {
                Update(o);
            }
        }

        public async Task Update(Expression<Func<TEntity, bool>> predicate)
        {
            var items = await this.Where(predicate);
            Update(items);
        }

        public async Task<TEntity> First(Expression<Func<TEntity, bool>> predicate, bool update = false)
        {
            var result = await this.DbSet.FirstAsync(predicate);

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public async Task<TEntity> Last(Expression<Func<TEntity, bool>> predicate, bool update = false)
        {
            var result = await this.DbSet.LastAsync(predicate);

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public async Task<IEnumerable<TEntity>> Take(int count, bool update = false)
        {
            var tmp = this.DbSet.Take(count);
            var result = await tmp.ToListAsync();

            if (update)
            {
                Update(result);
            }

            return result;
        }

        public async Task<bool> All(Expression<Func<TEntity, bool>> predicate)
        {
            var result = await this.DbSet.AllAsync(predicate);

            return result;
        }

        public async Task<IEnumerable<TEntity>> List()
        {
            var result = await this.DbSet.ToListAsync();

            return result;
        }
        #endregion

        private DBContextBase DBContext { get; }
        public DbSet<TEntity> DbSet { get { return this.DBContext.Set<TEntity>(); } }
    }
}

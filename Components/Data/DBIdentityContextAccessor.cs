/*
    @Date			 : 28.11.2019
    @Author			 : Stein Lundbeck
*/

using LC.Components.Core.Data.Models.Identity;
using System.Threading.Tasks;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBIdentityContextAccessor
    {
        /// <summary>
        /// Unit of Work for TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type to pass to Unit of Work</typeparam>
        IDBIdentityContextUnitOfWork<TEntity> GetWorker<TEntity>() where TEntity : class, IDataIdentityEntityBase;

        /// <summary>
        /// Assets DB Context
        /// </summary>
        IDBIdentityContextBase DBContext { get; }

        /// <summary>
        /// Save changes to element of TEntity type
        /// </summary>
        /// <typeparam name="TEntity">Type of elements</typeparam>
        Task SaveChanges<TEntity>() where TEntity : class, IDataIdentityEntityBase;
    }

    /// <summary>
    /// Provides access to the Identity DB context and db workers
    /// </summary>
    public sealed class DBIdentityContextAccessor : IDBIdentityContextAccessor
    {
        private readonly IDBIdentityContextBase _context;

        public DBIdentityContextAccessor(IDBIdentityContextBase context)
        {
            _context = context;
        }
        public IDBIdentityContextUnitOfWork<TEntity> GetWorker<TEntity>() where TEntity : class, IDataIdentityEntityBase
        {
            return new DBIdentityContextUnitOfWork<TEntity>(this.DBContext);
        }

        public async Task SaveChanges<TEntity>() where TEntity : class, IDataIdentityEntityBase
        {
            await GetWorker<TEntity>().SaveChanges();
        }

        public IDBIdentityContextBase DBContext => _context;
    }
}

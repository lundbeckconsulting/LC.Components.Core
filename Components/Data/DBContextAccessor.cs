/*
    @Date			 : 28.11.2019
    @Author			 : Stein Lundbeck
*/

using LundbeckConsulting.Components.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBContextAccessor
    {
        /// <summary>
        /// Unit of Work for TEntity
        /// </summary>
        /// <typeparam name="TEntity">Type to pass to Unit of Work</typeparam>
        IDBContextUnitOfWork<TEntity> GetWorker<TEntity>() where TEntity : class, IDataEntityBase;

        /// <summary>
        /// Assets DB Context
        /// </summary>
        IDBContextBase DBContext { get; }

        /// <summary>
        /// Save changes to element of TEntity type
        /// </summary>
        /// <typeparam name="TEntity">Type of elements</typeparam>
        Task SaveChanges<TEntity>() where TEntity : class, IDataEntityBase;
    }

    /// <summary>
    /// Provides access to the DB context and db workers
    /// </summary>
    public sealed class DBContextAccessor : DisposableBase, IDBContextAccessor
    {
        private readonly IDBContextBase _context;

        public DBContextAccessor(IDBContextBase context) : base(Marshal.GetIUnknownForObject(context))
        {
            _context = context;
        }
        public IDBContextUnitOfWork<TEntity> GetWorker<TEntity>() where TEntity : class, IDataEntityBase
        {
            return new DBContextUnitOfWork<TEntity>(this.DBContext);
        }

        public async Task SaveChanges<TEntity>() where TEntity : class, IDataEntityBase
        {
            await GetWorker<TEntity>().SaveChanges();
        }

        public IDBContextBase DBContext => _context;
    }
}

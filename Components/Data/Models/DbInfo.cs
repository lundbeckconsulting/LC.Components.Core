/*
    @Date			: 29.07.2021
    @Author         : Stein Lundbeck
*/

namespace LundbeckConsulting.Components.Core.Components.Data.Models
{
    public interface IDbInfo : IDb
    {
        string Name { get; set; }
        string Description { get; set; }
    }

    public abstract class DbInfo : Db, IDb, IDbInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

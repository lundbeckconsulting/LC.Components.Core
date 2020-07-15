/*
    @Date			 : 15.07.2020
    @Author			 : Stein Lundbeck
*/

using LC.Components.Core.Data.Models.Identity;
using LundbeckConsulting.Components.Data;
using System.Collections.Generic;

namespace LundbeckConsulting.Components.Core.Data.Models.Identity
{
    public interface IAppUserClaimCollection : IDataIdentityEntityBase
    {
        int AppId { get; set; }
        string Name { get; set; }
        string Ref { get; set; }
        string Description { get; set; }
        IEnumerable<AppUserClaimCollectionEntry> Entries { get; set; }
    }

    public class AppUserClaimCollection : DataIdentityEntityBase, IAppUserClaimCollection, IDataEntityBase, IDataIdentityEntityBase
    {
        public int AppId { get; set; }
        public string Name { get; set; }
        public string Ref { get; set; }
        public string Description { get; set; }
        public IEnumerable<AppUserClaimCollectionEntry> Entries { get; set; }
    }
}

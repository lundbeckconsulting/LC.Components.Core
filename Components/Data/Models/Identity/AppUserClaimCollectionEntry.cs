/*
    @Date			 : 15.07.2020
    @Author			 : Stein Lundbeck
*/

using LC.Components.Core.Data.Models.Identity;
using LundbeckConsulting.Components.Data;

namespace LundbeckConsulting.Components.Core.Data.Models.Identity
{
    public interface IAppUserClaimCollectionEntry : IDataIdentityEntityBase
    {
        int AppUserClaimCollectionId { get; set; }
        string Issuer { get; set; }
        string Value { get; set; }
        string Description { get; set; }
    }

    public class AppUserClaimCollectionEntry : DataIdentityEntityBase, IAppUserClaimCollectionEntry, IDataEntityBase, IDataIdentityEntityBase
    {
        public int AppUserClaimCollectionId { get; set; }
        public string Issuer { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
    }
}


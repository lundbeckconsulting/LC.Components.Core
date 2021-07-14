/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Data.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace LC.Components.Core.Data
{
    public interface IDBIdentityContextBaseDbSets
    {
        DbSet<User> Users { get; set; }
        DbSet<UserLogin> UserLogins { get; set; }
        DbSet<UserClaim> UserClaims { get; set; }
        DbSet<UserRole> UserRoles { get; set; }
        DbSet<UserToken> UserTokens { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<RoleClaim> RoleClaims { get; set; }
        DbSet<AppUserClaimCollectionEntry> AppUserClaimCollectionEntry { get; set; }
        DbSet<AppUserClaimCollection> AppUserClaimCollection { get; set; }
    }

    public partial class DBIdentityContextBase : IDBIdentityContextBaseDbSets
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserLogin> UserLogins { get; set; }
        public DbSet<UserClaim> UserClaims { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleClaim> RoleClaims { get; set; }
        public DbSet<AppUserClaimCollectionEntry> AppUserClaimCollectionEntry { get; set; }
        public DbSet<AppUserClaimCollection> AppUserClaimCollection { get; set; }
    }
}

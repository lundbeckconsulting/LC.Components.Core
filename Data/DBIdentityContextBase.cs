/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Data.Models.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LundbeckConsulting.Components.Core.Data
{
    public interface IDBIdentityContextBase
    {
        IConfiguration Configuration { get; }
    }

    public partial class DBIdentityContextBase : IdentityDbContext<User, Role, int, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        private readonly IConfiguration _config;

        public DBIdentityContextBase() { }

        public DBIdentityContextBase(DbContextOptions<DBIdentityContextBase> options, IConfiguration config) : base(options) 
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(opt => opt.ToTable("IdentityUser"));
            builder.Entity<Role>(opt => opt.ToTable("IdentityRole"));
            builder.Entity<UserRole>(opt => opt.ToTable("IdentityUserRole"));
            builder.Entity<UserClaim>(opt => opt.ToTable("IdentityUserClaim"));
            builder.Entity<UserToken>(opt => opt.ToTable("IdentityUserToken"));
            builder.Entity<RoleClaim>(opt => opt.ToTable("IdentityRoleClaim"));
            builder.Entity<UserLogin>(opt => opt.ToTable("IdentityUserLogin"));
            builder.Entity<AppUserClaimCollectionEntry>(opt => opt.ToTable("IdentityAppUserClaimCollectionEntry"));
            builder.Entity<AppUserClaimCollection>(opt => opt.ToTable("IdentityAppUserClaimCollection"));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);

            builder.UseSqlServer(this.Configuration.GetConnectionString("Default"));
        }

        public IConfiguration Configuration => _config;
    }
}

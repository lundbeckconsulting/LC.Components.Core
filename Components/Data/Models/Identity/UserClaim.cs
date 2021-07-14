/*
    @Date			 : 15.07.2020
    @Author			 : Stein Lundbeck
*/

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using LC.Components.Core.Data.Models.Identity;
using LundbeckConsulting.Components.Data;
using Microsoft.AspNetCore.Identity;

namespace LundbeckConsulting.Components.Core.Data.Models.Identity
{
    public interface IUserClaim : IDataEntityBase
    {
        /// <summary>
        /// Id of user who owns the claim
        /// </summary>
        int UserId { get; set; }
    }

    public class UserClaim : IdentityUserClaim<int>, IUserClaim, IDataIdentityEntityBase, IDataEntityBase
    {
        public UserClaim() { }

        public UserClaim(int userId, string type, string value) : this(userId, new Claim(type, value)) 
        {
            
        }

        public UserClaim(int userId, Claim claim) {
            this.UserId = userId;
            this.ClaimType = claim.Type;
            this.ClaimValue = claim.Value;
        }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [NotMapped]
        public bool Active { get; set; }

        [NotMapped]
        public DateTime? DateDeleted { get; set; }

        [NotMapped]
        public Guid UId { get; set; }
    }
}

/*
    @Date			 : 15.07.2020
    @Author			 : Stein Lundbeck
*/

using LC.Components.Core.Data.Models.Identity;
using LundbeckConsulting.Components.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LundbeckConsulting.Components.Core.Data.Models.Identity
{
    public class UserRole : IdentityUserRole<int>, IDataIdentityEntityBase, IDataEntityBase
    {
        public UserRole() { }

        public UserRole(int userId, int roleId)
        {
            this.RoleId = roleId;
            this.UserId = userId;
        }

        [NotMapped]
        public int Id { get; set; } = -1;
        [NotMapped]
        public bool Active { get; set; } = true;
        [NotMapped]
        public Guid UId { get; set; } = Guid.NewGuid();
        [PersonalData]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [NotMapped]
        public DateTime? DateDeleted { get; set; }
    }
}

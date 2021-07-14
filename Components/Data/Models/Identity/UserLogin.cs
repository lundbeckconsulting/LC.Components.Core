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
    public class UserLogin : IdentityUserLogin<int>, IDataIdentityEntityBase, IDataEntityBase
    {
        [NotMapped]
        public int Id { get; set; }

        [NotMapped]
        public bool Active { get; set; }

        [NotMapped]
        public Guid UId { get; set; }

        [NotMapped]
        public DateTime DateCreated { get; set; }

        [NotMapped]
        public DateTime? DateDeleted { get; set; }
    }
}

/*
    @Date			 : 15.07.2020
    @Author			 : Stein Lundbeck
*/

using LC.Components.Core.Data.Models.Identity;
using LundbeckConsulting.Components.Data;
using Microsoft.AspNetCore.Identity;
using System;

namespace LundbeckConsulting.Components.Core.Data.Models.Identity
{
    public interface IUser : IDataEntityBase
    {
        string Token { get; set; }
    }

    public class User : IdentityUser<int>, IUser, IDataIdentityEntityBase, IDataEntityBase
    {
        [PersonalData]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        [PersonalData]
        public Guid UId { get; set; } = Guid.NewGuid();

        [PersonalData]
        public string Token { get; set; }
    }
}

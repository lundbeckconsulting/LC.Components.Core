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
    public class Role : IdentityRole<int>, IDataIdentityEntityBase, IDataEntityBase
    {
        public Role() { }

        public Role(int siteId, string name)
        {
            this.SiteId = siteId;
            this.Name = name;
        }

        [NotMapped]
        public bool Active { get; set; } = true;

        [NotMapped]
        public Guid UId { get; set; } = Guid.NewGuid();
        
        [PersonalData]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        
        [NotMapped]
        public DateTime? DateDeleted { get; set; }

        [PersonalData]
        public int SiteId { get; set; }
    }
}

/*
    @Date			: 29.07.2021
    @Author         : Stein Lundbeck
*/

using System;

namespace LundbeckConsulting.Components.Core.Components.Data.Models
{
    public interface IDb
    {
        /// <summary>
        /// Element id
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Element unique id
        /// </summary>
        Guid UId { get; set; }

        /// <summary>
        /// Time when created
        /// </summary>
        DateTime DateCreated { get; set; }
    }

    public abstract class Db : IDb
    {
        public int Id { get; set; }
        public Guid UId { get; set; } = Guid.NewGuid();
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}

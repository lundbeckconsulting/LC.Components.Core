/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Data;

namespace LC.Components.Core.Data.Models.Identity
{
    public interface IDataIdentityEntityBase : IDataEntityBase
    {

    }

    public abstract class DataIdentityEntityBase : DataEntityBase, IDataIdentityEntityBase
    {
    }
}

/*
    @Date			              : 10.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Sample type for demonstration
*/

namespace LundbeckConsulting.Components.Core.Data.Models
{
    public interface ISampleTypeOne : IDataEntityBase
    {
        bool IsActive { get; set; }
    }

    public sealed class SampleTypeOne : DataEntityBase, ISampleTypeOne
    {
        public bool IsActive { get; set; }
    }
}

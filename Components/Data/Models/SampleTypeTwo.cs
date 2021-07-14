/*
    @Date			              : 10.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Sample type for demonstration
*/

using LundbeckConsulting.Components.Data;

namespace LundbeckConsulting.Components.Core.Data.Models
{
    public interface ISampleTypeTwo : IDataEntityBase
    {
        string Name { get; set; }
    }

    public sealed class SampleTypeTwo : DataEntityBase, ISampleTypeTwo
    {
        public string Name { get; set; }
    }
}

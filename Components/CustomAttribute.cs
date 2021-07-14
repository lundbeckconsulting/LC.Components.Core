/*
    @Date			: 29.03.2021
    @Author         : Stein Lundbeck
*/

namespace LundbeckConsulting.Components.Core.Components
{
    public interface ICustomAttribute
    {
        string Name { get; set; }
        string Value { get; set; }
    }

    public class CustomAttribute : ICustomAttribute
    {
        public CustomAttribute()
        {

        }

        public CustomAttribute(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public string Value { get; set; }
    }
}

/*
    @Date			              : 16.07.2020
    @Author                       : Stein Lundbeck
*/

namespace LundbeckConsulting.Components.Core
{
    public interface ITagBuilderCustomAttribute : IAttributeCustom
    {
        /// <summary>
        /// Indicates if the attribute value should merge with possible existing element
        /// </summary>
        bool Merge { get; }
    }

    /// <summary>
    /// Custom tag builder attribute element
    /// </summary>
    public class TagBuilderCustomAttribute : AttributeCustom, ITagBuilderCustomAttribute
    {
        private readonly bool _merge;

        /// <summary>
        /// Custom attribute element
        /// </summary>
        /// <remarks>The attribute isn't encoded and merges with existing</remarks>
        /// <param name="name">Name of attribute</param>
        /// <param name="value">Attribute value</param>
        public TagBuilderCustomAttribute(string name, string value) : this(name, value, false)
        {
            
        }

        /// <summary>
        /// Custom attribute element
        /// </summary>
        /// <remarks>The attribute merges with existing</remarks>
        /// <param name="name">Name of attribute</param>
        /// <param name="value">Attribute value</param>
        /// <param name="encode">Indicates if the attribute will be encoded</param>
        public TagBuilderCustomAttribute(string name, string value, bool encode) : this(name, value, encode, true)
        {

        }

        /// <summary>
        /// Custom attribute element
        /// </summary>
        /// <param name="name">Name of attribute</param>
        /// <param name="value">Attribute value</param>
        /// <param name="encode">Indicates if the attribute will be encoded</param>
        /// <param name="merge">Indicates if the value will merge with existing element</param>
        public TagBuilderCustomAttribute(string name, string value, bool encode, bool merge) : base(name, value, encode)
        {
            _merge = merge;
        }

        public bool Merge => _merge;
    }
}

/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

namespace LundbeckConsulting.Components.Core.TagHelpers
{
    public interface ITagHelperCustomAttribute : ITagBuilderCustomAttribute
    {
        /// <summary>
        /// Indicates if the attribute should merge if an equal exists
        /// </summary>
        bool Merge { get; }
    }

    public sealed class TagHelperCustomAttribute : TagBuilderCustomAttribute, ITagHelperCustomAttribute
    {
        private readonly bool _merge;

        /// <summary>
        /// Creates a custom attribute that doesn't merge or won't be encoded
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        public TagHelperCustomAttribute(string name, string value) : base(name, value, false)
        {
            
        }

        /// <summary>
        /// Creates a custom attribute that doesn't merge
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        /// <param name="encode">Indicates if the value should be encoded</param>
        public TagHelperCustomAttribute(string name, string value, bool encode) : this(name, value, encode, false)
        {

        }

        /// <summary>
        /// Creates a custom attribute element
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        /// <param name="encode">Indicates if the value should be encoded</param>
        /// <param name="merge">Indicates if the value should merge if an equal attribute exists</param>
        public TagHelperCustomAttribute(string name, string value, bool encode, bool merge) : base(name, value, encode)
        {
            _merge = merge;
        }

        public bool Merge => _merge;
    }
}

/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LundbeckConsulting.Components.Core.TagHelpers
{
    public interface ITagHelperCustomAttribute : IAttributeCustom
    {

    }

    /// <summary>
    /// Custom tag helper attribute element for custom content types
    /// </summary>
    public sealed class TagHelperCustomAttribute : AttributeCustom, ITagHelperCustomAttribute
    {
        /// <summary>
        /// Creates a new attribute based on a custom tag builder attribute
        /// </summary>
        /// <param name="attribute">Attribute to base element on</param>
        public TagHelperCustomAttribute(IAttributeCustom attribute) : this(attribute.Name, attribute.Value, attribute.Encode, attribute.ValueStyle)
        {

        }

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
        public TagHelperCustomAttribute(string name, string value, bool encode) : this(name, value, encode, HtmlAttributeValueStyle.DoubleQuotes)
        {

        }

        /// <summary>
        /// Creates a custom attribute element
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        /// <param name="encode">Indicates if the value should be encoded</param>
        /// <param name="merge">Indicates if the value should merge if an equal attribute exists</param>
        public TagHelperCustomAttribute(string name, string value, bool encode, HtmlAttributeValueStyle valueStyle) : base(name, value, encode)
        {
            this.ValueStyle = valueStyle;
        }
    }
}

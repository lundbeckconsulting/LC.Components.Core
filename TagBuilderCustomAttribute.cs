/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;

namespace LundbeckConsulting.Components.Core
{
    public interface ITagBuilderCustomAttribute
    {
        /// <summary>
        /// Name of the attribute
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Value of the attribute
        /// </summary>
        string Value { get; }
        
        /// <summary>
        /// Indicates if the should be encoded when added
        /// </summary>
        bool Encode { get; }

        /// <summary>
        /// The value style of the attribute
        /// </summary>
        HtmlAttributeValueStyle ValueStyle { get; set; }
        
        /// <summary>
        /// A TagHelperAttribute representation 
        /// </summary>
        TagHelperAttribute Attribute { get; }
    }

    public class TagBuilderCustomAttribute : ITagBuilderCustomAttribute
    {
        private readonly string _name;
        private readonly string _value;
        private readonly bool _encode;

        /// <summary>
        /// New custom attribute based on name and value. Value will not be encoded
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        public TagBuilderCustomAttribute(string name, string value) : this(name, value, false)
        {

        }

        /// <summary>
        /// New custom attribute based on name, value and encode
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        /// <param name="encode">Indicates if the value should be encoded when added</param>
        public TagBuilderCustomAttribute(string name, string value, bool encode)
        {
            _name = name;
            _value = value;
            _encode = encode;
        }

        public string Name => _name;
        public string Value => _value;
        public bool Encode => _encode;
        public HtmlAttributeValueStyle ValueStyle { get; set; } = HtmlAttributeValueStyle.DoubleQuotes;
        public TagHelperAttribute Attribute
        {
            get
            {
                string val = this.Encode ? WebUtility.HtmlEncode(this.Value) : this.Value;

                return new TagHelperAttribute(this.Name, this.Value, this.ValueStyle);
            }
        }
    }
}

/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Net;

namespace LundbeckConsulting.Components.Core
{
    public interface IAttributeCustom
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

    public class AttributeCustom : IAttributeCustom
    {
        private readonly string _name;
        private readonly string _value;
        private readonly bool _encode;

        /// <summary>
        /// New custom attribute based
        /// </summary>
        /// <remarks>Value doesn't merge with existing attribute element</remarks>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        public AttributeCustom(string name, string value) : this(name, value, false)
        {

        }

        /// <summary>
        /// New custom attribute
        /// </summary>
        /// <param name="name">Name of the attribute</param>
        /// <param name="value">Value of the attribute</param>
        /// <param name="encode">Indicates if the value should be encoded when added</param>
        /// <param name="merge">Indicates if the value should be merged with possible existing attribute</param>
        public AttributeCustom(string name, string value, bool encode)
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

                return new TagHelperAttribute(this.Name, val, this.ValueStyle);
            }
        }
    }
}

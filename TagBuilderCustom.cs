/*
    @Date			: 15.07.2020
    @Author         : Stein Lundbeck
*/

using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using AttributeDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.AttributeDictionary;

namespace LundbeckConsulting.Components.Core
{
    public interface ITagBuilderCustom : IHtmlContent
    {
        /// <summary>
        /// If true a new line will be added after the tag builder content. Default is true
        /// </summary>
        bool ApplyNewLine { get; set; }

        /// <summary>
        /// Adds a child tag element
        /// </summary>
        /// <param name="child"></param>
        void AddChild(ITagBuilderCustom child);

        /// <summary>
        /// Adds multiple tag elements
        /// </summary>
        /// <param name="children">Elements to add</param>
        void AddChildRange(params ITagBuilderCustom[] children);

        /// <summary>
        /// Adds names of tag attributes to consume
        /// </summary>
        /// <param name="name"></param>
        void AddAttributesToConsume(params string[] names);

        /// <summary>
        /// Gets the attribute with equal name
        /// </summary>
        /// <param name="name">Name of attribute</param>
        /// <returns>Attribute with equal name</returns>
        ITagBuilderCustomAttribute GetAttribute(string name);

        /// <summary>
        /// Removes all content and attributes
        /// </summary>
        void Clear();

        /// <summary>
        /// Tag consumes attributes if true
        /// </summary>
        bool ConsumeAttributes { get; set; }

        /// <summary>
        /// A list with the name of attributes to consume if ConsumeAttributes is true. If no elements in collection all attributes are consumed
        /// </summary>
        IEnumerable<string> AttributesToConsume { get; }

        /// <summary>
        /// Adds content to inner HTML
        /// </summary>
        /// <param name="content">Content element to add</param>
        void AddInnerContent(ITagBuilderCustomInnerContent content);

        /// <summary>
        /// Adds inner content that will be appended
        /// </summary>
        /// <param name="content">Content value</param>
        /// <param name="encode">Specifies if the content should be encoded. Default is false</param>
        void AddInnerContent(string content, bool encode = false);

        /// <summary>
        /// Adds an attribute based on name and value
        /// </summary>
        /// <param name="name">Name of attribute</param>
        /// <param name="value">Value of attribute</param>
        /// <param name="encode">Indicates if the value should be encoded</param>
        void AddAttribute(string name, string value, bool encode = false);

        /// <summary>
        /// Adds an attribute
        /// </summary>
        /// <param name="attribute">Attribute to add</param>
        void AddAttribute(ITagBuilderCustomAttribute attribute);

        /// <summary>
        /// Add a range of attributes
        /// </summary>
        /// <param name="attributes">Elements to add</param>
        void AddAttributeRange(IEnumerable<ITagBuilderCustomAttribute> attributes);

        /// <summary>
        /// Adds a collection of attributes based on the collection of name and values
        /// </summary>
        /// <param name="attributes">Name and values of attributes to add</param>
        /// <param name="encode">Indicates if the value will be encoded</param>
        void AddAttributeRange(IDictionary<string, string> attributes, bool encode = false);

        /// <summary>
        /// Indicates if an attribute with the name exists
        /// </summary>
        /// <param name="name">Name to validate</param>
        /// <returns>True if an attribute with equal name exists</returns>
        bool AttributeExists(string name);

        /// <summary>
        /// Converts custom attribute to and adds standard tag builder attributes
        /// </summary>
        /// <returns>Count of converted attributes</returns>
        int ConvertCustomAttributes();

        /// <summary>
        /// Adds a CSS class
        /// </summary>
        /// <param name="value">Name of CSS class</param>
        void AddCssClass(string value);

        /// <summary>
        /// Adds all elements in collection to the class attribute
        /// </summary>
        /// <param name="values">Values to add</param>
        void AddCssClassRange(params string[] values);

        /// <summary>
        /// List of inner content elements
        /// </summary>
        IEnumerable<ITagBuilderCustomInnerContent> InnerContent { get; }

        /// <summary>
        /// Inner HTML
        /// </summary>
        IHtmlContentBuilder InnerHtml { get; }

        /// <summary>
        /// Specifies if the tag should be encoded. Default is true
        /// </summary>
        bool Encode { get; set; }

        /// <summary>
        /// Where to render the element
        /// </summary>
        ContentPosition Position { get; set; }

        /// <summary>
        /// How to render the html
        /// </summary>
        HtmlRender Render { get; set; }

        /// <summary>
        /// List of children elements
        /// </summary>
        IEnumerable<ITagBuilderCustom> Children { get; }

        /// <summary>
        /// The attribute elements
        /// </summary>
        IEnumerable<ITagBuilderCustomAttribute> CustomAttributes { get; }

        AttributeDictionary Attributes { get; }
    }

    /// <summary>
    /// Element that inherits TagBuilder and offer special custom functionality
    /// </summary>
    public class TagBuilderCustom : TagBuilder, ITagBuilderCustom
    {
        private readonly ICollection<string> _consumeAttr = new Collection<string>();
        private readonly ICollection<ITagBuilderCustom> _content = new Collection<ITagBuilderCustom>();
        private readonly ICollection<ITagBuilderCustomAttribute> _attributes = new Collection<ITagBuilderCustomAttribute>();
        private readonly ICollection<ITagBuilderCustomInnerContent> _innerContent;

        /// <summary>
        /// Creates a new element that will be added at post element and not encoded
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        public TagBuilderCustom(string tagName) : this(tagName, ContentPosition.PostElement, false)
        {
            
        }

        /// <summary>
        /// Creates a new element with attributes
        /// </summary>
        /// <param name="tagName"></param>
        /// <param name="encoded">Indicates if the tag must be encoded</param>
        /// <param name="attributes">Name and value of attributes to add</param>
        public TagBuilderCustom(string tagName, bool encoded, IDictionary<string, string> attributes) : this(tagName, encoded)
        {
            AddAttributeRange(attributes);
        }

        /// <summary>
        /// Creates a new element that's not encoded
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="mode">Tag render mode</param>
        public TagBuilderCustom(string tagName, TagRenderMode mode) : this(tagName, mode, false)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element that will be added post element
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="encode">Indicates if the tag must be encoded</param>
        public TagBuilderCustom(string tagName, bool encode) : this(tagName, ContentPosition.PostElement, encode)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element with normal tag render mode
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="position">Where the tag will rendered</param>
        /// <param name="encode">Tag will be encoded if true</param>
        public TagBuilderCustom(string tagName, ContentPosition position, bool encode) : this(tagName, position, TagRenderMode.Normal, encode)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="mode">Tag render mode</param>
        /// <param name="encode">Tag will be encoded if true</param>
        public TagBuilderCustom(string tagName, TagRenderMode mode, bool encode) : this(tagName, ContentPosition.PostElement, mode, false, encode)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="position">Where the tag will rendered</param>
        /// <param name="mode">Tag render mode</param>
        /// <param name="encode">Tag will be encoded if true</param>
        public TagBuilderCustom(string tagName, ContentPosition position, TagRenderMode mode, bool encode) : this(tagName, position, mode, false, encode)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="position">Where the tag will rendered</param>
        /// <param name="mode">Tag render mode</param>
        /// <param name="consumeAttributes">List of names of attributes to consume</param>
        /// <param name="encode">Tag will be encoded if true</param>
        public TagBuilderCustom(string tagName, ContentPosition position, TagRenderMode mode, bool consumeAttributes, bool encode) : this(tagName, position, mode, consumeAttributes, new string[0], encode, true)
        {

        }

        /// <summary>
        /// Creates a new TagHelperCustom element
        /// </summary>
        /// <param name="tagName">Tag type name</param>
        /// <param name="position">Where the tag will rendered</param>
        /// <param name="mode">Tag render mode</param>
        /// <param name="consumeAttributes">If true element consumes attributes</param>
        /// <param name="attributesToConsume">List of names of attributes to consume if consumeAttributes is true. Consumes all attributes if set to default</param>
        /// <param name="encode">Tag will be encoded if true</param>
        /// <param name="applyNewLine">Indicates to add a new line after rendering tag</param>
        public TagBuilderCustom(string tagName, ContentPosition position, TagRenderMode mode, bool consumeAttributes, IEnumerable<string> attributesToConsume, bool encode, bool applyNewLine) : base(tagName)
        {
            _innerContent = new Collection<ITagBuilderCustomInnerContent>();

            this.TagRenderMode = mode;
            this.Encode = encode;
            this.ConsumeAttributes = consumeAttributes;
            this.Position = position;
            this.ApplyNewLine = applyNewLine;

            if (!attributesToConsume.Zero())
            {
                _consumeAttr.AddRange(attributesToConsume);
            }
        }

        public void AddChild(ITagBuilderCustom child)
        {
            _content.Add(child);
        }

        public void AddChildRange(params ITagBuilderCustom[] children)
        {
            foreach (ITagBuilderCustom child in children)
            {
                AddChild(child);
            }
        }

        public void AddAttributesToConsume(params string[] names)
        {
            _consumeAttr.AddRange(names);
        }

        public new void AddCssClass(string value) => base.AddCssClass(value);

        public void AddCssClassRange(params string[] values)
        {
            foreach (string attr in values)
            {
                AddCssClass(attr);
            }
        }

        public bool AttributeExists(string name) => _attributes.Exists(attr => attr.Name == name);

        public void AddAttribute(string name, string value, bool encode = false) => AddAttribute(new TagBuilderCustomAttribute(name, value, encode));

        public void AddAttribute(ITagBuilderCustomAttribute attribute) => _attributes.Add(attribute);

        public void AddAttributeRange(IEnumerable<ITagBuilderCustomAttribute> attributes)
        {
            foreach(ITagBuilderCustomAttribute attr in attributes)
            {
                AddAttribute(attr);
            }
        }

        public void AddAttributeRange(IDictionary<string, string> attributes, bool encode = false)
        {
            foreach (KeyValuePair<string, string> attr in attributes)
            {
                AddAttribute(new TagBuilderCustomAttribute(attr.Key, attr.Value, encode));
            }
        }

        public int ConvertCustomAttributes()
        {
            foreach(IAttributeCustom attr in this.CustomAttributes)
            {
                this.Attributes.Add(attr.Attribute);
            }

            return this.CustomAttributes.Count();
        }

        public void AddInnerContent(ITagBuilderCustomInnerContent content) => _innerContent.Add(content);

        public void AddInnerContent(string content, bool encode = false) => AddInnerContent(new TagBuilderCustomInnerContent() { Content = new HtmlString(content), Encode = encode, AppendContent = true });

        public ITagBuilderCustomAttribute GetAttribute(string name) => _attributes.SingleOrDefault(attr => attr.Name == name);

        public void Clear()
        {
            _content.Clear();
            _attributes.Clear();
        }

        public bool ApplyNewLine { get; set; } = true;
        public bool ConsumeAttributes { get; set; } = true;
        public IEnumerable<string> AttributesToConsume => _consumeAttr;
        public IEnumerable<ITagBuilderCustomInnerContent> InnerContent => _innerContent;
        public IEnumerable<ITagBuilderCustom> Children => _content;
        public IEnumerable<ITagBuilderCustomAttribute> CustomAttributes => _attributes;
        public bool Encode { get; set; } = false;
        public ContentPosition Position { get; set; } = ContentPosition.PostElement;
        public HtmlRender Render { get; set; } = HtmlRender.Cascade;
    }
}

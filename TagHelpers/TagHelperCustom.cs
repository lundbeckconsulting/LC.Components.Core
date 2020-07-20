/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using ViewDataDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary;

namespace LundbeckConsulting.Components.Core.TagHelpers
{
    public interface ITagHelperCustom : ITagHelper, ITagHelperCustomBaseAttributes
    {
        /// <summary>
        /// Initiates the custom tag helper
        /// </summary>
        /// <param name="context">Current context of derived tag helper</param>
        /// <param name="output">Current output of derived tag helper</param>
        /// <param name="suppressOutput">Value of Output.SuppressOutput</param>
        Task PreProcess(TagHelperContext context, TagHelperOutput output, bool suppressOutput = true);

        /// <summary>
        /// Processes content, base attributes. Doesn't keep original attributes and doesn't exclude any attribute
        /// </summary>
        Task ProcessCustom();

        /// <summary>
        /// Processes content, base attributes and doesn't keep original attributes
        /// </summary>
        /// <param name="attributeToExclude">Name of attributes that shouldn't be included</param>
        Task ProcessCustom(IEnumerable<string> attributeToExclude);

        /// <summary>
        /// Processes content and attributes
        /// </summary>
        /// <param name="keepAttributes">Indicates if original attributes will be included</param>
        /// <param name="attributesToExclude">Names of attributes to exclude</param>
        Task ProcessCustom(bool keepAttributes = false, IEnumerable<string> attributesToExclude = default);

        /// <summary>
        /// Current tag helper context
        /// </summary>
        TagHelperContext Context { get; }

        /// <summary>
        /// Current tag helper output
        /// </summary>
        TagHelperOutput Output { get; }

        /// <summary>
        /// Add content
        /// </summary>
        /// <param name="tag">Element to add</param>
        void AddContent(ITagBuilderCustom tag);

        /// <summary>
        /// Add content
        /// </summary>
        /// <param name="tag">Element to add</param>
        /// <param name="position">Where to add the element</param>
        void AddContent(ITagBuilderCustom tag, ContentPositions position = ContentPositions.PostElement);

        /// <summary>
        /// Add multiple content elements
        /// </summary>
        /// <param name="contents">Elements to add</param>
        void AddContentRange(params ITagBuilderCustom[] contents);

        /// <summary>
        /// Add multiple content element set with specified position
        /// </summary>
        /// <param name="position">Where to render the content</param>
        /// <param name="contents">Elements to add</param>
        void AddContentRange(ContentPositions position, params ITagBuilderCustom[] contents);

        /// <summary>
        /// Adds an attribute element
        /// </summary>
        /// <param name="attribute">Element to add</param>
        void AddAttribute(ITagHelperCustomAttribute attribute);

        /// <summary>
        /// Adds multiple attributes
        /// </summary>
        /// <param name="attributes">Elements to add</param>
        void AddAttributeRange(params ITagHelperCustomAttribute[] attributes);

        /// <summary>
        /// Indicates if an attribute with equal name exists
        /// </summary>
        /// <param name="name">Name to validate</param>
        /// <returns>True if an attribute with equal name exists</returns>
        bool AttributeExists(string name);

        /// <summary>
        /// Indicates if an equal attribute exists
        /// </summary>
        /// <param name="attribute">Attribute to validate</param>
        /// <returns>True if an attribute with same name exists</returns>
        bool AttributeExists(ITagHelperCustomAttribute attribute);

        /// <summary>
        /// Removes an attribute element with equal name
        /// </summary>
        /// <param name="name">Name of attribute to remove</param>
        void RemoveAttribute(string name);

        /// <summary>
        /// Removes an equal attribute element
        /// </summary>
        /// <param name="attribute">Attribute element to remove</param>
        void RemoveAttribute(ITagHelperCustomAttribute attribute);

        /// <summary>
        /// Removes a collection of attribute elements
        /// </summary>
        /// <param name="attributes">Elements to remove</param>
        void RemoveAttributeRange(params ITagHelperCustomAttribute[] attributes);

        /// <summary>
        /// Replaces an attribute element
        /// </summary>
        /// <param name="attribute">Element to replace with</param>
        void ReplaceAttribute(ITagHelperCustomAttribute attribute);

        /// <summary>
        /// Gets an attribute element with equal name
        /// </summary>
        /// <param name="name">Name of attribute</param>
        /// <returns>Attribute with equal name</returns>
        ITagHelperCustomAttribute GetAttribute(string name);

        /// <summary>
        /// The attribute collection
        /// </summary>
        IEnumerable<ITagHelperCustomAttribute> Attributes { get; }

        /// <summary>
        /// List of content elements
        /// </summary>
        IEnumerable<ITagBuilderCustom> Content { get; }

        /// <summary>
        /// View Context used by view
        /// </summary>
        /// <returns>Current View Context</returns>
        ViewContext ViewContext { get; }

        /// <summary>
        /// Data from current View Context
        /// </summary>
        /// <returns>View Contex's View Data</returns>
        ViewDataDictionary ViewData { get; }

        /// <summary>
        /// The culture used by current thread
        /// </summary>
        CultureInfo CurrentCulture { get; }

        /// <summary>
        /// Clears all content and attribute elements
        /// </summary>
        void Clear();

        /// <summary>
        /// The web host environment
        /// </summary>
        IWebHostEnvironment Environment { get; }

        /// <summary>
        /// Element of type IHtmlHelper
        /// </summary>
        IHtmlHelper HtmlHelper { get; }

        /// <summary>
        /// Content of original base tag
        /// </summary>
        TagHelperContent InnerContent { get; }

        /// <summary>
        /// Tag helper repo
        /// </summary>
        ITagHelperRepo HelperRepo { get; }
    }

    /// <summary>
    /// Base class for custom tag helpers
    /// </summary>
    public abstract partial class TagHelperCustom : TagHelper, ITagHelperCustom
    {
        private readonly ICollection<ITagBuilderCustom> _content = new Collection<ITagBuilderCustom>();
        private readonly ICollection<ITagHelperCustomAttribute> _attributes = new Collection<ITagHelperCustomAttribute>();
        private readonly IWebHostEnvironment _env;
        private readonly HtmlRender _render = HtmlRender.Cascade;
        private readonly ITagHelperRepo _repo;
        private readonly IHtmlHelper _htmlHelper;

        private TagHelperContext _context;
        private TagHelperOutput _output;
        private TagHelperContent _innerContent = default;
        private bool _preProcessed = false;

        public TagHelperCustom(IWebHostEnvironment environment, ITagHelperRepo tagHelperRepo, IHtmlHelper htmlHelper)
        {
            _env = environment;
            _repo = tagHelperRepo;
            _htmlHelper = htmlHelper;
        }

        public async Task PreProcess(TagHelperContext context, TagHelperOutput output, bool suppressOutput = true)
        {
            if (_preProcessed)
            {
                throw new InvalidOperationException("PreProcess has already been invoked and can only be invoked once");
            }

            (this.HtmlHelper as IViewContextAware).Contextualize(this.ViewContext);

            _context = context;
            _output = output;
            _innerContent = await this.Output.GetChildContentAsync();

            if (suppressOutput)
            {
                this.Output.SuppressOutput();
            }

            _preProcessed = true;
        }

        public async Task ProcessCustom() => await ProcessCustom(default);

        public async Task ProcessCustom(IEnumerable<string> excludeAttributes) => await ProcessCustom(false, excludeAttributes);

        public async Task ProcessCustom(bool keepAttributes, IEnumerable<string> excludeAttributes = default)
        {
            if (!_preProcessed)
            {
                throw new InvalidOperationException("The function PreProcess must be invoked before the calling Process");
            }

            this.HelperRepo.ProcessCustomTagHelper(this, keepAttributes, excludeAttributes);

            await base.ProcessAsync(this.Context, this.Output);
        }

        public void AddContent(ITagBuilderCustom tag)
        {
            _content.Add(tag);
        }

        public void AddContent(ITagBuilderCustom tag, ContentPositions position = ContentPositions.PostElement)
        {
            tag.Position = position;

            _content.Add(tag);
        }

        public void AddContentRange(params ITagBuilderCustom[] content)
        {
            AddContentRange(ContentPositions.PostElement, content);
        }

        public void AddContentRange(ContentPositions position, params ITagBuilderCustom[] content)
        {
            foreach (ITagBuilderCustom tag in content)
            {
                tag.Position = position;

                _content.Add(tag);
            }
        }

        public void AddAttribute(ITagHelperCustomAttribute attribute)
        {
            _attributes.Add(attribute);
        }

        public void AddAttributeRange(params ITagHelperCustomAttribute[] attributes)
        {
            foreach (ITagHelperCustomAttribute attr in attributes)
            {
                _attributes.Add(attr);
            }
        }

        public void RemoveAttribute(string name)
        {
            if (AttributeExists(name))
            {
                _attributes.Remove(GetAttribute(name));
            }
        }

        public void RemoveAttribute(ITagHelperCustomAttribute attribute) => RemoveAttribute(attribute.Attribute.Name);

        public void RemoveAttributeRange(params ITagHelperCustomAttribute[] attributes)
        {
            foreach (ITagHelperCustomAttribute attr in attributes)
            {
                RemoveAttribute(attr);
            }
        }

        public void ReplaceAttribute(ITagHelperCustomAttribute attribute)
        {
            RemoveAttribute(attribute);
            AddAttribute(attribute);
        }

        public void Clear()
        {
            _content.Clear();
            _attributes.Clear();
        }

        public TagHelperContext Context => _context;
        public TagHelperOutput Output => _output;
        public TagHelperContent InnerContent => _innerContent;
        public HtmlRender Render => _render;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeNotBound]
        public ViewDataDictionary ViewData => this.ViewContext.ViewData;

        public IEnumerable<ITagBuilderCustom> Content => _content.AsEnumerable();
        public CultureInfo CurrentCulture => this.HelperRepo.CurrentCulture;
        public bool AttributeExists(string name) => _attributes.Exists(attr => attr.Attribute.Name == name);
        public bool AttributeExists(ITagHelperCustomAttribute attribute) => _attributes.Exists(attr => attr.Attribute.Name == attribute.Attribute.Name);
        public ITagHelperCustomAttribute GetAttribute(string name) => _attributes.SingleOrDefault(attr => attr.Attribute.Name == name);
        public IEnumerable<ITagHelperCustomAttribute> Attributes => _attributes;
        public IWebHostEnvironment Environment => _env;
        public ITagHelperRepo HelperRepo => _repo;
        public IHtmlHelper HtmlHelper => _htmlHelper;
    }
}

/*
    @Date			              : 25.03.2021
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Components.Repos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LundbeckConsulting.Components.Core.Components.TagHelpers
{
    public interface ITagHelperCustom : ITagHelper
    {
        IWebHostEnvironment Environment { get; }
        ITagHelperRepo HelperRepo { get; }
        IHtmlHelper HtmlHelper { get; }
        void AddContent(TagBuilderCustom tag, params TagBuilderCustom[] tags);
        void AddContent(IEnumerable<TagBuilderCustom> tags);
        IEnumerable<TagBuilderCustom> Content { get; }
        TagHelperContext Context { get; }
        TagHelperOutput Output { get; }
        ViewContext ViewContext { get; set; }
        ViewDataDictionary ViewData { get; }
        CultureInfo CurrentCulture { get; }
        string Id { get; set; }
        string Name { get; set; }
        string IdName { get; set; }
        string Class { get; set; }
        string Style { get; set; }
        string SiteTitle { get; set; }
        string Alt { get; set; }
        string Role { get; set; }
        string For { get; set; }
        int TabIndex { get; set; }
        string OnChange { get; set; }
        string OnKeyUp { get; set; }
        string OnClick { get; set; }
        string OnMouseOver { get; set; }
        string OnMouseOut { get; set; }
        string OnMouseDown { get; set; }
    }

    /// <summary>
    /// Base class for custom Assets tag helpers
    /// </summary>
    public class TagHelperCustom : TagHelper, ITagHelperCustom
    {
        private readonly IWebHostEnvironment _env;
        private readonly ITagHelperRepo _helpRepo;
        private readonly IHtmlHelper _htmlHelp;
        private TagHelperContext _context;
        private TagHelperOutput _output;
        private bool _preProcessed = false;
        private ICollection<TagBuilderCustom> _content = new Collection<TagBuilderCustom>();

        public TagHelperCustom(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper)
        {
            _env = environment;
            _helpRepo = helperRepo;
            _htmlHelp = htmlHelper;
        }

        public async Task PreProcessAsync(TagHelperContext context, TagHelperOutput output, bool suppressOutput = true)
        {
            (this.HtmlHelper as IViewContextAware).Contextualize(this.ViewContext);

            if (suppressOutput)
            {
                output.SuppressOutput();
            }

            _context = context;
            _output = output;
            _preProcessed = true;
        }

        public async Task ProcessAsync()
        {
            if (!_preProcessed)
            {
                throw new Exception("PreProcessAsync must be invoked");
            }

            _helpRepo.ProcessCustomTagHelper(this);

            await base.ProcessAsync(this.Context, this.Output);
        }

        public void AddContent(TagBuilderCustom tag, params TagBuilderCustom[] tags)
        {
            _content.Add(tag);

            foreach(TagBuilderCustom tg in tags)
            {
                _content.Add(tg);
            }
        }

        public void AddContent(IEnumerable<TagBuilderCustom> tags)
        {
            foreach(TagBuilderCustom tag in tags)
            {
                _content.Add(tag);
            }
        }

        public IEnumerable<TagBuilderCustom> Content => _content;
        public IWebHostEnvironment Environment => _env;
        public ITagHelperRepo HelperRepo => _helpRepo;
        public IHtmlHelper HtmlHelper => _htmlHelp;
        public TagHelperContext Context => _context;
        public TagHelperOutput Output => _output;

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        [HtmlAttributeNotBound]
        public ViewDataDictionary ViewData => this.ViewContext.ViewData;

        public CultureInfo CurrentCulture => Thread.CurrentThread.CurrentCulture;

        [HtmlAttributeName("id")]
        public string Id { get; set; }

        [HtmlAttributeName("name")]
        public string Name { get; set; }
        
        [HtmlAttributeName("idname")]
        public string IdName { get; set; }

        [HtmlAttributeName("class")]
        public string Class { get; set; }

        [HtmlAttributeName("style")]
        public string Style { get; set; }

        [HtmlAttributeName("title")]
        public string SiteTitle { get; set; }

        [HtmlAttributeName("onchange")]
        public string OnChange { get; set; }

        [HtmlAttributeName("onkeyup")]
        public string OnKeyUp { get; set; }

        [HtmlAttributeName("onclick")]
        public string OnClick { get; set; }
        
        [HtmlAttributeName("onmouseover")]
        public string OnMouseOver { get; set; }
        
        [HtmlAttributeName("onmouseout")]
        public string OnMouseOut { get; set; }
        
        [HtmlAttributeName("onmousedown")]
        public string OnMouseDown { get; set; }

        [HtmlAttributeName("tabindex")]
        public int TabIndex { get; set; }
        
        [HtmlAttributeName("alt")]
        public string Alt { get; set; }
        
        [HtmlAttributeName("role")]
        public string Role { get; set; }
        
        [HtmlAttributeName("role")]
        public string For { get; set; }
    }
}

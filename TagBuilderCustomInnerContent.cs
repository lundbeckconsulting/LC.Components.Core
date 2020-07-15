/*
    @Date			              : 14.06.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LundbeckConsulting.Components.Core
{
    public interface ITagBuilderCustomInnerContent
    {
        /// <summary>
        /// Content value
        /// </summary>
        IHtmlContent Content { get; set; }
        
        /// <summary>
        /// Determines if the content should be encoded
        /// </summary>
        bool Encode { get; set; }

        /// <summary>
        /// If true content will be appended. If false content will be set
        /// </summary>
        bool AppendContent { get; set; }

        /// <summary>
        /// Creates a html string element
        /// </summary>
        /// <param name="encode">If true content is encoded</param>
        /// <returns></returns>
        HtmlString ToHtmlString(bool encode = false);
    }

    public class TagBuilderCustomInnerContent : ITagBuilderCustomInnerContent
    {
        public TagBuilderCustomInnerContent()
        {

        }

        public TagBuilderCustomInnerContent(TagHelperContent content)
        {
            this.Content = content;
        }

        public IHtmlContent Content { get; set; }
        public bool Encode { get; set; } = false;
        public bool AppendContent { get; set; } = true;
        public HtmlString ToHtmlString(bool encode = false) => this.Content.ToHtmlString(this.Encode);
    }
}

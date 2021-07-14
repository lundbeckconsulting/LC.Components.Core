/*
    @Date			              : 26.03.2021
    @Author                       : Stein Lundbeck
    @Description                  : Processes the base class TagHelperCustom's attributes and content
*/

using LundbeckConsulting.Components.Core.Components.Extensions;
using LundbeckConsulting.Components.Core.Components.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace LundbeckConsulting.Components.Core.Components.Repos
{
    public interface ITagHelperRepoCustomProcess
    {
        /// <summary>
        /// The main processing of a TagBuilderCustom element
        /// </summary>
        /// <param name="tag">The custom tag helper to process</param>
        /// <param name="keepOriginalAttributes">If true the attributes of the base tag is added to the output</param>
        void ProcessCustomTagHelper(ITagHelperCustom tag);
    }

    public sealed partial class TagHelperRepo
    {
        private ITagHelperCustom _customTag;

        public void ProcessCustomTagHelper(ITagHelperCustom tag)
        {
            _customTag = tag;

            foreach (TagBuilderCustom contentTag in _customTag.Content)
            {
                TagBuilderCustom content = ProcessContent(contentTag, true);
                
                switch (contentTag.Position)
                {
                    case ContentPosition.Body:
                        AddTag(_customTag.Output.Content, content);
                        break;

                    case ContentPosition.PreElement:
                        AddTag(_customTag.Output.PreElement, content);
                        break;

                    case ContentPosition.PreContent:
                        AddTag(_customTag.Output.PreContent, content);
                        break;

                    case ContentPosition.PostContent:
                        AddTag(_customTag.Output.PostContent, content);
                        break;

                    case ContentPosition.PostElement:
                        AddTag(_customTag.Output.PostElement, content);
                        break;
                }
            }
        }

        private TagBuilderCustom ProcessContent(TagBuilderCustom content, bool isBase)
        {
            if (isBase)
            {
                if (!_customTag.Class.Null())
                {
                    content.AddCssClass(_customTag.Class);
                }

                if (!_customTag.Id.Null() && _customTag.IdName.Null())
                {
                    content.AddAttribute("id", _customTag.Id);
                }

                if (!_customTag.Name.Null() && _customTag.IdName.Null())
                {
                    content.AddAttribute("name", _customTag.Name);
                }

                if (!_customTag.IdName.Null())
                {
                    content.AddAttribute("id", _customTag.IdName);
                    content.AddAttribute("name", _customTag.IdName);
                }

                if (!_customTag.Title.Null())
                {
                    content.AddAttribute("title", _customTag.Title);
                }

                if (!_customTag.OnChange.Null())
                {
                    content.AddAttribute("onchange", _customTag.OnChange);
                }

                if (!_customTag.OnClick.Null())
                {
                    content.AddAttribute("onclick", _customTag.OnClick);
                }

                if (!_customTag.OnMouseOver.Null())
                {
                    content.AddAttribute("onmouseover", _customTag.OnMouseOver);
                }

                if (!_customTag.OnMouseOut.Null())
                {
                    content.AddAttribute("onmouseout", _customTag.OnMouseOut);
                }

                if (!_customTag.OnMouseDown.Null())
                {
                    content.AddAttribute("onmousedown", _customTag.OnMouseDown);
                }

                if (!_customTag.OnKeyUp.Null())
                {
                    content.AddAttribute("onkeyup", _customTag.OnKeyUp);
                }

                foreach (TagHelperAttribute attr in _customTag.Context.AllAttributes)
                {
                    if (attr.Name.StartsWith("data-") || attr.Name.StartsWith("aria-"))
                    {
                        content.AddAttribute(attr.Name, attr.Value.ToString());
                    }
                }
            }

            foreach (TagBuilderCustom sub in content.Children)
            {
                if (content.ApplyNewLine)
                {
                    content.InnerHtml.AppendBlankLine();
                }

                TagBuilderCustom tmp = ProcessContent(sub, false);

                switch (sub.Render)
                {
                    case HtmlRender.Line:
                        content.InnerHtml.AppendHtmlLine(tmp.ToHtmlString(tmp.Encode).Value);
                        break;

                    case HtmlRender.Cascade:
                        content.InnerHtml.AppendLine(tmp.ToHtmlString(tmp.Encode));
                        break;
                }
            }

            return content;
        }

        private TagHelperContent AddTag(TagHelperContent content, TagBuilderCustom tag)
        {
            if (tag.ApplyNewLine)
            {
                content.AppendLine(tag.ToHtmlString(tag.Encode));
                content.AppendBlankLine();
            }
            else
            {
                content.AppendHtml(tag.ToHtmlString(tag.Encode));
            }

            return content;
        }
    }
}

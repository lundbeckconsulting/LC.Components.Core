/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Processes the base class TagHelperCustom's attributes and content
*/

using LundbeckConsulting.Components.Core.Extensions;
using LundbeckConsulting.Components.Core.TagHelpers;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Web;

namespace LundbeckConsulting.Components.Core.Repos
{
    public interface ITagHelperRepoCustomProcess
    {
        /// <summary>
        /// The main processing of a TagBuilderCustom element
        /// </summary>
        /// <param name="tag">The custom tag helper to process</param>
        /// <param name="keepOriginalAttributes">If true the attributes of the base tag is added to the output</param>
        void ProcessCustomTagHelper(ITagHelperCustom tag, bool keepOriginalAttributes);

        /// <summary>
        /// The main processing of a TagBuilderCustom element
        /// </summary>
        /// <param name="tag">The custom tag helper to process</param>
        /// <param name="keepOriginalAttributes">If true the attributes of the base tag is added to the output</param>
        /// <param name="attributeNameExcludes">Name of attributes that should be excluded from the tag. If default all are included</param>
        void ProcessCustomTagHelper(ITagHelperCustom tag, bool keepOriginalAttributes, IEnumerable<string> attributeNameExcludes);
    }

    public sealed partial class TagHelperRepo
    {
        private ITagHelperCustom _customTag;

        public void ProcessCustomTagHelper(ITagHelperCustom tag, bool keepOriginalAttributes) => ProcessCustomTagHelper(tag, keepOriginalAttributes, default);

        public void ProcessCustomTagHelper(ITagHelperCustom tag, bool keepOriginalAttributes, IEnumerable<string> attributeNameExcludes)
        {
            _customTag = tag;

            ProcessBaseAttributes();

            if (keepOriginalAttributes)
            {
                ProcessKeepAttributes(attributeNameExcludes);
            }

            foreach (ITagBuilderCustom contentTag in _customTag.Content)
            {
                ITagBuilderCustom content = ProcessContent(contentTag, true);

                switch (contentTag.Position)
                {
                    case ContentPositions.Body:
                        AddTag(_customTag.Output.Content, contentTag);
                        break;

                    case ContentPositions.PreElement:
                        AddTag(_customTag.Output.PreElement, contentTag);
                        break;

                    case ContentPositions.PreContent:
                        AddTag(_customTag.Output.PreContent, contentTag);
                        break;

                    case ContentPositions.PostContent:
                        AddTag(_customTag.Output.PostContent, contentTag);
                        break;

                    case ContentPositions.PostElement:
                        AddTag(_customTag.Output.PostElement, contentTag);
                        break;
                }
            }
        }

        private ITagBuilderCustom ProcessContent(ITagBuilderCustom content, bool isBase)
        {
            bool override_ = !ContentConsumesAttributes && isBase;
            ITagBuilderCustom result = ProcessAttributes(content, override_);

            ProcessInnerContent(result);

            foreach (ITagBuilderCustom sub in result.Children)
            {
                ITagBuilderCustom tmp = ProcessContent(sub, false);

                switch (sub.Render)
                {
                    case HtmlRender.Line:
                        result.InnerHtml.AppendHtml(tmp.ToHtmlString(tmp.Encode));
                        break;

                    case HtmlRender.Cascade:
                        result.InnerHtml.AppendLine(tmp.ToHtmlString(tmp.Encode));
                        break;
                }
            }

            return result;
        }

        private ITagBuilderCustom ProcessAttributes(ITagBuilderCustom tag, bool consumeOverride = false)
        {
            if (tag.ConsumeAttributes || consumeOverride)
            {
                if (!tag.AttributesToConsume.Zero())
                {
                    foreach (string attName in tag.AttributesToConsume)
                    {
                        ITagBuilderCustomAttribute attr = tag.AttributeExists(attName) ? tag.GetAttribute(attName) : new TagBuilderCustomAttribute(attName, string.Empty);
                        var att = GetProcessedAttribute(attr);

                        tag.Attributes.Add(att);
                    }
                }
                else
                {
                    foreach (IAttributeCustom attr in tag.CustomAttributes)
                    {
                        tag.Attributes.Add(attr.Attribute);
                    }
                }
            }

            return tag;
        }

        private KeyValuePair<string, string> GetProcessedAttribute(ITagBuilderCustomAttribute attribute)
        {
            string val = attribute.Value;

            if (attribute.Merge && _customTag.AttributeExists(attribute.Name)) {
                ITagHelperCustomAttribute attr = _customTag.GetAttribute(attribute.Name);

                if (!attr.Value.IsEmpty())
                {
                    val = $"{val} {attr.Value}";
                }
            }

            if (attribute.Encode)
            {
                val = HttpUtility.HtmlEncode(val);
            }

            return new KeyValuePair<string, string>(attribute.Name, val);
        }

        private bool ContentConsumesAttributes
        {
            get
            {
                bool result = false;

                foreach (ITagBuilderCustom tag in _customTag.Content)
                {
                    ProcessContent(tag);
                }

                return result;

                void ProcessContent(ITagBuilderCustom tag)
                {
                    if (tag.ConsumeAttributes)
                    {
                        result = true;
                        return;
                    }
                    else
                    {
                        foreach (ITagBuilderCustom tg in tag.Children)
                        {
                            ProcessContent(tg);
                        }
                    }
                }
            }
        }

        private ITagBuilderCustom ProcessInnerContent(ITagBuilderCustom tag)
        {
            foreach (ITagBuilderCustomInnerContent item in tag.InnerContent)
            {
                if (item.AppendContent)
                {
                    tag.InnerHtml.AppendLine(item.Content.ToHtmlString(item.Encode));
                }
                else
                {
                    tag.InnerHtml.SetHtmlContent(item.Content.ToHtmlString(item.Encode));
                }
            }

            return tag;
        }

        private void ProcessBaseAttributes()
        {
            if (_customTag.IdName.Null() && !_customTag.Id.Null())
            {
                ProcessAttribute("id", _customTag.Id);
            }

            if (_customTag.IdName.Null() && !_customTag.Name.Null())
            {
                ProcessAttribute("name", _customTag.Name);
            }

            if (!_customTag.IdName.Null())
            {
                ProcessAttribute("id", _customTag.IdName);
                ProcessAttribute("name", _customTag.IdName);
            }

            if (!_customTag.Class.Null())
            {
                ProcessAttribute("class", _customTag.Class, true);
            }

            if (!_customTag.Style.Null())
            {
                ProcessAttribute("style", _customTag.Style, true);
            }

            if (!_customTag.Title.Null())
            {
                ProcessAttribute("title", _customTag.Title);
            }

            if (!_customTag.Alt.Null())
            {
                ProcessAttribute("alt", _customTag.Alt);
            }

            if (!_customTag.Role.Null())
            {
                ProcessAttribute("role", _customTag.Role.ToLower());
            }

            if (!_customTag.For.Null())
            {
                ProcessAttribute("for", _customTag.For);
            }

            if (_customTag.TabIndex > -1)
            {
                ProcessAttribute("tabindex", _customTag.TabIndex.ToString());
            }

            if (!_customTag.Lang.Null())
            {
                ProcessAttribute("lang", _customTag.Lang);
            }

            ProcessAttribute("draggable", _customTag.Draggable.ToLower());

            foreach (TagHelperAttribute attr in _customTag.Context.AllAttributes)
            {
                if (attr.Name.StartsWith("data-") || attr.Name.StartsWith("aria-"))
                {
                    ProcessAttribute(attr.Name, attr.Value.ToString());
                }
            }

            void ProcessAttribute(string name, string value, bool merge = false)
            {
                if (_customTag.AttributeExists(name))
                {
                    ITagHelperCustomAttribute attr = _customTag.GetAttribute(name);
                    string val = value;

                    if (merge)
                    {
                        attr = new TagHelperCustomAttribute(name, value, false);
                    }

                    _customTag.ReplaceAttribute(attr);
                }
            }
        }

        private void ProcessKeepAttributes(IEnumerable<string> nameOfAttributesToExlude)
        {
            foreach (TagHelperAttribute attr in _customTag.Context.AllAttributes)
            {
                if (!_customTag.IsBaseAttribute(attr.Name))
                {
                    if (!nameOfAttributesToExlude.Null())
                    {
                        foreach (string str in nameOfAttributesToExlude)
                        {
                            if (str.Contains("=>"))
                            {
                                if (!attr.Name.StartsWith(str.Replace("=>", "")))
                                {
                                    _customTag.AddAttribute(new TagHelperCustomAttribute(attr.Name, attr.Value.ToString(), true));
                                }
                            }
                            else if (!nameOfAttributesToExlude.Exists(excl => excl == attr.Name)) //attribute name not in list
                            {
                                _customTag.AddAttribute(new TagHelperCustomAttribute(attr.Name, attr.Value.ToString(), true));
                            }
                        }
                    }
                    else // no attributes to exclude - add all
                    {
                        foreach (TagHelperAttribute att in _customTag.Context.AllAttributes)
                        {
                            _customTag.AddAttribute(new TagHelperCustomAttribute(att.Name, att.Value.ToString(), true));
                        }

                        return;
                    }
                }
            }
        }

        private TagHelperContent AddTag(TagHelperContent content, ITagBuilderCustom tag)
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

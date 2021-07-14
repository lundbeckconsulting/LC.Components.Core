/*
    @Date			: 25.03.2021
    @Author         : Stein Lundbeck
*/

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LundbeckConsulting.Components.Core.Components
{
    public class TagBuilderCustom : TagBuilder
    {
        private readonly ICollection<TagBuilderCustom> _children = new Collection<TagBuilderCustom>();

        public TagBuilderCustom(string tagName, ContentPosition position = ContentPosition.PostElement) : base(tagName)
        {
            this.Position = position;
        }

        public void AddChild(TagBuilderCustom tag, params TagBuilderCustom[] tags)
        {
            _children.Add(tag);

            foreach(TagBuilderCustom tg in tags)
            {
                _children.Add(tg);
            }
        }

        public void AddChildRange(IEnumerable<TagBuilderCustom> tags)
        {
            foreach(TagBuilderCustom tag in tags)
            {
                AddChild(tag);
            }
        }

        public void AddAttribute(string name, string value)
        {
            if (this.Attributes.ContainsKey(name))
            {
                this.Attributes.Remove(name);
            }

            this.Attributes.Add(name, value);
        }

        public void AddAttribute(ICustomAttribute attr, params ICustomAttribute[] attrs)
        {
            AddAttribute(attr.Name, attr.Value);

            foreach(ICustomAttribute atr in attrs)
            {
                AddAttribute(atr.Name, atr.Value);
            }
        }

        public void AddAttributeRange(IEnumerable<ICustomAttribute> attrs)
        {
            foreach(ICustomAttribute attr in attrs)
            {
                AddAttribute(attr.Name, attr.Value);
            }
        }

        public void ApplyInnerContent(string content, bool line = true)
        {
            if (line)
            {
                this.InnerHtml.AppendHtmlLine(content);
            }
            else
            {
                this.InnerHtml.Append(content);
            }
        }

        public void ApplyInnerContent(IHtmlContent content, bool line = true)
        {
            if (line)
            {
                this.InnerHtml.AppendLine(content);
            }
            else
            {
                this.InnerHtml.AppendHtml(content);
            }
        }

        public bool Encode { get; set; } = true;
        public bool ApplyNewLine { get; set; } = true;
        public IEnumerable<TagBuilderCustom> Children => _children;
        public ContentPosition Position { get; set; } = ContentPosition.PostElement;
        public HtmlRender Render { get; set; } = HtmlRender.Cascade;
    }
}

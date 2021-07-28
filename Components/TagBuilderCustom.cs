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
    public interface ITagBuilderCustom
    {
        void AddChild(ITagBuilderCustom tag, params ITagBuilderCustom[] tags);
        void AddChildRange(IEnumerable<ITagBuilderCustom> tags);
        void AddAttribute(string name, string value);
        void AddAttribute(ICustomAttribute attr, params ICustomAttribute[] attrs);
        void AddAttributeRange(IEnumerable<ICustomAttribute> attrs);
        void ApplyBlankLine();
        void ApplyContent(string content, bool line = true);
        void ApplyContent(IHtmlContent content, bool line = true);

        /// <summary>
        /// Defines if the content should be encoded when rendered
        /// </summary>
        bool Encode { get; set; }
        bool ApplyNewLine { get; set; }
        IEnumerable<ITagBuilderCustom> Children { get; }

        /// <summary>
        /// The position where the tag should be rendered
        /// </summary>
        ContentPosition Position { get; set; }
        HtmlRender Render { get; set; }

        /// <summary>
        /// Defines if base parameters should be processed
        /// </summary>
        bool AttachParameters { get; set; }
    }

    public class TagBuilderCustom : TagBuilder, ITagBuilderCustom
    {
        private readonly ICollection<ITagBuilderCustom> _children = new Collection<ITagBuilderCustom>();

        public TagBuilderCustom(string tagName, ContentPosition position = ContentPosition.PostElement) : base(tagName)
        {
            this.Position = position;
        }

        public void AddChild(ITagBuilderCustom tag, params ITagBuilderCustom[] tags)
        {
            _children.Add(tag);

            foreach(ITagBuilderCustom tg in tags)
            {
                _children.Add(tg);
            }
        }

        public void AddChildRange(IEnumerable<ITagBuilderCustom> tags)
        {
            foreach(ITagBuilderCustom tag in tags)
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

        public void ApplyBlankLine() => this.InnerHtml.AppendHtmlLine("");

        public void ApplyContent(string content, bool line = true)
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

        public void ApplyContent(IHtmlContent content, bool line = true)
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
        public IEnumerable<ITagBuilderCustom> Children => _children;
        public ContentPosition Position { get; set; } = ContentPosition.PostElement;
        public HtmlRender Render { get; set; } = HtmlRender.Cascade;
        public bool AttachParameters { get; set; } = true;
    }
}

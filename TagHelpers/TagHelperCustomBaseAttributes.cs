using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;

namespace LundbeckConsulting.Components.Core.TagHelpers
{
    public interface ITagHelperCustomBaseAttributes
    {
        /// <summary>
        /// Value of attribute Id
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Value of attribute Name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Set both attribute Id and Name to equal value
        /// </summary>
        string IdName { get; set; }

        /// <summary>
        /// Name of styles who merge into existing elemenst class attribute
        /// </summary>
        string Class { get; set; }

        /// <summary>
        /// Value of Style attribute who merges if existing
        /// </summary>
        string Style { get; set; }

        /// <summary>
        /// Value of attribute Title
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Value of attribute Alt
        /// </summary>
        string Alt { get; set; }

        /// <summary>
        /// Value of attribute For
        /// </summary>
        string For { get; set; }

        /// <summary>
        /// Value of attribute Role
        /// </summary>
        AriaRoleValues Role { get; set; }

        /// <summary>
        /// Tab index of element
        /// </summary>
        int TabIndex { get; set; }

        /// <summary>
        /// Element language
        /// </summary>
        string Lang { get; set; }

        /// <summary>
        /// Indicates if the element is draggable
        /// </summary>
        DraggableValues Draggable { get; set; }

        /// <summary>
        /// Processes all the base attributes
        /// </summary>
        void ProcessBaseAttributes();

        /// <summary>
        /// List of the names of all base attributes
        /// </summary>
        IEnumerable<string> AttributesList { get; }
        
        /// <summary>
        /// Indicates if an attribute name matches a base attribute
        /// </summary>
        /// <param name="name">Name to process</param>
        /// <returns>True if name equals a base attribute</returns>
        bool IsBaseAttribute(string name);
    }

    public abstract partial class TagHelperCustom : ITagHelperCustomBaseAttributes
    {
        public IEnumerable<string> AttributesList => new string[] {
            "id",
            "name",
            "idname",
            "class",
            "style",
            "title",
            "alt",
            "role",
            "for",
            "tabindex",
            "lang",
            "draggable"
        };

        public void ProcessBaseAttributes()
        {
            if (this.IdName.Null() && !this.Id.Null())
            {
                ProcessAttribute("id", this.Id);
            }

            if (this.IdName.Null() && !this.Name.Null())
            {
                ProcessAttribute("name", this.Name);
            }

            if (!this.IdName.Null())
            {
                ProcessAttribute("id", this.IdName);
                ProcessAttribute("name", this.IdName);
            }

            if (!this.Class.Null())
            {
                ProcessAttribute("class", this.Class, true);
            }

            if (!this.Style.Null())
            {
                ProcessAttribute("style", this.Style, true);
            }

            if (!this.Title.Null())
            {
                ProcessAttribute("title", this.Title);
            }

            if (!this.Alt.Null())
            {
                ProcessAttribute("alt", this.Alt);
            }

            if (!this.Role.Null())
            {
                ProcessAttribute("role", this.Role.ToLower());
            }

            if (!this.For.Null())
            {
                ProcessAttribute("for", this.For);
            }

            if (this.TabIndex > -1)
            {
                ProcessAttribute("tabindex", this.TabIndex.ToString());
            }

            if (!this.Lang.Null())
            {
                ProcessAttribute("lang", this.Lang);
            }

            ProcessAttribute("draggable", this.Draggable.ToLower());

            foreach (TagHelperAttribute attr in this.Context.AllAttributes)
            {
                if (attr.Name.StartsWith("data-") || attr.Name.StartsWith("aria-"))
                {
                    ProcessAttribute(attr.Name, attr.Value.ToString());
                }
            }

            void ProcessAttribute(string name, string value, bool merge = false)
            {
                if (AttributeExists(name))
                {
                    ITagHelperCustomAttribute attr = GetAttribute(name);
                    string val = value;

                    if (merge)
                    {
                        attr = new TagHelperCustomAttribute(name, value, false);
                    }

                    ReplaceAttribute(attr);
                }
            }
        }

        public bool IsBaseAttribute(string name) => this.AttributesList.Exists(attr => attr == name);

        #region Attributes
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
        public string Title { get; set; }

        [HtmlAttributeName("alt")]
        public string Alt { get; set; }

        [HtmlAttributeName("role")]
        public AriaRoleValues Role { get; set; }

        [HtmlAttributeName("for")]
        public string For { get; set; }

        [HtmlAttributeName("tabindex")]
        public int TabIndex { get; set; } = -1;

        [HtmlAttributeName("lang")]
        public string Lang { get; set; }

        [HtmlAttributeName("draggable")]
        public DraggableValues Draggable { get; set; } = DraggableValues.Auto;
        #endregion
    }
}

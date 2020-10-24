﻿/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;
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
        AriaRole Role { get; set; }

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
        DraggableValue Draggable { get; set; }

        /// <summary>
        /// Shortcut key to set focus on element
        /// </summary>
        string AccessKey { get; set; }

        /// <summary>
        /// JavaScript function
        /// </summary>
        string OnClick { get; set; }

        /// <summary>
        /// JavaScript on mouse over
        /// </summary>
        string OnMouseOver { get; set; }

        /// <summary>
        /// JavaScript on mouse out
        /// </summary>
        string OnMouseOut { get; set; }

        /// <summary>
        /// JavaScript on mouse down
        /// </summary>
        string OnMouseDown { get; set; }

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
            "draggable",
            "accesskey",
            "onclick",
            "onmouseover",
            "onmouseout",
            "onmousedown"
        };

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
        public AriaRole Role { get; set; }

        [HtmlAttributeName("for")]
        public string For { get; set; }

        [HtmlAttributeName("tabindex")]
        public int TabIndex { get; set; } = -1;

        [HtmlAttributeName("lang")]
        public string Lang { get; set; }

        [HtmlAttributeName("draggable")]
        public DraggableValue Draggable { get; set; } = DraggableValue.Auto;

        [HtmlAttributeName("accesskey")]
        public string AccessKey { get; set; }

        [HtmlAttributeName("onclick")]
        public string OnClick { get; set; }

        [HtmlAttributeName("onmouseover")]
        public string OnMouseOver { get; set; }

        [HtmlAttributeName("onmouseout")]
        public string OnMouseOut { get; set; }

        [HtmlAttributeName("onmousedown")]
        public string OnMouseDown { get; set; }
        #endregion
    }
}

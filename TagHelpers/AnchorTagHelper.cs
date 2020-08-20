/*
    @Date			              : 31.07.2020
    @Author                       : Stein Lundbeck
*/

using LundbeckConsulting.Components.Core.Repos;
using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace LundbeckConsulting.Components.Core.TagHelpers
{
    [HtmlTargetElement("a", Attributes = "rel", TagStructure = TagStructure.NormalOrSelfClosing)]
    class AnchorTagHelper : TagHelperCustom
    {
        private string _anchorRel = string.Empty;

        public AnchorTagHelper(IWebHostEnvironment environment, ITagHelperRepo helperRepo, IHtmlHelper htmlHelper) : base(environment, helperRepo, htmlHelper)
        {

        }

        [HtmlAttributeName("rel")]
        public AnchorRelType Rel
        {
            get => _anchorRel.GetEnumItem<AnchorRelType>();

            set
            {
                _anchorRel = value.ToLower();
            }
        }
    }
}

using Microsoft.AspNetCore.Html;

namespace LundbeckConsulting.Components.Core.Extensions
{
    public static class IHtmlContentBuilderExstensions
    {
        /// <summary>
        /// Adds a blank line to the content
        /// </summary>
        /// <param name="content">Content to add line too</param>
        public static IHtmlContentBuilder AppendBlankLine(this IHtmlContentBuilder content) => content.AppendHtml("\n");
    }
}

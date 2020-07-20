/*
    @Date			              : 15.07.2020
    @Author                       : Stein Lundbeck
    @Description                  : Helper repo for custom tag helpers
*/

using LundbeckConsulting.Components.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace LundbeckConsulting.Components.Core.Repos
{
    public interface ITagHelperRepo : IRepoCoreBase, ITagHelperRepoCustomProcess
    {
        /// <summary>
        /// Gets a string representation of the application media type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>String value of application media type</returns>
        string GetMediaApplicationTypesValue(MediaApplicationTypes type);
        
        /// <summary>
        /// Get anchor target value of anchor target
        /// </summary>
        /// <returns>String value of anchor target</returns>
        string GetAnchorTarget(TagAnchorTargets target);

        /// <summary>
        /// Gets a string representation of the media text type
        /// </summary>
        /// <returns>String value of media text type</returns>
        string GetTextMediaTypesValue(TextMediaTypes type);

        /// <summary>
        /// Gets a string representation of the image media type
        /// </summary>
        /// <returns>String value of image media type</returns>
        string GetImageMediaTypesValue(ImageMediaTypes type);

        /// <summary>
        /// Gets a string representation of the font media type
        /// </summary>
        /// <returns>String value of font media type</returns>
        string GetFontMediaTypesValue(FontMediaTypes type);

        /// <summary>
        /// Gets file type equal to path
        /// </summary>
        /// <param name="path">Path to process</param>
        /// <returns>File type based on path</returns>
        FileTypes GetFileType(string path);

        /// <summary>
        /// Current web host environment element
        /// </summary>
        IWebHostEnvironment Environment { get; }
    }

    /// <summary>
    /// Contains methods that helps developing of custom tag helpers
    /// </summary>
    public sealed partial class TagHelperRepo : RepoCoreBase, ITagHelperRepo
    {
        private readonly IWebHostEnvironment _env;

        public TagHelperRepo(IHttpContextAccessor httpContext, IConfiguration config, IWebHostEnvironment environment) : base(httpContext, config)
        {
            _env = environment;
        }

        public string GetMediaApplicationTypesValue(MediaApplicationTypes type)
        {
            StringBuilder result = new StringBuilder("application/");

            switch (type)
            {
                case MediaApplicationTypes.JavaScript:
                    result.Append("JAVASCRIPT");
                    break;

                case MediaApplicationTypes.HTTP:
                    result.Append("HTTP");
                    break;

                case MediaApplicationTypes.XML:
                    result.Append("XML");
                    break;

                case MediaApplicationTypes.JSON:
                    result.Append("JSON");
                    break;

                case MediaApplicationTypes.EcmaScript:
                    result.Append("ECMASCRIPT");
                    break;

                case MediaApplicationTypes.NodeJS:
                    result.Append("NODE");
                    break;
            }

            return result.ToString();
        }

        public string GetAnchorTarget(TagAnchorTargets target)
        {
            string result = string.Empty;

            switch (target)
            {
                case TagAnchorTargets._blank:
                    result = "_blank";
                    break;

                case TagAnchorTargets._parent:
                    result = "_parent";
                    break;

                case TagAnchorTargets._self:
                    result = "_self";
                    break;

                case TagAnchorTargets._top:
                    result = "_top";
                    break;
            }

            return result;
        }

        public string GetTextMediaTypesValue(TextMediaTypes type)
        {
            StringBuilder result = new StringBuilder("text/");

            switch(type)
            {
                case TextMediaTypes.CSS:
                    result.Append("CSS");
                    break;

                case TextMediaTypes.CSV:
                    result.Append("CSV");
                    break;

                case TextMediaTypes.HTML:
                    result.Append("HTML");
                    break;

                case TextMediaTypes.XML:
                    result.Append("XML");
                    break;

                case TextMediaTypes.DNS:
                    result.Append("DNS");
                    break;

                case TextMediaTypes.JAVASCRIPT:
                    result.Append("JAVASCRIPT");
                    break;
            }

            return result.ToString();
        }

        public string GetImageMediaTypesValue(ImageMediaTypes type)
        {
            StringBuilder result = new StringBuilder("image/");

            switch(type)
            {
                case ImageMediaTypes.BMP:
                    result.Append("BMP");
                    break;

                case ImageMediaTypes.JPEG:
                    result.Append("JPEG");
                    break;

                case ImageMediaTypes.PNG:
                    result.Append("PNG");
                    break;

                case ImageMediaTypes.SVG:
                    result.Append("SVG");
                    break;

                case ImageMediaTypes.TIFF:
                    result.Append("TIFF");
                    break;

                case ImageMediaTypes.GIF:
                    result.Append("GIF");
                    break;
            }

            return result.ToString();
        }

        public string GetFontMediaTypesValue(FontMediaTypes type)
        {
            StringBuilder result = new StringBuilder("font/");

            switch (type)
            {
                case FontMediaTypes.Collection:
                    result.Append("COLLECTION");
                    break;

                case FontMediaTypes.OTF:
                    result.Append("OTF");
                    break;

                case FontMediaTypes.SFNT:
                    result.Append("SFNT");
                    break;

                case FontMediaTypes.TTF:
                    result.Append("TTF");
                    break;

                case FontMediaTypes.WOFF:
                    result.Append("WOFF");
                    break;

                case FontMediaTypes.WOFF2:
                    result.Append("WOFF2");
                    break;
            }

            return result.ToString();
        }

        public FileTypes GetFileType(string path)
        {
            FileTypes result = FileTypes.None;

            switch (new FileInfo(path).ExtensionCustom())
            {
                case "JPG":
                case "JPEG":
                case "GIF":
                case "PNG":
                case "SVG":
                    result = FileTypes.Image;
                    break;

                case "PDF":
                    result = FileTypes.PDF;
                    break;

                case "DOCX":
                    result = FileTypes.Docx;
                    break;

                case "TXT":
                    result = FileTypes.Text;
                    break;

                case "ODT":
                    result = FileTypes.OpenDocument;
                    break;

                case "RTF":
                    result = FileTypes.RichText;
                    break;

                case "MK":
                    result = FileTypes.Markdown;
                    break;

                case "CSS":
                    result = FileTypes.Style;
                    break;

                case "JS":
                    result = FileTypes.JavaScript;
                    break;

                case "CSHTML":
                    result = FileTypes.View;
                    break;

                case "SCSS":
                    result = FileTypes.SASS;
                    break;
            }

            return result;
        }

        public IWebHostEnvironment Environment => _env;
    }
}

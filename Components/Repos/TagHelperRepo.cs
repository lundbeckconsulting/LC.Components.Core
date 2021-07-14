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

namespace LundbeckConsulting.Components.Core.Components.Repos
{
    public interface ITagHelperRepo : IRepoCoreBase, ITagHelperRepoCustomProcess
    {
        /// <summary>
        /// Gets a string representation of the application media type
        /// </summary>
        /// <param name="type"></param>
        /// <returns>String value of application media type</returns>
        string GetMediaApplicationTypesValue(MediaApplicationType type);
        
        /// <summary>
        /// Get anchor target value of anchor target
        /// </summary>
        /// <returns>String value of anchor target</returns>
        string GetAnchorTarget(TagAnchorTarget target);

        /// <summary>
        /// Gets a string representation of the media text type
        /// </summary>
        /// <returns>String value of media text type</returns>
        string GetTextMediaTypesValue(TextMediaType type);

        /// <summary>
        /// Gets a string representation of the image media type
        /// </summary>
        /// <returns>String value of image media type</returns>
        string GetImageMediaTypesValue(ImageMediaType type);

        /// <summary>
        /// Gets a string representation of the font media type
        /// </summary>
        /// <returns>String value of font media type</returns>
        string GetFontMediaTypesValue(FontMediaType type);

        /// <summary>
        /// Gets file type equal to path
        /// </summary>
        /// <param name="path">Path to process</param>
        /// <returns>File type based on path</returns>
        FileType GetFileType(string path);

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

        public string GetMediaApplicationTypesValue(MediaApplicationType type)
        {
            StringBuilder result = new StringBuilder("application/");

            switch (type)
            {
                case MediaApplicationType.JavaScript:
                    result.Append("JAVASCRIPT");
                    break;

                case MediaApplicationType.HTTP:
                    result.Append("HTTP");
                    break;

                case MediaApplicationType.XML:
                    result.Append("XML");
                    break;

                case MediaApplicationType.JSON:
                    result.Append("JSON");
                    break;

                case MediaApplicationType.EcmaScript:
                    result.Append("ECMASCRIPT");
                    break;

                case MediaApplicationType.NodeJS:
                    result.Append("NODE");
                    break;
            }

            return result.ToString();
        }

        public string GetAnchorTarget(TagAnchorTarget target) => $"_{target.ToLower()}";

        public string GetTextMediaTypesValue(TextMediaType type)
        {
            StringBuilder result = new StringBuilder("text/");

            switch(type)
            {
                case TextMediaType.CSS:
                    result.Append("CSS");
                    break;

                case TextMediaType.CSV:
                    result.Append("CSV");
                    break;

                case TextMediaType.HTML:
                    result.Append("HTML");
                    break;

                case TextMediaType.XML:
                    result.Append("XML");
                    break;

                case TextMediaType.DNS:
                    result.Append("DNS");
                    break;

                case TextMediaType.JAVASCRIPT:
                    result.Append("JAVASCRIPT");
                    break;
            }

            return result.ToString();
        }

        public string GetImageMediaTypesValue(ImageMediaType type)
        {
            StringBuilder result = new StringBuilder("image/");

            switch(type)
            {
                case ImageMediaType.BMP:
                    result.Append("BMP");
                    break;

                case ImageMediaType.JPEG:
                    result.Append("JPEG");
                    break;

                case ImageMediaType.PNG:
                    result.Append("PNG");
                    break;

                case ImageMediaType.SVG:
                    result.Append("SVG");
                    break;

                case ImageMediaType.TIFF:
                    result.Append("TIFF");
                    break;

                case ImageMediaType.GIF:
                    result.Append("GIF");
                    break;
            }

            return result.ToString();
        }

        public string GetFontMediaTypesValue(FontMediaType type)
        {
            StringBuilder result = new StringBuilder("font/");

            switch (type)
            {
                case FontMediaType.Collection:
                    result.Append("COLLECTION");
                    break;

                case FontMediaType.OTF:
                    result.Append("OTF");
                    break;

                case FontMediaType.SFNT:
                    result.Append("SFNT");
                    break;

                case FontMediaType.TTF:
                    result.Append("TTF");
                    break;

                case FontMediaType.WOFF:
                    result.Append("WOFF");
                    break;

                case FontMediaType.WOFF2:
                    result.Append("WOFF2");
                    break;
            }

            return result.ToString();
        }

        public FileType GetFileType(string path)
        {
            FileType result = FileType.None;

            switch (new FileInfo(path).ExtensionCustom())
            {
                case "JPG":
                case "JPEG":
                case "GIF":
                case "PNG":
                case "SVG":
                    result = FileType.Image;
                    break;

                case "PDF":
                    result = FileType.PDF;
                    break;

                case "DOCX":
                    result = FileType.Docx;
                    break;

                case "TXT":
                    result = FileType.Text;
                    break;

                case "ODT":
                    result = FileType.OpenDocument;
                    break;

                case "RTF":
                    result = FileType.RichText;
                    break;

                case "MK":
                    result = FileType.Markdown;
                    break;

                case "CSS":
                    result = FileType.Style;
                    break;

                case "JS":
                    result = FileType.JavaScript;
                    break;

                case "CSHTML":
                    result = FileType.View;
                    break;

                case "SCSS":
                    result = FileType.SASS;
                    break;
            }

            return result;
        }

        public IWebHostEnvironment Environment => _env;
    }
}

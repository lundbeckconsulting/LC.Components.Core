namespace LundbeckConsulting.Components.Core
{
    /// <summary>
    /// Different easy access functions
    /// </summary>
    public static class LCCoreStatics
    {
        /// <summary>
        /// Gets the name of the tag equal to format
        /// </summary>
        /// <param name="format">Format to process</param>
        /// <returns>Tab with type based on format</returns>
        public static string GetHtmlTextContentTagName(HtmlTextContentFormat format)
        {
            string result = string.Empty;

            switch (format)
            {
                case HtmlTextContentFormat.Article:
                    result = "article";
                    break;

                case HtmlTextContentFormat.Div:
                    result = "div";
                    break;

                case HtmlTextContentFormat.Section:
                    result = "section";
                    break;

                case HtmlTextContentFormat.Span:
                    result = "span";
                    break;

                case HtmlTextContentFormat.Inserted:
                    result = "ins";
                    break;

                case HtmlTextContentFormat.Paragraph:
                    result = "p";
                    break;

                case HtmlTextContentFormat.PreFormatted:
                    result = "pre";
                    break;
            }

            return result;
        }
    }

    /// <summary>
    /// The types defining how the tag and content html should be rendered
    /// </summary>
    public enum HtmlRender
    {
        Line,
        Cascade
    }

    /// <summary>
    /// Supported values for the rel attribute
    /// </summary>
    public enum RelType
    {
        Icon,
        Stylesheet,
        Preload,
        Manifest,
        Alternate,
        Author,
        Bookmark,
        External,
        Help,
        License,
        NoFollow,
        ShortLink,
        Tag,
        Next,
        Prerender,
        Search
    }

    /// <summary>
    /// Supported values for the Rel attribute of the anchor tag
    /// </summary>
    public enum AnchorRelType
    {
        None,
        Alternate,
        Author,
        Bookmark,
        Enclosure,
        External,
        Help,
        License,
        Next,
        NoFollow,
        NoReferrer,
        NoOpener,
        Prev,
        Search,
        Tag
    }

    /// <summary>
    /// Different jQuery versions
    /// </summary>
    public enum jQueryVersion
    {
        Standard,
        UI,
        ValidationUnobtrusive
    }

    /// <summary>
    /// Different jQuery info parts
    /// </summary>
    public enum jQueryInfoPart
    {
        Href,
        Integrity
    }

    /// <summary>
    /// Valid values to the Role attribute
    /// </summary>
    public enum AriaRole
    {
        None,
        Comment,
        Complementary,
        List,
        Listitem,
        Main,
        Mark,
        Navigation,
        Region,
        Suggestion,
        Alert,
        Application,
        Article,
        Banner,
        Button,
        Cell,
        Checkbox,
        Contentinfo,
        Dialog,
        Document,
        Feed,
        Figure,
        Form,
        Grid,
        Gridcell,
        Heading,
        Img,
        Listbox,
        Row,
        Rowgroup,
        Search,
        Switch,
        Tab,
        Table,
        Tabpanel,
        Textbox,
        Timer
    }

    /// <summary>
    /// Valid value for the draggable attribute
    /// </summary>
    public enum DraggableValue
    {
        Auto,
        True,
        False
    }

    /// <summary>
    /// Form methods of the form tag
    /// </summary>
    public enum TagFormMethod
    {
        Delete,
        Get,
        Post,
        Put
    }

    /// <summary>
    /// Supported types of the button tag
    /// </summary>
    public enum TagButtonType
    {
        Button,
        Reset,
        Submit
    }

    /// <summary>
    /// Supported targets for the anchor tag
    /// </summary>
    public enum TagAnchorTarget
    {
        None,
        Blank,
        Self,
        Parent,
        Top
    }

    /// <summary>
    /// Valid importance items for a tag importance attribute
    /// </summary>
    public enum TagImportanceAttributeValue
    {
        None,
        Auto,
        High,
        Low
    }

    /// <summary>
    /// Supported positions for where to add new tag content
    /// </summary>
    public enum TagContentPosition
    {
        Body,
        PreElement,
        PreContent,
        PostElement,
        PostContent
    }

    /// <summary>
    /// Supported values for the media application value
    /// </summary>
    public enum MediaApplicationType
    {
        None,
        JavaScript,
        HTTP,
        XML,
        JSON,
        Unobtrusive,
        EcmaScript,
        NodeJS
    }

    /// <summary>
    /// Supported values for the media type
    /// </summary>
    public enum TextMediaType
    {
        None,
        HTML,
        XML,
        CSS,
        CSV,
        DNS,
        JAVASCRIPT
    }

    /// <summary>
    /// Supported image media types
    /// </summary>
    public enum ImageMediaType
    {
        None,
        PNG,
        TIFF,
        BMP,
        JPEG,
        SVG,
        GIF
    }

    /// <summary>
    /// Supported font media types
    /// </summary>
    public enum FontMediaType
    {
        Collection,
        OTF,
        SFNT,
        TTF,
        WOFF,
        WOFF2
    }

    /// <summary>
    /// Positions where content is added
    /// </summary>
    public enum ContentPosition
    {
        Body,
        PreElement,
        PreContent,
        PostContent,
        PostElement
    }


    /// <summary>
    /// Type of html tags that can contain ordinary textual content
    /// </summary>
    public enum HtmlTextContentFormat
    {
        Article,
        Div,
        Section,
        Span,
        Inserted,
        Paragraph,
        PreFormatted
    }

    /// <summary>
    /// Indicates the relative importance of the element
    /// </summary>
    public enum LinkTagImportanceType
    { 
        None,
        Auto,
        High,
        Low
    }

}

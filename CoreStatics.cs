namespace LundbeckConsulting.Components.Core
{
    public static class CoreStatics
    {
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
    public enum RelTypes
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
    /// Different jQuery versions
    /// </summary>
    public enum jQueryVersions
    {
        Standard,
        UI,
        ValidationUnobtrusive
    }

    /// <summary>
    /// Different jQuery info parts
    /// </summary>
    public enum jQueryInfoParts
    {
        Href,
        Integrity
    }

    /// <summary>
    /// Valid values to the Role attribute
    /// </summary>
    public enum AriaRoleValues
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
    public enum DraggableValues
    {
        Auto,
        True,
        False
    }

    /// <summary>
    /// Form methods of the form tag
    /// </summary>
    public enum TagFormMethods
    {
        Delete,
        Get,
        Post,
        Put
    }

    /// <summary>
    /// Supported types of the button tag
    /// </summary>
    public enum TagButtonTypes
    {
        Button,
        Reset,
        Submit
    }

    /// <summary>
    /// Supported targets for the anchor tag
    /// </summary>
    public enum TagAnchorTargets
    {
        None,
        _blank,
        _self,
        _parent,
        _top
    }

    /// <summary>
    /// Valid importance items for a tag importance attribute
    /// </summary>
    public enum TagImportanceAttributeValues
    {
        None,
        Auto,
        High,
        Low
    }

    /// <summary>
    /// Supported positions for where to add new tag content
    /// </summary>
    public enum TagContentPositions
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
    public enum MediaApplicationTypes
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
    public enum TextMediaTypes
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
    public enum ImageMediaTypes
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
    public enum FontMediaTypes
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
    public enum ContentPositions
    {
        Body,
        PreElement,
        PreContent,
        PostContent,
        PostElement
    }
}

using Newtonsoft.Json;

namespace Best.XmlDocumentCommentParser
{
    /// <summary>
    /// Property comment info
    /// </summary>
    public class XmlDocumentCommentProperty
    {
        /// <summary>
        /// Property's name
        /// </summary>
        [JsonProperty("PropertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Property's summary
        /// </summary>
        [JsonProperty("PropertyDescription")]
        public string PropertyDescription { get; set; }
    }
}

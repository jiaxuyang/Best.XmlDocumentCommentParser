using Newtonsoft.Json;

namespace Best.XmlDocumentCommentParser
{
    /// <summary>
    /// Field comment info
    /// </summary>
    public class XmlDocumentCommentField
    {
        /// <summary>
        /// Field's name
        /// </summary>
        [JsonProperty("FieldName")]
        public string FieldName { get; set; }

        /// <summary>
        /// Field's summary
        /// </summary>
        [JsonProperty("FieldDescription")]
        public string FieldDescription { get; set; }
    }
}

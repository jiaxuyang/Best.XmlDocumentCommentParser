using Newtonsoft.Json;

namespace Best.XmlDocumentCommentParser
{
    /// <summary>
    /// Method parameter comment info
    /// </summary>
    public class XmlDocumentCommentParameter
    {
        /// <summary>
        /// Method parameter's name
        /// </summary>
        [JsonProperty("ParamName")]
        public string ParamName { get; set; }

        /// <summary>
        /// Method parameter's summary
        /// </summary>
        [JsonProperty("ParamDescription")]
        public string ParamDescription { get; set; }
    }
}

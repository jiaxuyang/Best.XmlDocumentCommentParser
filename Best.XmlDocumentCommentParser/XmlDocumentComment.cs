using Newtonsoft.Json;

namespace Best.XmlDocumentCommentParser
{
    /// <summary>
    /// Xml document of comment
    /// </summary>
    public class XmlDocumentComment
    {
        /// <summary>
        /// Object's Name
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Object's FullName
        /// </summary>
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        /// <summary>
        /// Summary comment
        /// </summary>
        [JsonProperty("Summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Code comment
        /// </summary>
        [JsonProperty("Code")]
        public string Code { get; set; }

        /// <summary>
        /// Exceptions comment
        /// </summary>
        [JsonProperty("Exceptions")]
        public string Exceptions { get; set; }

        /// <summary>
        /// Examples comment
        /// </summary>
        [JsonProperty("Examples")]
        public string Examples { get; set; }

        /// <summary>
        /// Returns comment
        /// </summary>
        [JsonProperty("Returns")]
        public string Returns { get; set; }

        /// <summary>
        /// Remarks comment
        /// </summary>
        [JsonProperty("Remarks")]
        public string Remarks { get; set; }

        /// <summary>
        /// Method's parameters comment info
        /// </summary>
        [JsonProperty("Parameters")]
        public XmlDocumentCommentParameter[] Parameters { get; set; }

        /// <summary>
        /// Class Field's comment info
        /// </summary>
        [JsonProperty("Fields")]
        public XmlDocumentCommentField[] Fields { get; set; }

        /// <summary>
        /// Class Property's comment info
        /// </summary>
        [JsonProperty("Properties")]
        public XmlDocumentCommentProperty[] Properties { get; set; }

        /// <summary>
        /// Class Method's comment info
        /// </summary>
        [JsonProperty("Methods")]
        public XmlDocumentComment[] Methods { get; set; }
    }
}
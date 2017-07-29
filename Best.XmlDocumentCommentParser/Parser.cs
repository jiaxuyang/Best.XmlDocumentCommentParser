using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml.Xsl;
using Jolt;
using Mono.Cecil;
using Newtonsoft.Json;

namespace Best.XmlDocumentCommentParser
{
    /// <summary>
    /// XML comment info parser
    /// </summary>
    public class Parser
    {
        private ConcurrentDictionary<string, XmlDocCommentReader> AssemblyFullnameReaderDictionary { get; set; }

        /// <summary>
        /// XML comment info parser
        /// </summary>
        public Parser()
        {
            AssemblyFullnameReaderDictionary = new ConcurrentDictionary<string, XmlDocCommentReader>();
        }

        #region ParseDocument

        /// <summary>
        /// Get special type's comment info
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public IList<XmlDocumentComment> ParseDocument(Type typeInfo)
        {
            var xmlElement = GetComments(typeInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Type);
            if (string.IsNullOrWhiteSpace(jsonStr))
                return null;

            return JsonConvert.DeserializeObject<IList<XmlDocumentComment>>(jsonStr);
        }

        /// <summary>
        /// Get special type's comment info
        /// </summary>
        /// <param name="typeReference"></param>
        /// <returns></returns>
        public IList<XmlDocumentComment> ParseDocument(TypeReference typeReference)
        {
            var xmlElement = GetComments(typeReference);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Type);
            if (string.IsNullOrWhiteSpace(jsonStr))
                return null;

            return JsonConvert.DeserializeObject<IList<XmlDocumentComment>>(jsonStr);
        }

        /// <summary>
        /// Get special method's comment info
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public XmlDocumentComment ParseDocument(MethodInfo methodInfo)
        {
            var xmlElement = GetComments(methodInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Method);
            if (string.IsNullOrWhiteSpace(jsonStr))
                return null;

            return JsonConvert.DeserializeObject<XmlDocumentComment>(jsonStr);
        }

        /// <summary>
        /// Get special method's comment info
        /// </summary>
        /// <param name="methodReference"></param>
        /// <returns></returns>
        public XmlDocumentComment ParseDocument(MethodReference methodReference)
        {
            var xmlElement = GetComments(methodReference);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Method);
            if (string.IsNullOrWhiteSpace(jsonStr))
                return null;

            return JsonConvert.DeserializeObject<XmlDocumentComment>(jsonStr);
        }

        #endregion

        #region TransformDocumentJson

        /// <summary>
        /// Get special type's comment info in Json string
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public string TransformDocumentJson(Type typeInfo)
        {
            var xmlElement = GetComments(typeInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Type);

            return jsonStr;
        }

        /// <summary>
        /// Get special method's comment info in Json string
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public string TransformDocumentJson(MethodInfo methodInfo)
        {
            var xmlElement = GetComments(methodInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToJson(xmlElement.ToString(), XslType.Method);

            return jsonStr;
        }

        #endregion

        #region TransformDocumentHtml

        /// <summary>
        /// Get special type's comment info in html
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        public string TransformDocumentHtml(Type typeInfo)
        {
            var xmlElement = GetComments(typeInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToHtml(xmlElement.ToString(), XslType.Type);

            return jsonStr;
        }

        /// <summary>
        /// Get special type's comment info in html
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public string TransformDocumentHtml(MethodInfo methodInfo)
        {
            var xmlElement = GetComments(methodInfo);
            if (xmlElement == null)
                return null;

            var jsonStr = TransformXmlToHtml(xmlElement.ToString(), XslType.Method);

            return jsonStr;
        }

        #endregion

        #region GetComments
        /// <summary>
        /// Get special type's comment XElement, include all field/properties/method...
        /// </summary>
        /// <param name="typeInfo"></param>
        /// <returns></returns>
        private XElement GetComments(Type typeInfo)
        {
            var assembly = typeInfo.GetTypeInfo().Assembly;
            if (!AssemblyFullnameReaderDictionary.ContainsKey(assembly.FullName))
                AssemblyFullnameReaderDictionary.TryAdd(assembly.FullName, new XmlDocCommentReader(assembly));

            var reader = AssemblyFullnameReaderDictionary[assembly.FullName];

            var xmlElement = reader.GetComments(typeInfo);

            return xmlElement;
        }

        /// <summary>
        /// Get special type's comment XElement, include all field/properties/method...
        /// </summary>
        /// <param name="typeReference"></param>
        /// <returns></returns>
        private XElement GetComments(TypeReference typeReference)
        {
            var assembly = typeReference.Module.Assembly;
            if (!AssemblyFullnameReaderDictionary.ContainsKey(assembly.FullName))
                AssemblyFullnameReaderDictionary.TryAdd(assembly.FullName,
                    new XmlDocCommentReader(assembly));

            var reader = AssemblyFullnameReaderDictionary[assembly.FullName];

            var xmlElement = reader.GetComments(typeReference);

            return xmlElement;
        }

        /// <summary>
        /// Get special method's comment XElement
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private XElement GetComments(MethodInfo methodInfo)
        {
            if (methodInfo.DeclaringType == null)
                return null;

            var assembly = methodInfo.DeclaringType.GetTypeInfo().Assembly;
            if (!AssemblyFullnameReaderDictionary.ContainsKey(assembly.FullName))
                AssemblyFullnameReaderDictionary.TryAdd(assembly.FullName, new XmlDocCommentReader(assembly));

            var reader = AssemblyFullnameReaderDictionary[assembly.FullName];
            var xmlElement = reader.GetComments(methodInfo);

            return xmlElement;
        }

        private XElement GetComments(MethodReference methodReference)
        {
            if (methodReference.DeclaringType == null)
                return null;

            var assembly = methodReference.Module.Assembly;
            if (!AssemblyFullnameReaderDictionary.ContainsKey(assembly.FullName))
                AssemblyFullnameReaderDictionary.TryAdd(assembly.FullName, new XmlDocCommentReader(assembly));

            var reader = AssemblyFullnameReaderDictionary[assembly.FullName];
            var xmlElement = reader.GetComments(methodReference);

            return xmlElement;
        }

        #endregion

        #region Transform
        /// <summary>
        /// Use this type to decide how to transform comment of type
        /// </summary>
        private enum XslType
        {
            /// <summary>
            /// Type
            /// </summary>
            Type,
            /// <summary>
            /// MethodInfo
            /// </summary>
            Method
        }

        /// <summary>
        /// GetResource's string by XslType enum
        /// </summary>
        /// <param name="xslType"></param>
        /// <returns></returns>
        private string GetXslFileName(XslType xslType)
        {
            switch (xslType)
            {
                case XslType.Type:
                    return "Best.XmlDocumentCommentParser.DocumentationToJson.Type.xsl";
                case XslType.Method:
                    return "Best.XmlDocumentCommentParser.DocumentationToJson.Method.xsl";
                default:
                    throw new ArgumentOutOfRangeException("xslType");
            }
        }

        /// <summary>
        /// Transform xml to json by xsl
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xslType"></param>
        /// <returns></returns>
        private string TransformXmlToJson(string xml, XslType xslType)
        {
            var xmlSr = new StringReader(xml);

            var myXPathDoc = new XPathDocument(xmlSr);
            var myXslTrans = new XslCompiledTransform();

            var type = GetType();
            var fileName = GetXslFileName(xslType);
            var xslStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(fileName);
            if (xslStream == null)
                throw new Exception(string.Format("Resource:{0} is null", fileName));

            var xslSr = new StreamReader(xslStream);
            var xslXr = XmlReader.Create(xslSr);
            myXslTrans.Load(xslXr);

            var htmlBuilder = new StringBuilder();
            var htmlSw = new StringWriter(htmlBuilder);

            myXslTrans.Transform(myXPathDoc, null, htmlSw);

            htmlBuilder.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

            xslStream.Dispose();

            return htmlBuilder.ToString();
        }

        /// <summary>
        /// Transform xml to html by xsl
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="xslType"></param>
        /// <returns></returns>
        private string TransformXmlToHtml(string xml, XslType xslType)
        {
            var xmlSr = new StringReader(xml);

            var myXPathDoc = new XPathDocument(xmlSr);
            var myXslTrans = new XslCompiledTransform();

            var type = GetType();
            var fileName = GetXslFileName(xslType);
            var xslStream = type.GetTypeInfo().Assembly.GetManifestResourceStream(fileName);
            if (xslStream == null)
                throw new Exception(string.Format("Resource:{0} is null", fileName));

            var xslSr = new StreamReader(xslStream);
            var xslXr = XmlReader.Create(xslSr);
            myXslTrans.Load(xslXr);

            var htmlBuilder = new StringBuilder();
            var htmlSw = new StringWriter(htmlBuilder);

            myXslTrans.Transform(myXPathDoc, null, htmlSw);

            htmlBuilder.Replace("<?xml version=\"1.0\" encoding=\"utf-16\"?>", "");

            xslStream.Dispose();

            return htmlBuilder.ToString();
        }
        #endregion
    }
}

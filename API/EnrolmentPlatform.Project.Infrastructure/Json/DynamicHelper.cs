using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EnrolmentPlatform.Project.Infrastructure
{
    /// <summary>
    /// 
    /// </summary>
    public class DynamicHelper
    {
        public static string ToXml(dynamic dynamicObject)
        {
            DynamicXElement xmlNode = dynamicObject;
            return xmlNode.XContent.ToString();
        }

        public static dynamic ToObject(string xml, dynamic dynamicResult) 
        {
            XElement element = XElement.Parse(xml);
            dynamicResult = new DynamicXElement(element);
            return dynamicResult;
        }

        public static dynamic ToObject(string xml)
        {
            XElement element = XElement.Parse(xml);
            dynamic dynamicResult = new DynamicXElement(element);
            return dynamicResult;
        }
    }
}

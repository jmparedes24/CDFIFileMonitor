using System.Xml;

namespace FileMonitor.Common.Helper
{
    public static class XmlHelper
    {
        public static string GetAttributeNodeValue(string attributeName, XmlElement element)
        {
            return FindAttribute(element, attributeName);
        }

        private static string FindAttribute(XmlNode nodeList, string attrName)
        {
            string parentSplit = "";
            XmlNode parent = nodeList.ParentNode;
            if (nodeList.Attributes != null
                && nodeList.Attributes[attrName] != null)
            {
                parentSplit = nodeList.Attributes[attrName].Value;
            }
            else
                parentSplit = FindAttribute(nodeList.ChildNodes, attrName);

            return parentSplit;
        }


        private static string FindAttribute(XmlNodeList nodeListName, string attrName)
        {
            string parentSplit = "";
            foreach (XmlNode xNode in nodeListName)
            {
                XmlNode parent = xNode.ParentNode;
                if (xNode.HasChildNodes)
                    parentSplit = FindAttribute(xNode.ChildNodes, attrName);
                else
                {
                    if (xNode.Attributes != null
                   && xNode.Attributes[attrName] != null)
                    {
                        parentSplit = xNode.Attributes[attrName].Value;
                        break;
                    }
                }
            }

            return parentSplit;
        }
    }
}

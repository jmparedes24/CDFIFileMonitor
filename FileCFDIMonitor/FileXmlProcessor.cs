using FileMonitor.Common.BaseClases;
using FileMonitor.Common.Helper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml;

namespace FileCFDIMonitor
{
    public class FileXmlProcessor : BaseHandleFile
    {
        private string _attributes;
        

        public FileXmlProcessor(string attributes)
        {
            _attributes = attributes;
        }

        public override IDictionary<string,string> GetInfoFile(string fileName)
        {
            try
            {
                
                XmlDocument docXml = new XmlDocument();
                docXml.Load(fileName);
                XmlElement root = docXml.DocumentElement;

                IDictionary<string, string> iDictionaryValuesXml = new Dictionary<string, string>();

                var attributes = _attributes.Split(',');

                foreach(var attribute in attributes)
                {
                    var attrName = attribute.Split('|');

                    var attrValue = XmlHelper.GetAttributeNodeValue(attrName[1], root);

                    if (string.IsNullOrEmpty(attrValue))
                        throw new Exception(FileMonitor.Common.Constants.errorMessageNotFoundAttribute);

                    iDictionaryValuesXml.Add(attrName[0], attrValue);                    
                }
                return iDictionaryValuesXml;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}

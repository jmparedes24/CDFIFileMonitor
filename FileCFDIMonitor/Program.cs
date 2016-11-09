using FileMonitor.Common.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;

namespace FileCFDIMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var appFilePath = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.pathFileKey];
            var fileExtensions = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.fileExtensionsKey];
            var attributesNames = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.attributesNames];
             
            FileXmlProcessor fileProcessor = new FileXmlProcessor(attributesNames);
            var fileExtensionsList = fileExtensions.Split(',').ToList();

            var fileNames = fileProcessor.GetFilesName(appFilePath, fileExtensionsList);

            foreach (var fileName in fileNames)
            {
                IDictionary<string,string> myDictionaryValues = fileProcessor.GetInfoFile(fileName);

                var model = new BdospModel();

                var modelToInsert = ReflectionHelper.GetObject<BdospModel>(model, myDictionaryValues);

                using (var db = new ObradorDBContext())
                {            
                    db.Entry(modelToInsert).State = EntityState.Added;

                    db.SaveChanges();
                }

                Console.WriteLine("Model:{0}", modelToInsert.ToString());
            }
                

            Console.ReadLine();
        }
        
    }
}

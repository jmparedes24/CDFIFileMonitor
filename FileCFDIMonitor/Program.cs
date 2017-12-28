using FileMonitor.Common.Helper;
using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.IO;
using System.Linq;

namespace FileCFDIMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.BasicConfigurator.Configure();
            ILog log = log4net.LogManager.GetLogger(typeof(Program));

            try
            {
                var appFilePath = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.pathFileKey];
                var fileExtensions = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.fileExtensionsKey];
                var attributesNames = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.attributesNames];
                var processedPath = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.processedPathKey];
                var errorPath = ConfigurationManager.AppSettings[FileMonitor.Common.Constants.errorPathKey];

                
                FileXmlProcessor fileProcessor = new FileXmlProcessor(attributesNames);
                var fileExtensionsList = fileExtensions.Split(',').ToList();

                var fileNames = fileProcessor.GetFilesName(appFilePath, fileExtensionsList);

                foreach (var fileName in fileNames)
                {
                    try
                    {

                        IDictionary<string, string> myDictionaryValues = fileProcessor.GetInfoFile(fileName);

                        var model = new BdospModel();

                        var modelToInsert = ReflectionHelper.GetObject<BdospModel>(model, myDictionaryValues);

                        using (var db = new ObradorDBContext())
                        {
                            db.Entry(modelToInsert).State = EntityState.Added;

                            db.SaveChanges();
                        }

                        fileProcessor.MoveFile(fileName, processedPath);
                     
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Equals(FileMonitor.Common.Constants.errorMessageNotFoundAttribute))
                        {
                            fileProcessor.MoveFile(fileName, errorPath);
                            log.Error(string.Format("{0} on file: {1}",FileMonitor.Common.Constants.errorMessageNotFoundAttribute,Path.GetFileName(fileName)), ex);
                        }                           
                        else
                            throw ex;
                                                
                    }
                }
                
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }            
        }
        
    }
}

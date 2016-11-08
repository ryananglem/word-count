using System;
using System.Configuration;
using Ninject.Modules;

namespace wordcount
{
    public class WordCountModule : NinjectModule
    {
        public override void Load()
        {
            Bind<services.IDocumentRepo>().To<textfilerepo.TextFileDocumentRepo>()
                .WithConstructorArgument("chunkSize", Convert.ToInt32(GetConfigValue("textChunkSize")))           
                .WithConstructorArgument("fileName", GetConfigValue("fileName"));

            Bind<services.IService>().To<services.Service>();
        }

        private string GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}

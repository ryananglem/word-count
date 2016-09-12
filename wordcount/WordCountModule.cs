using Ninject.Modules;

namespace wordcount
{
    public class WordCountModule : NinjectModule
    {
        public override void Load()
        {
            // chunk size could be stored in app.settings
            // filename could be in the method calls 
            Bind<services.IRepo>().To<textfilerepo.Repo>()
                .WithConstructorArgument("chunkSize", 100)
                .WithConstructorArgument("fileName", "text.txt");
            Bind<services.IService>().To<services.Service>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ninject.Modules;
using Raven.Client;
using Ninject.Activation;
using Ninject;
using Ninject.Web.Common;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using Ninject.Extensions.Conventions;
using System.Reflection;
using Raven.Database.Server;

namespace ComicListMobile.App_Start
{
    public class RavenModule : NinjectModule
    {
        public override void Load()
        {
            /*Kernel.Bind(x => 
                x.FromThisAssembly()
                .SelectAllClasses()
                .InheritedFrom<AbstractIndexCreationTask>()
                .BindAllInterfaces()
                .
            */

            // register ravendb document store
            // - singleton, started only once
            Bind<IDocumentStore>()
                .ToMethod(InitDocStore)
                .InSingletonScope();

            // register ravendb document store session
            // - one session per http request
            Bind<IDocumentSession>()
                .ToMethod(c => c.Kernel.Get<IDocumentStore>().OpenSession())
                .InRequestScope();
        }

        private IDocumentStore InitDocStore(IContext context)
        {

            NonAdminHttp.EnsureCanListenToWhenInNonAdminContext(8080);

            var ds = new EmbeddableDocumentStore() { UseEmbeddedHttpServer = true };
            
            // attach profiler to the docstore
            MvcMiniProfiler.RavenDb.Profiler.AttachTo(ds);
            
            // initialize docstore
            ds.Initialize();

            // create indexes found in the web project
            IndexCreation.CreateIndexes(Assembly.GetExecutingAssembly(), ds);

            return ds;
        }
    }
}
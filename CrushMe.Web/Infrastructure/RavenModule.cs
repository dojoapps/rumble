using CrushMe.Core.Indexes;
using CrushMe.Core.Tasks;
using Ninject.Modules;
using Ninject.Web.Common;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Extensions;
using Raven.Client.Indexes;
using Raven.Client.MvcIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Web.Infrastructure
{
    public class RavenModule : NinjectModule
    {
        public override void Load()
        {
            /*var documentStore = new DocumentStore()
            {
                ConnectionStringName = "RavenDB"
            }.Initialize();

            documentStore.DatabaseCommands.EnsureDatabaseExists("Grupando");

            IndexCreation.CreateIndexes(typeof(Users_Index).Assembly, documentStore);

#if DEBUG
            RavenProfiler.InitializeFor(documentStore, "PasswordHash");
#endif

            this.Bind<IDocumentStore>().ToConstant(documentStore).InSingletonScope();

            this.Bind<IDocumentSession>().ToMethod(x => documentStore.OpenSession()).InRequestScope();

            TaskExecutor.DocumentStore = documentStore;*/
        }
    }
}
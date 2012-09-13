using CrushMe.Core.Indexes;
using Raven.Client;
using Raven.Client.Embedded;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testing.CrushMe
{
    public abstract class BaseRavenTest
    {
        public IDocumentStore Store { get; set; }

        public IDocumentSession Session { get; set; }

        protected BaseRavenTest()
	    {
            Store = new EmbeddableDocumentStore()
            {
                RunInMemory = true

            }.Initialize();

            IndexCreation.CreateIndexes(typeof(Users_Index).Assembly, Store);

            Session = Store.OpenSession();
	    }

        ~BaseRavenTest()
        {
            Session.Dispose();
            Store.Dispose();
        }
    }
}

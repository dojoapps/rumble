using CrushMe.Core.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Core.Indexes
{
    public class Users_Index : AbstractIndexCreationTask<User>
    {
        public Users_Index()
        {
            this.Map = users => from u in users
                                select new
                                {
                                    u.Id,
                                    u.Name
                                };

            this.Index(x => x.Name, FieldIndexing.Analyzed);
        }
    }
}
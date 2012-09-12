using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CrushMe.Core.Helpers
{
    public static class RavenExtensions
    {
        public static string BuildRavenId<T>(this IDocumentSession session, ValueType id)
        {
            return session.Advanced.DocumentStore.Conventions.FindTypeTagName(typeof(T)) + "/" + id;
        }

        public static int ToIntId(this string stringId)
        {
            return int.Parse(stringId.Split('/')[1]);
        }

        public static long ToLongId(this string stringId)
        {
            return long.Parse(stringId.Split('/')[1]);
        }
    }
}
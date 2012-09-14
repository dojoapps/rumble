using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrushMe.Common.Infrastructure
{
    public interface IFormsAuthentication
    {
        void SetAuthCookie(string name, bool permanentCookie);
        void SignOut();
    }
}

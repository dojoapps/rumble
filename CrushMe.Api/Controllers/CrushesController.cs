using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using CrushMe.Api.Models.Crushes;

namespace CrushMe.Api.Controllers
{
    public class CrushesController : BaseApiController
    {
        [GET("/api/crushes/received")]
        public IEnumerable<CrushReceivedApiModel> GetCrushesReceived()
        {
            var model = new List<CrushReceivedApiModel>();
            var currentUserId = this.CurrentUser.FbId;
            foreach (var crush in db.Crushes.Where(x => x.Target.FbId == currentUserId))
            {
                //model.Add(new CrushReceivedApiModel() { 
                //    CrushDate = crush.DateCreated,
                //    CrusherFbId = crush.CrusherId.Value,
                //    Options = crush.o
                //});
            }
            return model;
        }
    }
}

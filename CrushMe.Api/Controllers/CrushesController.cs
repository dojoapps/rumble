﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;
using CrushMe.Api.Models.Crushes;
using CrushMe.Database.Infrastructure;
using CrushMe.Api.Services;

namespace CrushMe.Api.Controllers
{
    public class CrushesController : BaseApiController
    {
        [GET("/api/crushes/received")]
        public IEnumerable<CrushReceivedApiModel> GetCrushesReceived()
        {
            var currentUserId = this.CurrentUser.FbId;
            var model = db.Crushes
                .Where(x => x.Target.FbId == currentUserId)
                .MapTo<CrushReceivedApiModel>();
            return model;
        }

        [GET("/api/crushes/sent")]
        public IEnumerable<CrushSentApiModel> GetCrushesSent()
        {
            var currentUserId = this.CurrentUser.FbId;
            var model = db.Crushes
                .Where(x => x.Crusher.FbId == currentUserId)
                .MapTo<CrushSentApiModel>();
            return model;
        }

        [POST("/api/crushes/sent/{fbid}")]
        public CrushSentApiModel Crush(long fbid, int? crushFather = null)
        {
            return CrushServices.Crush(db, CurrentUser.FbId, fbid, crushFather)
                .MapTo<CrushSentApiModel>();
        }
    }
}

define([
  // Application.
  "app",

  "modules/crush-sent",
  "modules/crush-received"
],

function(app,CrushSent,CrushReceived) {

  // Defining the application router, you can attach sub routers here.
  var Router = Backbone.Router.extend({
    routes: {
      "": "index",
      "crush/:direction/:crush_id" : "detail"
    },

    index: function() {
      var crushesSent = CrushSent.List(),
          crushesReceived = CrushReceived.List();

      app.useLayout("main").setView({
        ".received": new CrushReceived.Views.List({
          collection: _.filter(crushesReceived,function(crush) {
            return crush.get("status") === Crush.STATUS.RECEIVED;
          });
        }),
        ".pending": new CrushReceived.Views.List({
          collection: _.filter(crushesReceived,function(crush) {
            return crush.get("status") === Crush.STATUS.PENDING;
          });
        },
        ".sent": new CrushSent.Views.List({
          collection : crushesSent
        })
      }).render();

      crushsSent.fetch();
      crushesReceived.fetch();
    },

    detail: function(direction, crush_id) {
      
    }
  });

  return Router;

});

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
      "crush/:direction/:crush_id": "detail"
    },

    index: function() {
      var crushesSent = new CrushSent.List(),
          crushesReceived = new CrushReceived.List();

      crushesReceived.fetch();

      var l = app.useLayout("main");

      l.setViews({
        ".crushes-pending": new CrushReceived.Views.List({
          collection: new CrushReceived.List(crushesReceived.filter(function(crush) {
            return crush.get("status") === app.constants.CRUSH_STATUS.PENDING;
          }))
        }),
        ".crushes-replied": new CrushReceived.Views.List({
          collection: new CrushReceived.List(crushesReceived.filter(function(crush) {
            return crush.get("status") != app.constants.CRUSH_STATUS.PENDING;
          }))
        })/*,
        ".sent": new CrushSent.Views.List({
          collection : crushesSent
        })*/
      }).render();



      /*crushsSent.fetch();*/
      
    },

    detail: function(direction, crush_id) {
      
    }
  });

  return Router;

});

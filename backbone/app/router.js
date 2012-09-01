define([
  // Application.
  "app"
],

function(app) {

  // Defining the application router, you can attach sub routers here.
  var Router = Backbone.Router.extend({
    routes: {
      "": "index",
      "crush/:crush_id" : "detail"
    },

    index: function() {

    },

    detail: function(crush_id) {
      
    }
  });

  return Router;

});

define([
  "app",

  // Libs
  "backbone",

  // Views
  "modules/todo/views",

  // Plugins
  "plugins/backbone-localstorage"
],

function(app, Backbone, Views) {

  // Create a new module
  var CrushSent = app.module();

  CrushSent.Model = Backbone.Model.extend({
    // Default attributes for the todo.
    defaults: {
      id: null,
      targetId: null,
      targetName: null,
      status: null,
      dateSent: null
    }
  });

  // TODO: Crush Collection
  CrushSent.List = Backbone.Collection.extend({
    model : CrushSent.Model,

    url : "/crushes/sent"
  });
  
  // Required, return the module for AMD compliance
  return CrushSent;

});

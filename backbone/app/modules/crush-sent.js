define([
  "app",

  // Libs
  "backbone",

  // Views
  "modules/sent/views"
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

  CrushSent.Views = Views;
  
  // Required, return the module for AMD compliance
  return CrushSent;

});

define([
  "app",

  // Libs
  "backbone",

  // Views
  "modules/received/views"
],

function(app, Backbone, Views) {

  // Create a new module
  var CrushReceived = app.module();

  CrushReceived.Status = {
    PENDING: 0,
    REPLIED: 1
  };

  CrushReceived.Model = Backbone.Model.extend({
    // Default attributes for the todo.
    defaults: {
      id: null,
      candidates: [],
      dateSent: null,
      status: null
    }
  });

  CrushReceived.List = Backbone.Collection.extend({
    model: CrushReceived.Model,

    url: "/api/crushes/received"
  });

  CrushReceived.Views = Views;
  
  // Required, return the module for AMD compliance
  return CrushReceived;

});

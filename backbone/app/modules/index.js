define([
  "app",

  // Libs
  "backbone"
],

function(app, Backbone) {

  // Create a new module
  var Index = app.module();

  Index.Model = Backbone.Model.extend({
    // Default attributes for the todo.
    defaults: {
      crushes : []
    }
  }); 

  // Required, return the module for AMD compliance
  return Index;

});

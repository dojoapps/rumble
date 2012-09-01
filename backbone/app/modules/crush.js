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
  var Crush = app.module();

  Crush.DIRECTIONS = {
    FROM_USER : 0,
    TO_USER : 1
  };

  Crush.STATUS = {
    PENDING : 0,
    MATCH : 1,
    NO_MATCH : 2
  };

  Crush.Model = Backbone.Model.extend({
    // Default attributes for the todo.
    defaults: {
      id: null,
      senderId: null,
      crushOptions: [],
      dateSent: null,
      status: null
    }
  });

  // TODO: Crush Collection
  
  // Required, return the module for AMD compliance
  return Crush;

});

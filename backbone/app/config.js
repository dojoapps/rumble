// Set the require.js configuration for your application.
require.config({

  // Initialize the application with the main application file.
  deps: ["main"],

  paths: {
    // JavaScript folders.
    libs: "../assets/js/libs",
    plugins: "../assets/js/plugins",
    mocking: "../assets/js/mocking",

    // Libraries.
    jquery: "../assets/js/libs/jquery",
    bootstrap: "../assets/js/libs/bootstrap",
    lodash: "../assets/js/libs/lodash",
    backbone: "../assets/js/libs/backbone",
    handlebars: "../assets/js/libs/handlebars"
  },

  shim: {
    // Backbone library depends on lodash and jQuery.
    backbone: {
      deps: ["lodash", "jquery"],
      exports: "Backbone"
    },

    handlebars: {
      exports: "Handlebars"
    },

    bootstrap: ["jquery"],

    // Backbone.LayoutManager depends on Backbone.
    "plugins/backbone.layoutmanager": ["backbone"],
    "mocking/apimock" : ["backbone"]
  }

});

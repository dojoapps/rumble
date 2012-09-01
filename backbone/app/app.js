define([
  // Libraries.
  "jquery",
  "lodash",
  "backbone",
  "handlebars",

  "bootstrap",

  // Mocking
  "mocking/apimock",

  // Plugins.
  "plugins/backbone.layoutmanager"
],

function($, _, Backbone, Handlebars) {

  // Provide a global location to place configuration settings and module
  // creation.
  var app = {
    // The root path to run the application.
    root: "/",
    authResponse : null,
    constants : {
      CRUSH_DIRECTION : {
        RECEIVED : 0,
        SENT : 1
      },
      CRUSH_STATUS : {
        PENDING : 0,
        MATCH : 1,
        NO_MATCH : 2
      }
    }
  };

  // Localize or create a new JavaScript Template object.
  var JST = window.JST = window.JST || {};

  // Configure LayoutManager with Backbone Boilerplate defaults.
  Backbone.LayoutManager.configure({
    paths: {
      layout: "app/templates/layouts/",
      template: "app/templates/"
    },

    fetch: function(path) {
      path = path + ".html";

      if (!JST[path]) {
        $.ajax({ url: app.root + path, async: false }).then(function(contents) {
          JST[path] = Handlebars.compile(contents);
        });
      } 
      
      return JST[path];
    }
  });

  // Configura integracao com facebook
  window.fbAsyncInit = function() {
    FB.init({
      appId      : '440596072632150', // App ID
      channelUrl : '//localhost:57324/channel.html', // Channel File
      status     : true, // check login status
      cookie     : true, // enable cookies to allow the server to access the session
      xfbml      : true  // parse XFBML
    });

    FB.Event.subscribe('auth.authResponseChange', function(response) {
      if ( response.status === 'connected' ) {
        app.authResponse = response.authResponse; 
        $.ajaxSetup({
          headers : {
            ACCESS_TOKEN : response.authResponse.accessToken
          }
        });
      } else if (response.status === 'not_authorized') {
        console.log("FB: Sem permissao", response);
      } else {
        console.log("FB: Erro!", response);
      }
    });
  };

  // Load the SDK Asynchronously
  (function(d){
    var js, id = 'facebook-jssdk', ref = d.getElementsByTagName('script')[0];
    if (d.getElementById(id)) {return;}
    js = d.createElement('script'); js.id = id; js.async = true;
    js.src = "//connect.facebook.net/en_US/all.js";
    ref.parentNode.insertBefore(js, ref);
  }(document));


  // Mix Backbone.Events, modules, and layout management into the app object.
  return _.extend(app, {
    // Create a custom object with a nested Views object.
    module: function(additionalProps) {
      return _.extend({ Views: {} }, additionalProps);
    },

    // Helper for using layouts.
    useLayout: function(name) {
      // If already using this Layout, then don't re-inject into the DOM.
      if (this.layout && this.layout.options.template === name) {
        return this.layout;
      }

      // If a layout already exists, remove it from the DOM.
      if (this.layout) {
        this.layout.remove();
      }

      // Create a new Layout.
      var layout = new Backbone.Layout({
        template: name,
        className: "layout " + name,
        id: "layout"
      });

      // Insert into the DOM.
      $("#main").empty().append(layout.el);

      // Render the layout.
      layout.render();

      // Cache the refererence.
      this.layout = layout;

      // Return the reference, for chainability.
      return layout;
    }
  }, Backbone.Events);

});

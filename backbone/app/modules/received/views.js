define([
  "app",

  // Libs
  "backbone"
],

function(app, Backbone) {
	var Views = {};

	Views.ListItem = Backbone.View.extend({
		template: "received/item",

		tagName: "tr",

		events : {
			"click": "details"
		},

		click: function() {
			app.navigate("crush/" + app.constants.CRUSH_DIRECTION.RECEIVED + "/" + this.model.get("id"), true );
		}
	});

	return Views;
});
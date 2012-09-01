define([
  "app",

  // Libs
  "backbone"
],

function(app, Backbone) {
	var Views = {};

	Views.ListItem = Backbone.View.extend({
		template: "received/item",

		tagName: "li",

		className: "clearfix",

		events : {
			"click": "details"
		},

		details: function() {
			app.navigate("crush/" + app.constants.CRUSH_DIRECTION.RECEIVED + "/" + this.model.get("id"), true );
		},

		serialize: function() {
			return {
				status: this.model.get("status"),
				candidates: this.model.get("candidates")
			};
		}
	});

	Views.List = Backbone.View.extend({

		render: function(manage) {
			this.collection.each(function(item) {
				this.insertView(new Views.ListItem({
			 		model: item
				}));
			}, this);

			return manage(this).render();
		},

		initialize: function() {
			this.collection.on("reset", function() {
				this.render();
			}, this);

			this.collection.on("add", function(item) {
				this.insertView(new Views.Item({
			 	 model: item
				})).render();
			}, this);
		}
	});

	return Views;
});
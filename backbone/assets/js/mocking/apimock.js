define([
"backbone"
],
function(Backbone) {
	Backbone.sync = function(method, model, options) {
    if ( model.url === "/api/crushes/received" ) {
      if ( method === "read" ) {
        options.success([
          {
            id: 0,
            dateSent: '2012-09-01',
            status: 0,
            candidates: [
              {
                name: "fulano",
                id: "99999999"
              }
            ]
          },
          {
            id: 1,
            dateSent: '2012-09-01',
            status: 1,
            candidates: [
              {
                name: "fulano",
                id: "99999999"
              },
              {
                name: "fulano 3",
                id: "99999999"
              },
              {
                name: "fulano 2",
                id: "99999999"
              }
            ]
          }
        ]);
      }
    }
  }
});
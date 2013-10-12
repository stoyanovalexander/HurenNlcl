var app = app || {};

(function(a) {
    var viewModel = kendo.observable({
        continent: [],
        
    });
    
    function init(e) {
        kendo.bind(e.view.element, viewModel);
        httpRequest.getJSON( app.servicesBaseUrl + "continent")
        .then(function (continent) {
            viewModel.set("continent", continent);
        });        
    }
    
    a.continents = {
        init:init          
    };
}(app));
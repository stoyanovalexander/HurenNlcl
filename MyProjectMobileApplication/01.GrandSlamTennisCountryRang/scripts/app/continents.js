var app = app || {};

(function(a) {
    var viewModel = kendo.observable({
        continentTake: [], 
    });
    
    function init(e) {
        kendo.bind(e.view.element, viewModel);
        httpRequest.getJSON( app.servicesBaseUrl + "continent")
        .then(function (continent) {
            viewModel.set("continentTake", continent);
        });        
    }
    
    a.continents = {
        init:init          
    };
}(app));
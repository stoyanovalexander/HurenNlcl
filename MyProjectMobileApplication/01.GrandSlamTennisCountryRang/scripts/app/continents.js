var app = app || {};

(function(a) {
    var viewModel = kendo.observable({
        continentTake: [],
        selectedContinent: null,
        change: onContinentChanged
    });
    
    function init(e) {
        kendo.bind(e.view.element, viewModel);
        httpRequest.getJSON( app.servicesBaseUrl + "continent")
        .then(function (continent) {
            viewModel.set("continentTake", continent);
        });        
    }
    
    function onContinentChanged(e) {
        httpRequest.getJSON(app.servicesBaseUrl + "continent?name=" + e.sender._selectedValue)
        .then(function (continentChange) {
            viewModel.set("selectedContinent", continentChange);
        });
    }

    a.continents = {
        init:init          
    };
}(app));
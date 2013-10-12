var app = app || {};

(function(a) {
    function getByLocation() {
        cordovaExt.getLocation().
        then(function(location) {
            var locationString = location.coords.latitude + "," + location.coords.longitude;            
            return httpRequest.getJSON(app.servicesBaseUrl + "country/loc?longt=" + location.coords.longitude + "&lat" + location.coords.latitude);
            //GET api/country/loc?longt={longt}&lat={lat}
        })
        .then(function (countriesByLocation2) {
            viewModel.set("countriesByLocation32", countriesByLocation2);
            //console.log(places);
        });
    }
    var viewModel = kendo.observable({
        countriesByLocation32: [],
        getByLocation: getByLocation
    });
    function init(e) {
        kendo.bind(e.view.element, viewModel);
        getByLocation();
    }

    a.countriesByLocations = {
        init: init
    };
}(app));
var app = app || {};

(function (a) {
    var viewModel = kendo.observable({
        countrieByLocationTake: [],
    });

    function init(e) {
        kendo.bind(e.view.element, viewModel);
        cordovaExt.getLocation().
         then(function (location) {
             var locationString = location.coords.latitude + "," + location.coords.longitude;
             console.log(locationString);
             return httpRequest.getJSON(app.servicesBaseUrl +
                 "country/loc?longt=" + +location.coords.longitude + "&lat=" + location.coords.latitude);
         })
        .then(function (countriesByLocation) {
            console.log(countriesByLocation);
            viewModel.set("countrieByLocationTake", countriesByLocation);
        });
    }

    a.byLocations = {
        init: init
    };
}(app));

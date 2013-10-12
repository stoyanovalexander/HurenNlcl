var app = app || {};

(function (a) {
    var viewModel = kendo.observable({
        countriesByLocation132: [],

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
        .then(function (countriesByLocation12) {
            console.log(countriesByLocation12);
            viewModel.set("countriesByLocation132", countriesByLocation12);
        });
    }

    a.byLocations = {
        init: init
    };
}(app));

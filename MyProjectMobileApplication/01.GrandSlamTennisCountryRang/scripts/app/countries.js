var app = app || {};

(function (a) {
    function getByWins() {
        httpRequest.getJSON(app.servicesBaseUrl + "country/win")
        .then(function (countriesByWin2) {
            viewModel.set("theCountries", countriesByWin2);
        });
    }

    function getByLocation() {
        cordovaExt.getLocation().
        then(function (location) {
            //var locationString = location.coords.latitude + "," + location.coords.longitude;
            return httpRequest.getJSON(app.servicesBaseUrl + "country/loc?longt=" + +location.coords.longitude + "&lat=" + location.coords.latitude);
        })
        .then(function (countriesByLocation12) {
            viewModel.set("theCountries", countriesByLocation12);
            //console.log(places);
        });
    }

    var viewModel = kendo.observable({
        theCountries: [],
        getByWins: getByWins,
        getByLocation: getByLocation
    });

    function init(e) {
        kendo.bind(e.view.element, viewModel);
        getByWins();
    }

    a.countries = {
        init: init
    };
}(app));

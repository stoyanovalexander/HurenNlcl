var app = app || {};

(function (a) {
    function getByWins() {
        httpRequest.getJSON(app.servicesBaseUrl + "country/win")
        .then(function (countriesByWin) {
            viewModel.set("countriesTake", countriesByWin);
        });
    }

    function getByLocation() {
        cordovaExt.getLocation().
        then(function (location) {
            return httpRequest.getJSON(app.servicesBaseUrl + "country/loc?longt=" +
                location.coords.longitude + "&lat=" + location.coords.latitude);
        })
        .then(function (countriesByLocation) {
            viewModel.set("countriesTake", countriesByLocation);
        });
    }

    var viewModel = kendo.observable({
        countriesTake: [],
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

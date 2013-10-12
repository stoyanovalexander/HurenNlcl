var app = app || {};

(function (a) {
    var viewModel = kendo.observable({
        countriesByWin32: [],

    });

    function init(e) {
        kendo.bind(e.view.element, viewModel);
        httpRequest.getJSON(app.servicesBaseUrl + "country/win")
        .then(function (countriesByWin2) {
            viewModel.set("countriesByWin32", countriesByWin2);
        });
    }

    a.countriesByWins = {
        init: init
    };
}(app));

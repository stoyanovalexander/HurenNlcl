var app = app || {};

(function (a) {
    var viewModel = kendo.observable({
        countriesByWinTake: [],
    });

    function init(e) {
        kendo.bind(e.view.element, viewModel);
        httpRequest.getJSON(app.servicesBaseUrl + "country/win")
        .then(function (countriesByWin) {
            viewModel.set("countriesByWinTake", countriesByWin);
        });
    }

    a.countriesByWins = {
        init: init
    };
}(app));

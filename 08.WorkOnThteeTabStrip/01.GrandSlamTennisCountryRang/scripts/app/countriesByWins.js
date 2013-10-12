var app = app || {};

(function (a) {
    var viewModel = kendo.observable({
        countriesByWin32: [],

    });

    function init(e) {
        kendo.bind(e.view.element, viewModel);
        //httpRequest.getJSON("http://localhost:62354/api/" + "categories")
        httpRequest.getJSON(app.servicesBaseUrl + "country/win")
        .then(function (countriesByWin2) {
            viewModel.set("countriesByWin32", countriesByWin2);
        });
    }

    // function onCategoryChanged(e) {             
    //     console.log(e.sender._selectedValue);
    //     
    //     httpRequest.getJSON(app.servicesBaseUrl  + "categories/" + e.sender._selectedValue)
    //     .then(function(category) {
    //         viewModel.set("selectedCategory", category);
    //         console.log(category);
    //     });
    // }

    a.countriesByWins = {
        init: init
    };
}(app));

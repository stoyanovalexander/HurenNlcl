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
             return httpRequest.getJSON(app.servicesBaseUrl + "country/loc?longt=" + location.coords.longitude + "&lat=" + location.coords.latitude);
             //GET api/country/loc?longt={longt}&lat={lat}
         })
        .then(function (countriesByLocation12) {
            viewModel.set("countriesByLocation132", countriesByLocation12);
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

    a.byLocations = {
        init: init
    };
}(app));

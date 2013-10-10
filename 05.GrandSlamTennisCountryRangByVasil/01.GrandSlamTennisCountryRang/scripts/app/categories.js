var app = app || {};

(function(a) {
    var viewModel = kendo.observable({
        continent: [],
        
    });
    
    function init(e) {
        kendo.bind(e.view.element, viewModel);
        
        //httpRequest.getJSON("http://localhost:62354/api/" + "categories")
        httpRequest.getJSON( app.servicesBaseUrl + "continent")
        .then(function (continent) {
            viewModel.set("continent", continent);
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
    
    a.categories = {
        init:init          
    };
}(app));
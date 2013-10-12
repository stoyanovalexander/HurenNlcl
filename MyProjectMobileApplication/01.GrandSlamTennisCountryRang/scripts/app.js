var app = app || {};

(function() {
    
    document.addEventListener("deviceready", function() {
        app.servicesBaseUrl = "http://localhost:61886/api/";
        
        var kendoApp = new kendo.mobile.Application(document.body);
    });    
}());
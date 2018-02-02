(function (app) {
    
    var LogController = function () {
        $scope.message = "Tests message";
    };
    app.controller("LogController", LogController);

}(angular.module("logs")));
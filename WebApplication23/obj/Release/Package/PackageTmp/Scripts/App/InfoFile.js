
var myApp = angular.module('App', []);

myApp.controller('UserController', ['$scope', '$http', function ($scope, $http) {


    /@ for get data about device@/
    $http.get('/api/files').then(function (response) {
        $scope.start = response.data.show;
        $scope.infos = response.data.inf;
        $scope.sta = response.data.S;

    });
    /@for show data in folder@/
    $scope.Go = function (inf) {


        $http.get('/api/files', {
            params: {
                p1: inf, p2: "0"

            }
        }).then(function (response) {
            $scope.start = response.data.show;
            $scope.infos = response.data.inf;
            $scope.sta = response.data.S;
            $scope.small = response.data.small;
            $scope.normal = response.data.normal;
            $scope.big = response.data.big;
        });


    };
    /@for go back in prev folder@/
    $scope.Back = function () {


        $http.get('/api/files', {
            params: {
                p1: "0",
                p2: "1"

            }
        }).then(function (response) {
            $scope.start = response.data.show;
            $scope.infos = response.data.inf;
            $scope.sta = response.data.S;
            $scope.small = response.data.small;
            $scope.normal = response.data.normal;
            $scope.big = response.data.big;
        });


    };




}]);
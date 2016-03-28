var app = angular.module('app', []);
app.controller('MeuCtrl', ['$scope', '$http', function () {
    $scope.title = "Meu Controller";
    $scope.token = '';
    $scope.autenticar = function ()
    {
        DoAuth();
    }
    var authurl = 'http://localhost/api/security/token';
    function DoAuth() {
        var data = "grant_type=password&username=usuario&password=senha";
        $http.post(authurl, data, { headers: { 'Content-type': 'x-www-form-urlencoded' }})
        .success(function(result){
            console.log(result);
            $scope.token = result.access_token;
        });
    }
}]);
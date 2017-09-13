var app = angular.module("TviterApp", ["TviterApp.controllers", "ngRoute", "ui.bootstrap"]);

app.config(["$routeProvider", function ($routeProvider, $locationProvider) {
    $routeProvider
        .when("/post", { templateUrl: "Partials/post.html", controller: "PostController" })
        .otherwise({ redirectTo: "/" });
}])

app.filter("mydate", function () {
    var re = /\/Date\(([0-9]*)\)\//;
    return function (x) {
        var m = x.match(re);
        if (m) return new Date(parseInt(m[1]));
        else return null;
    };
})
app.filter('reverse', function () {
    return function (items) {
        return items.slice().reverse();
    };
})
app.directive('myOnKeyDownCall', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            scope.$apply(function () {
                scope.$eval(attrs.ngEnter);
            });
            //event.preventDefault();
        });
    };
})
app.factory('userService', function ($http) {
    var data; // this is private

    return {
        users: function () {
            return data;
        },
        addUser: function (userName) {
            data = userName;
        },
        deleteUser: function (userName) {

        }
    
    }
   


    /*
    var factory = {}
    var user;
    factory.addUser= function(value) {
        alert(value);
        user = value;
    }
    factory.getUser = function () {
        alert("get User " + user);
        return $http.get("/Search/GetOtherUser", {otherUserName : user});
    }
    return factory;
    */
});

    

angular.module("TviterApp.controllers", [])
.controller("PostController", function ($scope,PostService, $routeParams, $route, $window)
{
   
    $scope.message = "PostController";


    $scope.addPost = function () {

        PostService.AddPost($scope.post);
        setTimeout(function () {
            $window.location.assign("/home/index");
        }, 500);
    }
    $scope.save = function (PostId, PostDescription) {

        PostService.EditPost(PostId, PostDescription);
        setTimeout(function () {
            $window.location.assign("/home/index");
        }, 500);
    };
    $scope.editPost = function () {

       
        if ($scope.editorEnabled) {
            $scope.editorEnabled = false;
        }
        else {
            $scope.editorEnabled = true;
        }
       
    }
    $scope.bljakPost = function (postId) {
        PostService.BljakPosts(postId);
        setTimeout(function () {
            $window.location.assign("/home/index");
        }, 500);
    }
    $scope.likePost = function (postId) {
        PostService.LikePosts(postId);
        setTimeout(function () {
            $window.location.assign("/home/index");
        }, 500);

    }
   
    $scope.deletePost = function (PostId) {
        PostService.DeletePost(PostId);
        setTimeout(function () {
            $window.location.assign("/home/index");
        }, 500);
    }
    /*
    $scope.showAllPosts = function () {
        PostService.GetAllPosts().then(function (response) {
            $scope.allPosts = response.data.listOfAllPosts;
        });
    }
    */
    
    $scope.$watch('$viewContentLoaded', function () {
        PostService.GetPostsByUser().then(function (response) {
            $scope.allPosts2 = response.data.listOfAllPosts;
            
        });
    });
    
}

)
.controller("HomeController", function ($scope, PostService, $routeParams, $window) {
    $scope.message = "HomeController";
    $scope.user;
    $scope.profile;

    $scope.editUser = function () {

        //alert($scope.model.UserDescription);
        PostService.EditUser($scope.model.Email,$scope.model.CustomUsername, $scope.model.UserDescription);
        setTimeout(function () {
           
            $window.location.reload();
        }, 500);
    }
    $scope.deleteUser = function () {

        //alert($scope.model.UserDescription);
        PostService.deleteUser();
        setTimeout(function () {
            $window.location.reload();
        }, 500);
    }
    

    // GETTING SPECIFIC USER
    $scope.$watch('$viewContentLoaded', function () {
        PostService.GetUser().then(function (response) {
            $scope.user = response.data.user;
        });
        PostService.GetFollowers().then(function (response) {
            $scope.followers = response.data.followers;
        });
    })
})
.controller("SearchController", function ($scope,userService, PostService, $routeParams, $window) {
    $scope.message = "SearchController";
    $scope.selected;
    
   
    $scope.funkcija = function (currentUser) {
  
        if ($scope.selected == currentUser) {
            $window.location.assign("/home/index");
        }
        else {
            $window.location.assign('/search/OtherUser?otherUserName=' + $scope.selected);
        }
    }

    $scope.$watch('$viewContentLoaded', function () {
        PostService.GetAllUsers().then(function (response) {
            $scope.users = response.data.users;

        });
    });
})
.controller("FileController", function ($scope, PostService)
{
    // Variables
    $scope.Message = "";
    $scope.FileInvalidMessage = "";
    $scope.SelectedFileForUpload = null;
    $scope.FileDescription = "";
    $scope.IsFormSubmitted = false;
    $scope.IsFileValid = false;
    $scope.IsFormValid = false;


    //Form Validation
    $scope.$watch("f1.$valid", function (isValid)
    {
        $scope.IsFormValid = isValid;

    });

    // File Validation
    $scope.CheckFileValid = function (file)
    {
        var isValid = false;
        if ($scope.SelectedFileForUpload != null) {
            if ((file.type == 'image/png' || file.type == 'image/jpeg' || file.type == "image/gif") && file.size <= (2 * 1024 * 1024)) {
                $scope.FileInvalidMessage = "";
                isValid = true;
            }
            else {
                $scope.FileInvalidMessage = "Format mora biti : pgn/jpeg/gif, velicina manja od 2MB";
            }

        }
        else {
            $scope.FileInvalidMessage = "Niste odabrali sliku!";
        }
        $scope.IsFileValid = isValid;
    }

    //File Select Event 
    $scope.selectFileForUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }
    //Save File
    $scope.SaveFiles = function () {
        $scope.IsFormSubmitted = true;
        $scope.Message = "";
        $scope.CheckFileValid($scope.SelectedFileForUpload);
        if ($scope.IsFormValid && $scope.IsFileValid) {
            PostService.SaveFiles($scope.SelectedFileForUpload, $scope.FileDescription).then(function (d) {
                alert(d.Message);
                ClearForm();
            }, function (e) {
                alert(e);
            });
        }
        else {
            $scope.Message = "All fields required";
        }
    }
    //Clear Form
    function ClearForm() {
        $scope.FileDescription = "";
        angular.forEach(angular.element("input[type='file']"), function (inputElem) {
            angular.element(inputElem).val(null);
        });
        $scope.f1.$setPristine();
        $scope.IsFormSubmitted = false;
    }

})
.controller("ImageController", function ($scope, PostService) {
    
    $scope.userName;
    $scope.$watch('$viewContentLoaded', function () {
        PostService.GetUserImage($scope.userName).then(function (response) {
            $scope.image = response.data.image;
        });
    })
})

.controller("OtherUserController", function ($scope,userService, PostService, $routeParams, $window, $location) {
    $scope.message = "OtherUserController";
    $scope.user;
    $scope.otherUser;
   
    
    $scope.follow = function (otherUserId) {
        
        PostService.Follow(otherUserId);

    }
   
    $scope.bljakPost = function (postId) {
        //alert("BLJAK " + postId + " " + userName);
        PostService.BljakPosts(postId);
        setTimeout(function () {
            $window.location.assign("/Search/OtherUser?otherUserName=" + $scope.otherUser);
        }, 500);
    }
    $scope.likePost = function (postId) {
        //alert("LIKE " + postId + " " + userName);
        PostService.LikePosts(postId);
        setTimeout(function () {
            $window.location.assign("/Search/OtherUser?otherUserName=" + $scope.otherUser);
        }, 500);

    }
    
  
    // GETTING SPECIFIC USER
    $scope.$watch('$viewContentLoaded', function () {

       
 
        var url = $location.absUrl().split('?')[1];
        var userName = url.split("=")[1];
        userName = userName.split("#")[0];
        $scope.otherUser = userName;
       
        PostService.GetPostsByUser(userName).then(function (response) {
            $scope.allPosts3 = response.data.listOfAllPosts;
            
        });
        PostService.GetOtherFollowers(userName).then(function (response) {
            $scope.followers = response.data.followers;
        });
        PostService.GetUserImage($scope.otherUser).then(function (response) {
            $scope.image = response.data.image;
        });
        PostService.GetUser($scope.otherUser).then(function (response) {
            $scope.user = response.data.user;
        });
    })
    
})
.factory("PostService", ["$http","$q", function ($http, $q)
    {
    var factory = {};
    // FUNKCIJA ZA ADMINA
    factory.GetAllPosts = function () {
        return $http.get("/Post/GetAllPosts");
    }
 
     factory.GetUserImage = function (userName) {
         return $http.get("/Home/GetUserImage", {
             params: { userName: userName }
         });
     }
    factory.SaveFiles = function (file, description) {
        var formData = new FormData();
        formData.append("file", file);
        formData.append("description", description);

        var defer = $q.defer();
        
        $http.post("/Home/SaveFiles", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            }).then(function (d) {
                defer.resolve(d);

            });
        return defer.promise;
    }

    factory.BljakPosts = function (PostId) {
        
        $http.post("/Post/BljakPost", {PostId:PostId}).then(function (response) {
            console.log("Post successfully bljaked/unbljaked  " + response.status);
        });
    }
    factory.Follow = function (OtherUserName) {

        $http.post("/Home/Follow", {OtherUserName: OtherUserName }).then(function (response) {
            console.log("Follow unfollow  " + response.status);
        });
    }
    factory.LikePosts = function (PostId) {
        
        $http.post("/Post/LikePost", {PostId: PostId}).then(function (response) {
            console.log("Post successfully liked/unliked  " + response.status);
        });
    }
    factory.deleteUser = function () {

        $http.post("/Home/UserDeleteConfirmed").then(function (response) {
            console.log("User successfully " + response.status);
        });
    }
    
   
    factory.GetPostsByUser = function (userName) {
        return $http.get("/Post/GetPostsByUser", {params : {userName: userName}});
    }
    factory.GetFollowers = function () {
        return $http.get("/Home/GetFollowers");
    }
    factory.GetOtherFollowers = function (userName) {
        return $http.get("/Home/GetOtherFollowers", {
            params: { userName: userName }
        });
    }
   
    factory.AddPost = function (post) {
        $http.post("/Post/AddPost", post).then(function (response) {
            console.log("Post successfully added  " + response.status);
        });
    }
    factory.EditPost = function (PostId, PostDescription) {
        $http.post("/Post/UpdatePost", { PostId: PostId, PostDescription: PostDescription }).then(function (response) {
            console.log("Post edited   " + response.status);
        });
    }
    factory.UpdatePost = function (post) {
        $http.post("/Post/UpdatePost", post).then(function (response) {
            console.log("Post successfully updated " + response.status);
        });
    }
    factory.DeletePost = function (PostId) {
        $http.post("/Post/DeletePost", {PostId: PostId}).then(function (response) {
            console.log("Post successfully deleted " + response.status);
        });
    }
    factory.EditUser = function (Email,CustomUsername, UserDescription) {
        $http.post("/Profile/EditUser", { Email: Email,CustomUsername:CustomUsername, UserDescription: UserDescription}).then(function (response) {
            console.log("User successfully edited " + response.status);
        });
    }
    factory.GetUser = function (userName) {
        return $http.get("/Home/GetUser", { params: {userName:userName}});
        
    }
    factory.GetOtherUser = function () {
        return $http.get("/Search/GetOtherUser");

    }
    factory.GetAllUsers = function () {
        return $http.get("/Home/GetAllUsers");

    }
    return factory;

    }
]);




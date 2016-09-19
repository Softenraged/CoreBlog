﻿(function () {
    'use strict';

    angular
        .module('postCreateApp')
        .controller('postCreateController', ['$routeParams',
                                             'postCreateFactory',
                                             '$window', postCreateController]);

    function postCreateController($routeParams, postCreateFactory, $window) {

        var vm = this;

        vm.error = "";
      
        vm.post = {};
  
        vm.isReady = false;
        vm.isFull = false;

        //if there is no route params, set status to false
        vm.edit = ( $routeParams.id === undefined ) ? false : true;

        vm.newPost = function () {
            postCreateFactory
                .getPostForm().success(function (response) {
                    angular.copy(response, vm.post);
                    vm.post.image = {};
                }).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                });
        };

        vm.getPost = function () {
            postCreateFactory
                .getPost($routeParams.id).success(function (response) {
                    angular.copy(response, vm.post);
                }).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                });
        };
        
        vm.updatePost = function () {
            postCreateFactory
                .updatePost(vm.post).success(function(){
                    vm.isReady = true;
                }).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                }).finally(function () {
                    $window.location.href = '/Post/Open/' + $routeParams.id;
                });
        };

        vm.addTag = function () {
            if (vm.post.tags.length < 5) {
                vm.post.tags.push({
                    name: ''
                });
            }

            if (vm.post.tags.length === 5) {
                vm.isFull = true;
            }
        };

        vm.removeTag = function (index) {
            vm.isFull = false;
            vm.post.tags.splice(index, 1);
        };

        vm.back = function (update) {

            vm.edit ? $window.location.href = '/Post/Open/' + $routeParams.id
                      :
                      $window.location.href = '/Blog/1';
        };

        vm.approve = function () {  
            postCreateFactory
                .sendNewPost(vm.post).success(function () {
                    vm.isReady = true;
                }).error(function () {
                    vm.error = "Oops. Something went wrong. Try again later!";
                }).finally(function () {
                    $window.location.href = '/Blog/1';
                });
        };
    };
})();

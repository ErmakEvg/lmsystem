﻿angular
    .module('dpApp.ctrl.project', [])
    .controller('projectCtrl', [
        '$scope',
        '$routeParams',
        '$location',
        'projectService',
        'projectId',
        '$modalInstance',
        function ($scope, $routeParams, $location, projectService, projectId, $modalInstance) {

            $scope.isNew = angular.isUndefined(projectId);

            $scope.project = {
                Theme: '',
                Id: null,
                SelectedGroupsIds: []
            };
            $scope.groups = [];

            if (!$scope.isNew) {
                projectService
                    .getProject(projectId)
                    .success(function (data, status, headers, config) {
                        $scope.project = data;
                    });
            }

            projectService
                .getGroupCorrelation()
                .success(function (data, status, headers, config) {
                    $scope.groups = data;
                });

            $scope.saveProject = function () {
                $scope.submitted = true;
                //              TODO  if ($scope.projectForm.Theme.$error.required) return; 

                var saveFunc = $scope.isNew ? projectService.createProject : projectService.updateProject;

                saveFunc($scope.project)
                    .success(function (data, status, headers, config) {
                        $modalInstance.close();
                        alertify.success('Проект успешно сохранен.');
                    })
                    .error(function (data) {
                        var message = '';

                        for (var prop in data.ModelState) {
                            if (data.ModelState.hasOwnProperty(prop)) {
                                $.each(data.ModelState[prop], function () {
                                    message += this + '<br/>';
                                });
                            }
                        }
                        $modalInstance.close();
                        alertify.error(message);
                    });
            };


            $scope.returnToList = function () {
                $modalInstance.close();
            };

        }]);
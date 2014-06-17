﻿'use strict';
knowledgeTestingApp.controller('questionDetailsCtrl', function ($scope, $http, id, $modalInstance) {

    $scope.QuestionType = 0;
    $scope.types = [{ Id: 1, Name: 'С несколькими вариантами' }, { Id: 0, Name: 'С одним вариантом' }, { Id: 2, Name: 'Ввод с клавиатуры' }, { Id: 3, Name: 'Последовательность элементов' }];

    $scope.deleteAnswer = function (index) {
        $scope.question.Answers.splice(index, 1);
    };

    $http({ method: 'GET', url: kt.actions.questions.getQuestion, dataType: 'json', params: { id: id } })
    .success(function (data) {
        $scope.question = data;
    })
    .error(function (data, status, headers, config) {
        alertify.error('Во время получения данных произошла ошибка');
    });

    $scope.saveQuestion = function() {
        $scope.question.TestId = getHashValue('testId');
        
        $.ajax({
            url: kt.actions.questions.saveQuestion,
            type: "POST",
            data: JSON.stringify($scope.question),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (data.ErrorMessage) {
                    alertify.error(data.ErrorMessage);
                } else {
                    $scope.loadQuestions();
                    $scope.closeDialog();
                    alertify.success('Вопрос успешно сохранен');
                }
            }
        });
    };

    $scope.closeDialog = function () {
        $modalInstance.close();
    };
    
    function aveTest(test) {
        
    }
});
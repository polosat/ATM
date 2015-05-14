var app = angular.module('atmApp', ['ui.router']);

app.config(function ($stateProvider) {
  $stateProvider.state('enterCard', {
    templateUrl: 'Display/EnterCard',
    controller: 'enterCardCtrl'
  });

  $stateProvider.state('enterPin', {
    templateUrl: 'Display/EnterPin',
    controller: 'enterPinCtrl'
  });

  $stateProvider.state('selectOperation', {
    templateUrl: 'Display/SelectOperation',
    controller: 'selectOperationCtrl'
  });

  $stateProvider.state('showBalance', {
    templateUrl: 'Display/ShowBalance',
    controller: 'showBalanceCtrl'
  });

  $stateProvider.state('withdrawal', {
    templateUrl: 'Display/Withdrawal',
    controller: 'withdrawalCtrl'
  });

  $stateProvider.state('showWithdrawalResult', {
    templateUrl: 'Display/ShowWithdrawalResult',
    controller: 'showWithdrawalResultCtrl'
  });

  $stateProvider.state('error', {
    template:
      '<div class="title-box">{{title}}</div>' +
      '<div class="content-error">' +
        '<div class="message-error">{{messageText}}</div>' +
        '<div class="btn btn-primary btn-control" ng-click="goBack()">{{buttonTitle.back}}</div>' +
      '</div>',
    controller: 'errorCtrl'
  });
});

app.controller('appCtrl', function ($scope, $state, strings) {
  $scope.pageTitle = '';
  $scope.title = '';
  $state.go('enterCard');
});

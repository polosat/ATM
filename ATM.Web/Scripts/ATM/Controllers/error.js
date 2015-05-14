app.controller('errorCtrl', function ($scope, $state, strings, errorModel) {
  $scope.title = strings.titleError;
  $scope.$parent.pageTitle = strings.pageTitleError;

  $scope.buttonTitle = {
    back: strings.buttonBack
  };

  $scope.messageText = errorModel.messageText;

  $scope.goBack = function () {
    $state.go(errorModel.returnState);
  };
});
app.controller('showWithdrawalResultCtrl', function ($scope, $state, strings, cardOperations, withdrawalModel) {
  var thisState = 'showWithdrawalResult';

  $scope.title = strings.titleWithdrawalResult;
  $scope.$parent.pageTitle = strings.pageTitleWithdrawalResult;

  $scope.formDisabled = false;

  $scope.label = {
    cardNumber: strings.labelCardNumber,
    operationDate: strings.labelOperationTime,
    balance: strings.labelBalance,
    operationAmount: strings.labelOperationAmount
  };

  $scope.buttonTitle = {
    back: strings.buttonBack,
    exit: strings.buttonExit
  };

  $scope.model = withdrawalModel;

  $scope.goBack = function () {
    $state.go('withdrawal');
  };

  $scope.logOut = function () {
    $scope.formDisabled = true;
    cardOperations.logOut(thisState);
  };
});
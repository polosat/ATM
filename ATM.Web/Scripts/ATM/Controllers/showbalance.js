app.controller('showBalanceCtrl', function ($scope, $state, strings, cardOperations, balanceModel) {
  var thisState = 'showBalance';

  $scope.title = strings.titleBalance;
  $scope.$parent.pageTitle = strings.pageTitleBalance;

  $scope.formDisabled = false;

  $scope.label = {
    cardNumber: strings.labelCardNumber,
    date: strings.labelDate,
    balance: strings.labelBalance
  };

  $scope.buttonTitle = {
    back: strings.buttonBack,
    exit: strings.buttonExit
  };

  $scope.model = balanceModel;

  $scope.goBack = function () {
    $state.go('selectOperation');
  };

  $scope.logOut = function () {
    $scope.formDisabled = true;
    cardOperations.logOut(thisState);
  };
});
app.controller('selectOperationCtrl', function ($scope, $state, strings, cardOperations, balanceModel) {
  var thisState = 'selectOperation';

  $scope.title = strings.titleOperations;
  $scope.$parent.pageTitle = strings.pageTitleOperations;

  $scope.formDisabled = false;

  $scope.buttonTitle = {
    balance: strings.buttonBalance,
    withdraw: strings.buttonWithdraw,
    exit: strings.buttonExit
  };

  $scope.showBalance = function () {
    $scope.formDisabled = true;
    var request = {};
    cardOperations.execute('getbalance', request, thisState,
      function (data) {
        balanceModel.date = new Date(data.Date);
        balanceModel.cardNumber = data.CardNumber;
        balanceModel.balance = data.Balance;
        balanceModel.currency = data.CurrencyCode;
        $state.go('showBalance');
      }
    );
  };

  $scope.showWithdrawal = function () {
    $state.go('withdrawal');
  };

  $scope.logOut = function () {
    $scope.formDisabled = true;
    cardOperations.logOut(thisState);
  };
});
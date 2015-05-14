app.controller('withdrawalCtrl', function ($scope, $state, strings, cardOperations, withdrawalModel) {
  var thisState = 'withdrawal';

  $scope.title = strings.titleWithdrawal;
  $scope.$parent.pageTitle = strings.pageTitleWithdrawal;

  $scope.model = {
    amount: '',
    amountMaxLength : 5
  };

  $scope.formDisabled = false;

  $scope.serviceButtons = [
    {
      title: strings.buttonOK,
      className: 'btn-success',
      onClick: function () {
        $scope.withdraw($scope.model.amount);
      }
    },
    {
      title: strings.buttonClear,
      className: 'btn-warning',
      onClick: function () {
        $scope.model.amount = '';
      }
    },
    {
      title: strings.buttonExit,
      className: 'btn-danger',
      onClick: function () {
        $scope.formDisabled = true;
        cardOperations.logOut(thisState);
      }
    }
  ];

  $scope.$watch('model.amount', function () {
    $scope.serviceButtons[0].disabled = ($scope.model.amount.length == 0 || $scope.model.amount == 0);
    $scope.serviceButtons[1].disabled = ($scope.model.amount.length == 0);
  });

  $scope.withdraw = function (amount) {
    $scope.formDisabled = true;
    var request = { Amount: amount };
    var customErrors = {
      '422': {
        text: strings.errorInsufficientFunds,
        returnTo: thisState
      }
    };

    cardOperations.execute('withdraw', request, thisState,
      function (data) {
        withdrawalModel.date = new Date(data.Date);
        withdrawalModel.cardNumber = data.CardNumber;
        withdrawalModel.balance = data.Balance;
        withdrawalModel.amount = data.Amount;
        withdrawalModel.currency = data.CurrencyCode;
        $state.go('showWithdrawalResult');
      },
      customErrors
    );
  };
});
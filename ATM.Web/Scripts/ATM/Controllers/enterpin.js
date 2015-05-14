app.controller('enterPinCtrl', function ($scope, $state, strings, cardOperations) {
  var thisState = 'enterPin';

  $scope.title = strings.titleEnterPin;
  $scope.$parent.pageTitle = strings.pageTitleEnterPin;

  $scope.model = {
    pinCode: '',
    pinMaxLength: 4
  };

  $scope.formDisabled = false;

  $scope.serviceButtons = [
    {
      title: strings.buttonOK,
      className: 'btn-success',
      onClick: function () {
        $scope.authenticate($scope.model.pinCode);
      }
    },
    {
      title: strings.buttonClear,
      className: 'btn-warning',
      onClick: function () {
        $scope.model.pinCode = '';
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

  $scope.$watch('model.pinCode', function () {
    $scope.serviceButtons[0].disabled = ($scope.model.pinCode.length < $scope.model.pinMaxLength);
    $scope.serviceButtons[1].disabled = ($scope.model.pinCode.length == 0);
  });

  $scope.authenticate = function (pinCode) {
    $scope.formDisabled = true;
    var request = { PinCode: pinCode };
    var customErrors = {
      '422': {
        text: strings.errorInvalidPin,
        returnTo: thisState
      }
    };

    cardOperations.execute('authenticate', request, thisState,
      function (data) {
        $state.go('selectOperation');
      },
      customErrors
    );
  };
});
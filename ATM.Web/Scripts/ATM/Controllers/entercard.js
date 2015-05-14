app.controller('enterCardCtrl', function ($scope, $state, strings, cardOperations) {
  var thisState = 'enterCard';

  $scope.title = strings.titleEnterCard;
  $scope.$parent.pageTitle = strings.pageTitleEnterCard;

  $scope.model = {
    cardNumber: '',
    cardNumberMaxLength: 16
  };

  $scope.formDisabled = false;

  $scope.serviceButtons = [{
    title: strings.buttonOK,
    disabled: true,
    className: 'btn-success',
    onClick: function () {
      $scope.identifyCard($scope.model.cardNumber);
    }
  }, {
    title: strings.buttonClear,
    disabled: true,
    className: 'btn-warning',
    onClick: function () {
      $scope.model.cardNumber = '';
    }
  }];

  $scope.$watch('model.cardNumber', function () {
    $scope.serviceButtons[0].disabled = ($scope.model.cardNumber.length < $scope.model.cardNumberMaxLength);
    $scope.serviceButtons[1].disabled = ($scope.model.cardNumber.length == 0);
  });

  $scope.identifyCard = function (cardNumber) {
    var request = { CardNumber: cardNumber };
    $scope.formDisabled = true;
    cardOperations.execute('identify', request, thisState,
      function () {
        $state.go('enterPin');
      }
    );
  };
});
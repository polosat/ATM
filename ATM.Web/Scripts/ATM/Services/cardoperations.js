app.constant('defaultErrorRules', {
  '401': {
    text: '',
    returnTo: 'enterCard'
  },
  'default': {
    text: '',
    returnTo: ''
  }
});

app.service('cardOperations', function ($http, $state, strings, errorModel, defaultErrorRules) {
  defaultErrorRules['401'].text = strings.accessDenied;
  defaultErrorRules['default'].text = strings.errorUnknown;

  this.execute = function (actionName, requestData, fromState, onSucceeded, errorRules) {
    var url = 'api/card/' + actionName;

    $http.post(url, requestData)
      .success(function (data) {
        onSucceeded(data);
      })
      .error(function (data, responseCode) {
        if (!angular.isDefined(errorRules)) {
          errorRules = {};
        }

        for (var errorCode in defaultErrorRules) {
          if (!errorRules.hasOwnProperty(errorCode)) {
            errorRules[errorCode] = defaultErrorRules[errorCode];
          }
          if (errorCode == 'default') {
            errorRules[errorCode].returnTo = fromState;
          }
        }

        var errorRule = errorRules.hasOwnProperty(responseCode) ? errorRules[responseCode] : errorRules['default'];

        errorModel.messageText = errorRule.text;
        errorModel.returnState = errorRule.returnTo;
        $state.go('error');
      });
  };

  this.logOut = function (fromState) {
    $http.post('api/card/logout')
      .success(function () {
        $state.go('enterCard');
      })
      .error(function () {
        errorModel.returnState = fromState;
        errorModel.messageText = strings.errorUnknown;
        $state.go('error');
      });
  };
});
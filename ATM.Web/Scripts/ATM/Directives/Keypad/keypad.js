app.directive("keypad", function() {
  return {
    restrict: 'E',
    replace: true,
    templateUrl: '/Scripts/ATM/Directives/Keypad/keypad.html',
    scope: {
      inputValue: '=',
      maxLength: '=',
      customKeys: '=',
      disabled: '='
    },
    link: function (scope) {
      scope.onNumericKeyPress = function (row, col) {
        if (scope.inputValue.toString().length < scope.maxLength) {
          scope.inputValue += scope.getDigitAtPos(row, col).toString();
        }        
      };

      scope.getDigitAtPos = function (row, col) {
        var digit = row * 3 + col;
        return (digit > 9) ? 0 : digit;
      };
    }
  };
});
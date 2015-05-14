app.filter('cardNumber', function () {
  return function (input) {
    var result = '';
    for (var i = 0, count = input.length; i < count; i += 4) {
      if (i > 0) {
        result += '-';
      }
      result += input.substr(i, 4);
    }
    return result;
  }
});

app.filter('hidden', function () {
  return function (input) {
    return result = new Array(input.length + 1).join('*');
  }
});


app.filter('currencyName', function (strings) {
  return function (input) {
    return strings.currencyCodes.hasOwnProperty(input) ? strings.currencyCodes[input] : ('/' + input + '/');
  }
});

app.factory('errorModel', function () {
  return {
    model: {
      returnState: '',
      messageText: ''
    }
  }
});

app.factory('balanceModel', function () {
  return {
    model: {
      cardNumber: '',
      date: '',
      balance: 0,
      currency: ''
    }
  }
});

app.factory('withdrawalModel', function () {
  return {
    model: {
      cardNumber: '',
      date: '',
      balance: 0,
      amount: 0,
      currency: ''
    }
  }
});

using System;

namespace ATM.Logic.Cards.OperationResults
{
  public class WithdrawalResult : CardOperationResult
  {
    public string CardNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
    public decimal Amount { get; set; }
    public short CurrencyCode { get; set; }
  }
}

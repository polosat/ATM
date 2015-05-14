using System;

namespace ATM.Web.Models.Card
{
  public class GetBalanceResponse
  {
    public string CardNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
    public short CurrencyCode { get; set; }
  }
}
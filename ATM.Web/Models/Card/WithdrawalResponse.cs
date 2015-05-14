using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ATM.Web.Models.Card
{
  public class WithdrawalResponse
  {
    public string CardNumber { get; set; }
    public DateTime Date { get; set; }
    public decimal Balance { get; set; }
    public decimal Amount { get; set; }
    public short CurrencyCode { get; set; }
  }
}
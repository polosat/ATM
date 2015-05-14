using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Web.Models.Card
{
  public class AuthenticateCardRequest
  {
    [MaxLength(4)]
    public string PinCode { get; set; }
  }
}
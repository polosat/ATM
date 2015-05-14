using System;
using System.ComponentModel.DataAnnotations;

namespace ATM.Web.Models.Card
{
  public class IdentifyCardRequest
  {
    [MaxLength(16)]
    public string CardNumber { get; set; }
  }
}
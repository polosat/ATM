using System;

namespace ATM.Logic.Cards.Enums
{
  public enum OperationStatus
  {
    Succeeded = 0,
    UnknownCard = 1,
    BlockedCard = 2,
    InsufficientFunds = 3,
    InvalidAmount = 4,
    InvalidPinCode = 5
  }
}
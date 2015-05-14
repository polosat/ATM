using System;

namespace ATM.Logic.Cards.Enums
{
  public enum CardBlockReason
  {
    NotActivatedYet = 0,
    TooManyFailedLogins = 1,
    BlockedByOperator = 2
  }
}
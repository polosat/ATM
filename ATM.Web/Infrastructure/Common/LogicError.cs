using System;

namespace ATM.Web.Infrastructure.Common
{
  public class LogicError
  {
    public string Type { get; protected set; }
    public string Message { get; protected set; }

    public LogicError(string message)
    {
      Type = "LogicError";
      Message = message;
    }
  }
}
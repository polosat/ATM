using System;
using System.Net;
using System.Net.Http;

namespace ATM.Web.Infrastructure.Common
{
  public static class Extensions
  {
    private const int UnprocessableEntity = 422;

    public static HttpResponseMessage CreateLogicErrorResponse(this HttpRequestMessage request, string message)
    {
      return request.CreateResponse<LogicError>((HttpStatusCode)UnprocessableEntity, new LogicError(message));
    }
  }
}
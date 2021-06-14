using Microsoft.Azure.Functions.Worker; // [Function], [HttpTrigger]
using Microsoft.Azure.Functions.Worker.Http; // HttpRequestData, HttpResponseData
using Microsoft.Extensions.Logging; // ILogger
using System.Collections.Specialized; // NameValueCollection
using System.Net; // HttpStatusCode
using System.Numerics;
using System.Web; // HttpUtility
using Packt.Shared;

namespace Northwind.AzureFuncs
{
  public static class NumbersToWordsFunction
  {
    [Function(nameof(NumbersToWordsFunction))]
    public static HttpResponseData Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, 
        "get", "post")] HttpRequestData req,
      FunctionContext executionContext)
    {
      ILogger logger = executionContext.GetLogger(
        nameof(NumbersToWordsFunction));
      logger.LogInformation($"C# HTTP trigger function URL: {req.Url}");

      // convert the query string into a dictionary
      NameValueCollection queryDictionary = HttpUtility
        .ParseQueryString(req.Url.Query);

      HttpResponseData response = req.CreateResponse();

      string amount = queryDictionary["amount"];
      string words;

      if (BigInteger.TryParse(amount, out BigInteger number))
      {
        words = number.ToWords();
        response.StatusCode = HttpStatusCode.OK;
      }
      else
      {
        words = $"Failed to parse: {amount}";
        response.StatusCode = HttpStatusCode.BadRequest;
      }

      response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

      response.WriteString(words);

      return response;
    }
  }
}
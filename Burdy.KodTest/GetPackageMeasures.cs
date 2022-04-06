using System.Collections.Generic;
using System.Net;
using System.Web;
using Burdy.KodTest.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace Burdy.KodTest
{
    public class GetPackageMeasures
    {
        private readonly ILogger _logger;
        private readonly PackageService _packageService;

        public GetPackageMeasures(ILoggerFactory loggerFactory, PackageService packageService)
        {
            _logger = loggerFactory.CreateLogger<GetPackageMeasures>();
            _packageService = packageService;
        }

        [Function("GetPackageMeasures")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Function, nameof(HttpMethod.Get), Route = "{packageNumber:long}")] HttpRequestData req, long packageNumber,
        FunctionContext executionContext)
        {
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            var message = "";
            _packageService.InitiateAllPackages();
            var (a,b) = _packageService.CheckPackageNumber(packageNumber);

            if (!b)
            {
                message = String.Format(a);
                response.WriteString(message);
                return response;
            }
            
            var package = _packageService.GetPackageByPackageNumber(packageNumber);
            if (package is null)
            {
                message = "Package number is missing";
                response.WriteString(message);
                return response;
            }
            message = String.Format($"Height: {package.Height},Lenght: {package.Lenght}, Width: {package.Width} ,Weight: {package.Weight}");
            response.WriteString(message);

            return response;
        }
    }
}

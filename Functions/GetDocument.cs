using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AegisVault.Models.Inbound;
using AegisVault.Create.Helpers;
using AegisVault.Retrieve.Models;
using System.Net.Http;

namespace AegisVault.Retrieve.Functions
{
    public class GetDocument
    {
        private readonly RetrieveHelper _retrieveHelper;
        private readonly AegisVaultContext _context;
        private readonly HttpContext _httpContext;

        public GetDocument(AegisVaultContext context, HttpContextAccessor contextAccessor)
        {
            _httpContext = contextAccessor.HttpContext;
            _retrieveHelper = new RetrieveHelper(context);
            _context = context;
        }

        [FunctionName(nameof(GetDocumentFunction))]
        public async Task<IActionResult> GetDocumentFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v2/GetDocument")] HttpRequest req,
            ILogger log)
        {
            string bodyText = new StreamReader(req.Body).ReadToEnd();

            RetrieveInbound body = JsonConvert.DeserializeObject<RetrieveInbound>(bodyText);
            Packa ps = await _retrieveHelper.GetDocumentPassword(body);
            req.HttpContext.Response.Headers.Add("FileName", ps.DocumentName);
            _httpContext.Response.Headers.Add("X-FileNameHeader", ps.DocumentName);
            _httpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-FileNameHeader, FileName");
            return new FileStreamResult(ps.Stream, ps.ContentType) { FileDownloadName = ps.DocumentName };
        }
    }
}

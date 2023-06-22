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

namespace AegisVault.Retrieve.Functions
{
    public class GetDocument
    {
        private readonly RetrieveHelper _retrieveHelper;
        private readonly AegisVaultContext _context;

        public GetDocument(AegisVaultContext context)
        {
            _retrieveHelper = new RetrieveHelper(context);
            _context = context;
        }

        [FunctionName(nameof(GetDocumentFunction))]
        public async Task<IActionResult> GetDocumentFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v2/GetDocument")] RetrieveInbound body,
            ILogger log)
        {
            Packa ps = await _retrieveHelper.GetDocumentPassword(body);
            return new FileStreamResult(ps.Stream, ps.ContentType) { FileDownloadName = ps.DocumentName };
        }
    }
}

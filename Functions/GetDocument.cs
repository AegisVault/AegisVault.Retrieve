using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using AegisVault.Create.Helpers;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;

namespace AegisVault.Function
{
    public class GetDocument
    {

        private readonly RetrieveLinkHelper _retrieveLinkHelper;
        private readonly AegisVaultContext _context;

        public GetDocument(AegisVaultContext context)
        {
            _retrieveLinkHelper = new RetrieveLinkHelper(context);
            _context = context;
        }

        [FunctionName(nameof(GetLink))]
        public async Task<IActionResult> GetLink(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/GetLink")] RetrieveLinkInbound body,
            ILogger log)
        {
            string toReturn = JsonConvert.SerializeObject(await _retrieveLinkHelper.GetLinkPassword(body));

            return new OkObjectResult(toReturn);
        }
    }
}

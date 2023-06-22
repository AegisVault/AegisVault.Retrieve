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
    public class GetLink
    {

        private readonly RetrieveHelper _retrieveLinkHelper;
        private readonly AegisVaultContext _context;

        public GetLink(AegisVaultContext context)
        {
            _retrieveLinkHelper = new RetrieveHelper(context);
            _context = context;
        }

        [FunctionName(nameof(GetLinkFunction))]
        public async Task<IActionResult> GetLinkFunction(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/GetLink")] RetrieveInbound body,
            ILogger log)
        {
            string toReturn = JsonConvert.SerializeObject(await _retrieveLinkHelper.GetLinkPassword(body));

            return new OkObjectResult(toReturn);
        }
    }
}

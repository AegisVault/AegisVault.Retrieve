using AegisVault.Models.Database;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Helpers
{
    public class RetrieveLinkHelper
    {
        private readonly AegisVaultContext _context;
        public RetrieveLinkHelper(AegisVaultContext context)
        {
            _context = context;
        }

        public async Task<RetrieveLinkOutbound> GetLinkPassword(RetrieveLinkInbound inboundData)
        {
            await _context.Database.EnsureCreatedAsync();

            var links = _context.Links.ToList();
            var databaseLink = await _context.Links.Where(item =>
                item.DbId == inboundData.Id &&
                item.Password == inboundData.Password).FirstOrDefaultAsync();

            RetrieveLinkOutbound toReturn = new RetrieveLinkOutbound()
            {
                Link = databaseLink.Url
            };

            return toReturn;
        }
    }
}

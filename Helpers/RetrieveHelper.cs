using AegisVault.Models.Database;
using AegisVault.Models.Inbound;
using AegisVault.Models.Outbound;
using AegisVault.Retrieve.Helpers;
using AegisVault.Retrieve.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Create.Helpers
{
    public class RetrieveHelper
    {
        private readonly AegisVaultContext _context;
        public RetrieveHelper(AegisVaultContext context)
        {
            _context = context;
        }

        public async Task<RetrieveLinkOutbound> GetLinkPassword(RetrieveInbound inboundData)
        {
            await _context.Database.EnsureCreatedAsync();

            var links = _context.Links.ToList();
            var databaseLink = await _context.Links.Where(item =>
                item.DisplayId == inboundData.DisplayId &&
                item.Password == inboundData.Password).FirstOrDefaultAsync();

            RetrieveLinkOutbound toReturn = new RetrieveLinkOutbound()
            {
                Link = databaseLink.Url
            };

            return toReturn;
        }

        public async Task<Packa> GetDocumentPassword(RetrieveInbound inboundData)
        {
            await _context.Database.EnsureCreatedAsync();
            BlobStorageHelper bs = new BlobStorageHelper();
            

            var documents = _context.Documents.ToList();
            var databaseDocument = await _context.Documents.Where(item =>
                item.DisplayId == inboundData.DisplayId &&
                item.Password == inboundData.Password).FirstOrDefaultAsync();

            Packa ps = new Packa();
            ps.DocumentName = databaseDocument.Location.Split("/")[1];
            ps.Stream = await bs.GetFile(databaseDocument.Location);
            ps.ContentType = databaseDocument.ContentType;
            return ps;
        }
    }
}

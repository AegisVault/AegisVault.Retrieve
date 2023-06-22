using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AegisVault.Retrieve.Models
{
    public class Packa
    {
        public string DocumentName { get; set; }
        public string ContentType { get; set; }
        public MemoryStream Stream { get; set; }
    }
}

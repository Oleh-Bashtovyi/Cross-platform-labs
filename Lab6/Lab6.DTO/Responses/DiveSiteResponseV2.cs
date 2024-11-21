using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.DTO
{
    public class DiveSiteResponseV2 : DiveSiteResponse
    {
        public DateTime? WreckDate { get; set; }
        public string? DiveSiteTypeDetails { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iucs.pharmacy.application.Dto
{
    public class TenantResponseDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public string Data { get; set; } = default!;
    }

    public class TenantResolveDto
    {
        public bool Success { get; set; }
        public string TenantCode { get; set; } = default!;
        public string Connection { get; set; } = default!;
        public string DbProvider { get; set; } = default!;
        public string Environment { get; set; } = default!;
    }
}

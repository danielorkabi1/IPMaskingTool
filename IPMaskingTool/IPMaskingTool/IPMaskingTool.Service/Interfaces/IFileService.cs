using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingTool.Service.Interfaces
{
    public interface IFileService
    {
        Task<string?[]> ReadAndCreateText(IFormFile file);
    }
}

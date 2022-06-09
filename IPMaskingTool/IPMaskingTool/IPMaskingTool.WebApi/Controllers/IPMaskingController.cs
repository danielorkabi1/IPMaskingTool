
using IPMaskingTool.Service.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IPMaskingTool.WebApi.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class IPMaskingController : ControllerBase
    {
        IFileService _fileService;
        public IPMaskingController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPut]
        [Route("upload")]
        public async Task<IActionResult> MasklLog(IFormFile file)
        {
            try
            {
                var data = await this._fileService.ReadAndCreateText(file);
 
                return File(Encoding.UTF8.GetBytes(data[1]), "application/octet-stream", "mask");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

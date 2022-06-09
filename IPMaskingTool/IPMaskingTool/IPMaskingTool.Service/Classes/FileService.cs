using IPMaskingToll.Logic.Interfaces;
using IPMaskingTool.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingTool.Service.Classes
{
    public class FileService:IFileService
    {
        readonly IFileReaderLogic _fileReaderLogic;
        public FileService(IFileReaderLogic fileReaderLogic)
        {
            _fileReaderLogic = fileReaderLogic;
        }
        public async Task<string?[]> ReadAndCreateText(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
                return await this._fileReaderLogic.ReadAndCreateText(stream);
        }
    }
}

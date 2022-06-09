using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingToll.Logic.Interfaces
{
    public interface IFileReaderLogic
    {
        Task<string[]> ReadAndCreateText(Stream stream);
    }
}

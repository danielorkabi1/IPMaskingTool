using IPMaskingToll.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingToll.Logic.Classes
{
    public class FileReaderLogic : IFileReaderLogic
    {
        public async Task<string[]> ReadAndCreateText(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            StringBuilder loadText = new StringBuilder();
            StringBuilder maskText = new StringBuilder();
            using (StreamReader sr = new StreamReader(stream))
            {
                if (!stream.CanRead)
                    throw new Exception("No Readable File");

                var size = (sr.BaseStream.Length / 1024f) / 1024f;
                if (size > 5)
                    throw new OutOfMemoryException($"The File Is Over 5mb -> {size}mb");

                string? line = await sr.ReadLineAsync();
                IIPAddressLogic ipLogic = new IPAddressLogic();
                while (line != null)
                {
                    maskText.AppendLine(ipLogic.ChangeLine(line));
                    loadText.AppendLine(line);
                    line = await sr.ReadLineAsync();
                }
                return new string[] { loadText.ToString(), maskText.ToString() };
            }
        }
    }
}


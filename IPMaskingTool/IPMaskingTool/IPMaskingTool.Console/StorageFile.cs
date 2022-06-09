using IPMaskingToll.Logic.Classes;
using IPMaskingToll.Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IPMaskingTool.Console
{
    public class StorageFile
    {
        public async Task<bool> Save(string path, string saveData)
        {
            try
            {
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    using (var writer = new StreamWriter(fileStream, Encoding.Default))
                    {
                        await writer.WriteLineAsync(saveData);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<string[]> Load(string path)
        {
            if (!File.Exists(path) || !CheckPath(path))
                throw new Exception("no such log file");
            IFileReaderLogic fileRedaerLogic = new FileReaderLogic();
            using (Stream stream = File.OpenRead(path))
            {
                return await fileRedaerLogic.ReadAndCreateText(stream);
            }
        }
        private bool CheckPath(string path)
        {
            string[] checkPath = path.Split('.');
            return checkPath != null && checkPath.Length > 0 && checkPath[checkPath.Length - 1] == "log";
        }

    }
}

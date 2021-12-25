using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace FlowerStore.Areas.Admin.Utils
{
    public class FileUtils
    {
        public static async Task<bool> DelteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }
            return false;
        } 
    }
}

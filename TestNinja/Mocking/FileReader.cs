using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    /*
        解偶: 把對外依賴的部分獨立出一個class，方便測試時抽換 
    */
    public interface IFileReader
    {
        string Read(string path);
    }

    public class FileReader: IFileReader
    {
        public string Read(string path)
        {
             return File.ReadAllText(path);
        }
    }
}

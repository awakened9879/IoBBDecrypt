using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IovationBlackBoxDecrypt.DecryptClasses;

namespace IovationBlackBoxDecrypt
{
    class Program
    {
        static void Main(string[] args)
        {
            var myDict = StarBucksSite.Decrypt("YOUR BLACKBOX TEXT");
            var s = string.Join("\r\n", myDict.Select(x => $"[\"{x.Key}\"] = \"{x.Value}\"").ToArray());
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}

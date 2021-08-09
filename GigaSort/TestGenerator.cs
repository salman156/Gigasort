using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaSort
{
    static class TestGenerator
    {
        static Random random = new Random();
        static char GenerateChar()
        {
            const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" + "abcdefghijklmnopqrstuvwxyz";

            return alphabet[random.Next(alphabet.Length)];
        }
        static StringBuilder GenerateString(int len)
        {
            StringBuilder sb = new StringBuilder(len);

            for (int i = 0; i < len; i++) 
            {
                sb.Append(GenerateChar());
            }

            return sb;
        }
        static public void GenerateTest()
        {
            if (!Directory.Exists("tempTest"))
                Directory.CreateDirectory("tempTest");

            const int avgstringsize = 12;

            using (TextWriter filestream = new StreamWriter("tempTest\\file.txt"))
            {
                for(int i=0;i<100000000/2;i++)
                    filestream.WriteLine(GenerateString(avgstringsize + random.Next(-2, 3)));
            }

        }
    }
}

using System.Collections.Generic;
using System.IO;

namespace GigaSort
{
    class Program
    {
        //Можно запараллелить, но не просили, поэтому не делаю
        //UPD:
        //ДОЛГО!!!!!!!!!!!!
        static void MergeFiles(ref int fileindex)
        {
            int mergeindex = 1;

            while(mergeindex + 1 != fileindex)
            {
                using (StreamReader file1 = new StreamReader($"tempTest\\tmp\\tmp{mergeindex}.txt"), 
                    file2 = new StreamReader($"tempTest\\tmp\\tmp{mergeindex+1}.txt"))
                using(var newfile = new StreamWriter($"tempTest\\tmp\\tmp{fileindex}.txt"))
                {
                    string str1 = file1.ReadLine(), str2=file2.ReadLine();
                    while (!(file1.EndOfStream || file2.EndOfStream)) { 

                        if (string.Compare(str1, str2) < 0)
                        {
                            newfile.WriteLine(str1);
                            str1 = file1.ReadLine();
                        }
                        else
                        {
                            newfile.WriteLine(str2);
                            str2 = file2.ReadLine();
                        }
                    }

                    while (!file1.EndOfStream)
                        newfile.WriteLine(file1.ReadLine());

                    while (!file2.EndOfStream)
                        newfile.WriteLine(file2.ReadLine());
                }
                File.Delete($"tempTest\\tmp\\tmp{mergeindex}.txt");
                File.Delete($"tempTest\\tmp\\tmp{mergeindex+1}.txt");
                mergeindex += 2;
                fileindex++;
            }

            using(StreamReader sortedfile = new StreamReader($"tempTest\\tmp\\tmp{mergeindex}.txt"))
            using(StreamWriter oldfile = new StreamWriter("tempTest\\file.txt"))
            {
                while (!sortedfile.EndOfStream)
                    oldfile.WriteLine(sortedfile.ReadLine());
            }

            File.Delete($"tempTest\\tmp\\tmp{mergeindex}.txt");
            Directory.Delete("tempTest\\tmp");
        }
        static void Sort()
        {
            const int buffsize = 1024 * 1024 + 512;

            int fileindex = 1;

            using (StreamReader filestream = new StreamReader("tempTest\\file.txt"))
            {
                Directory.CreateDirectory("tempTest\\tmp");

                while (!filestream.EndOfStream)
                {
                    using (StreamWriter sw = new StreamWriter($"tempTest\\tmp\\tmp{fileindex}.txt"))
                    {
                        int readbytes = 0;
                        List<string> list = new List<string>();

                        while (!filestream.EndOfStream && readbytes < buffsize)
                        {
                            list.Add(filestream.ReadLine());
                            readbytes += list[list.Count - 1].Length;
                        }
                        list.Sort();

                        foreach(var str in list)
                        {
                            sw.WriteLine(str);
                        }
                    }

                    fileindex++;
                }
            }

            MergeFiles(ref fileindex);
        }
        static void Main(string[] args)
        {
            TestGenerator.GenerateTest();

            Sort();

        }
    }
}

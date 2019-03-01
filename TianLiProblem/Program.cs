using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TianLiProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> inputs = new List<int>() { 0, 1, 127, 128, 255, 256, 511, 512, 1024, 1280, 16383, 16384, 65535, 2097151, 2097152, 268435455, 268435456, -1 };

            foreach (var input in inputs)
            {
                Console.WriteLine(input + ":");
                var result = Convert(input);
                foreach (var item in result)
                {
                    Console.Write(item + ",");
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

        static List<int> Convert(int input)
        {
            var base2 = System.Convert.ToString(input, 2).ToCharArray();
            List<int> base2list = new List<int>();
            foreach (var item in base2)
            {
                base2list.Add(item - '0');
            }
            //现在是大端序

            base2list.Reverse();
            //现在是小端序

            while (base2list.Count % 7 != 0)
            {
                base2list.Add(0);
            }//高位补 0

            var byteList = new List<int>();

            List<int> buffer = new List<int>();
            for (int i = 0; i < base2list.Count; i++)
            {
                if (buffer.Count != 7)
                {
                    buffer.Add(base2list[i]);
                }
                if (buffer.Count == 7)
                {
                    buffer.Add(base2list.Skip(i + 1).Sum() == 0 ? 0 : 1);

                    int b = 0;
                    if (buffer[7] == 1) //负数的补码
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            b += (int)((buffer[j] ^ 1) * Math.Pow(2, j));
                        }
                        b += 1;
                        b *= -1;
                    }
                    else //正数的补码
                    {
                        for (int j = 0; j < 7; j++)
                        {
                            b += (int)(buffer[j] * Math.Pow(2, j));
                        }
                    }

                    byteList.Add(b);
                    buffer.Clear();
                }
            }
            return byteList;
        }
    }
}
